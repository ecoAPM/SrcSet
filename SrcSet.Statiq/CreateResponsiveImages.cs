using Microsoft.Extensions.Logging;
using SkiaSharp;
using SrcSet.Core;

namespace SrcSet.Statiq;

public class CreateResponsiveImages(Func<Stream, SKImage> loadImage, IEnumerable<ushort> widths) : ParallelModule
{
	protected override async Task<IEnumerable<IDocument>> ExecuteInputAsync(IDocument input, IExecutionContext context)
	{
		await using var inputStream = input.GetContentStream();
		var destinations = widths.Select(w => input.Source.GetDestination(w)).ToList();
		var cached = destinations.Select(d => context.FileSystem.GetOutputFile(d)).Where(f => f.Exists).ToList();
		var alreadyDone = cached.Count == destinations.Count;
		if (alreadyDone)
		{
			if (context.IsEnabled(LogLevel.Debug))
				context.Log(LogLevel.Debug, "Skipping {Source} since all sizes already exist", input.Source);

			var docs = cached.Select(f => CachedDocument(input.Source, f.Path, f, context));
			return await Task.WhenAll(docs);
		}

		var image = loadImage(inputStream);
		var documents = widths.Select(w => GetResizedDocument(input.Source, image, w, context));
		return await Task.WhenAll(documents);
	}

	private static async Task<IDocument> GetResizedDocument(NormalizedPath source, SKImage image, ushort width, IExecutionContext context)
	{
		var destination = source.GetDestination(width);
		var cached = context.FileSystem.GetOutputFile(destination);
		if (cached.Exists)
		{
			if (context.IsEnabled(LogLevel.Debug))
				context.Log(LogLevel.Debug, "Skipping {Destination} since it already exists", destination);

			return await CachedDocument(source, destination, cached, context);
		}

		using var output = new MemoryStream();

		if (context.IsEnabled(LogLevel.Debug))
			context.Log(LogLevel.Debug, "Resizing {Source} to {Destination}...", source, destination);

		var size = new SKSizeI(image.Width, image.Height);
		var newSize = size.Resize(width);
		using var resized = image.Resize(newSize);
		SKBitmap.FromImage(image).Encode(output, Path.GetExtension(destination.ToString()).GetFormat(), 100);

		var content = context.GetContentProvider(output);
		return context.CreateDocument(source, destination, content);
	}

	private static async Task<IDocument> CachedDocument(NormalizedPath source, NormalizedPath destination, IFile cached, IExecutionContext context)
	{
		using var stream = new MemoryStream();
		await cached.OpenRead().CopyToAsync(stream);
		var content = context.GetContentProvider(stream);
		return context.CreateDocument(source, context.FileSystem.GetRelativeOutputPath(destination), content);
	}
}