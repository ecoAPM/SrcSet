using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SixLabors.ImageSharp;

namespace SrcSet.Core
{
	public sealed class SrcSetManager
	{
		public static readonly ushort[] DefaultSizes
			= { 240, 320, 480, 640, 800, 960, 1280, 1600, 1920, 2400 };

		public static readonly IReadOnlyCollection<string> ValidExtensions
			= SupportedFormats.Default
				.SelectMany(i => i.FileExtensions)
				.Select(e => $".{e.ToLower()}")
				.ToArray();

		private readonly Func<Stream, Task<Image>> _loadImage;
		private readonly Action<string> _log;

		public SrcSetManager(Func<Stream, Task<Image>> loadImage, Action<string> log)
		{
			_loadImage = loadImage;
			_log = log;
		}

		public async Task SaveSrcSet(string filePath, IEnumerable<ushort> widths)
		{
			var stream = File.OpenRead(filePath);
			using var image = await _loadImage(stream);
			var size = image.Size();
			foreach (var newSize in widths.Select(width => size.Resize(width)))
			{
				var resized = image.Resize(newSize);
				var newFile = await resized.Save(filePath);
				if (newFile != null)
					_log(newFile);
			}
		}
	}
}