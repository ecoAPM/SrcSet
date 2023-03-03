using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SrcSet.Core;

namespace SrcSet.Statiq;

public class CreateResponsiveImages : ParallelModule
{
	private readonly Func<Stream, Task<Image>> _loadImage;
	private readonly IEnumerable<ushort> _widths;

	public CreateResponsiveImages(Func<Stream, Task<Image>> loadImage, IEnumerable<ushort> widths)
	{
		_loadImage = loadImage;
		_widths = widths;
	}

	protected override async Task<IEnumerable<IDocument>> ExecuteInputAsync(IDocument input, IExecutionContext context)
	{
		var inputStream = input.GetContentStream();
		var destinations = _widths.Select(w => input.Source.GetDestination(w)).ToList();
		var cached = destinations.Select(d => context.FileSystem.GetOutputFile(d)).Where(f => f.Exists).ToList();
		var alreadyDone = cached.Count == destinations.Count;
		if (alreadyDone)
		{
			context.Log(LogLevel.Debug, "Skipping {source} since all sizes already exist", input.Source);
			var docs = cached.Select(f => CachedDocument(input.Source, f.Path, f, context));
			return await Task.WhenAll(docs);
		}

		var image = await _loadImage(inputStream);
		var documents = _widths.Select(w => GetResizedDocument(input.Source, image, w, context));
		return await Task.WhenAll(documents);
	}

	private static async Task<IDocument> GetResizedDocument(NormalizedPath source, Image image, ushort width, IExecutionContext context)
	{
		var destination = source.GetDestination(width);
		var cached = context.FileSystem.GetOutputFile(destination);
		if (cached.Exists)
		{
			context.Log(LogLevel.Debug, "Skipping {destination} since it already exists", destination);
			return await CachedDocument(source, destination, cached, context);
		}

		var encoder = image.DetectEncoder(destination.ToString());
		var output = new MemoryStream();

		context.Log(LogLevel.Debug, "Resizing {source} to {destination}...", source, destination);
		var newSize = image.Size.Resize(width);
		using var resized = image.Resize(newSize);
		await resized.SaveAsync(output, encoder);

		var content = context.GetContentProvider(output);
		return context.CreateDocument(source, destination, content);
	}

	private static async Task<IDocument> CachedDocument(NormalizedPath source, NormalizedPath destination, IFile cached, IExecutionContext context)
	{
		var stream = new MemoryStream();
		await cached.OpenRead().CopyToAsync(stream);
		var content = context.GetContentProvider(stream);
		return context.CreateDocument(source, context.FileSystem.GetRelativeOutputPath(destination), content);
	}
}