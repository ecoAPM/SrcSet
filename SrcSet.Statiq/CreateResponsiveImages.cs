using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SrcSet.Core;
using Statiq.Common;

namespace SrcSet.Statiq
{
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
			var alreadyDone = destinations.All(d => context.FileSystem.GetOutputFile(d).Exists);
			if (alreadyDone)
			{
				context.Log(LogLevel.Debug, $"Skipping {input.Source} since all sizes already exist");
				return destinations.Select(d => context.GetDocument(d));
			}

			var image = await _loadImage(inputStream);
			var documents = _widths.Select(w => GetResizedDocument(input.Source, image, w, context));
			return await Task.WhenAll(documents);
		}

		private static async Task<IDocument> GetResizedDocument(NormalizedPath source, Image image, ushort width, IExecutionContext context)
		{
			var destination = source.GetDestination(width);
			if (context.FileSystem.GetOutputFile(destination).Exists)
			{
				context.Log(LogLevel.Debug, $"Skipping {destination} since it already exists");
				return context.GetDocument(destination);
			}

			var encoder = image.DetectEncoder(destination);
			var output = new MemoryStream();

			context.Log(LogLevel.Debug, $"Resizing {source} to {destination}...");
			var newSize = image.Size().Resize(width);
			using var resized = image.Resize(newSize);
			await resized.SaveAsync(output, encoder);

			var content = context.GetContentProvider(output);
			return context.CreateDocument(source, destination, content);
		}
	}
}