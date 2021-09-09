using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace SrcSet
{
	public class SrcSetManager
	{
		private readonly Func<byte[], Image<Rgba32>> _loadImage;
		private readonly Action<string> _log;

		public SrcSetManager(Func<byte[], Image<Rgba32>> loadImage, Action<string> log)
		{
			_loadImage = loadImage;
			_log = log;
		}

		public async Task SaveSrcSet(string filePath, IEnumerable<ushort> widths)
		{
			var data = await File.ReadAllBytesAsync(filePath);
			using var image = _loadImage(data);
			var size = new System.Drawing.Size(image.Width, image.Height);
			foreach (var newSize in widths.Select(width => size.Resize(width)))
			{
				var newFile = image.SaveResizedImage(filePath, newSize);
				if (newFile != null)
					_log(newFile);
			}
		}
	}
}