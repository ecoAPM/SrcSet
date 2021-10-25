using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace SrcSet.Core
{
	public static class ImageExtensions
	{
		public static string SaveResizedImage(this Image<Rgba32> image, string filePath, Size newSize)
		{
			var dir = Path.GetDirectoryName(filePath);
			var newFileName = FileHelpers.GetFilename(filePath, newSize);
			var newPath = Path.Combine(dir, newFileName);
			if (File.Exists(newPath))
				return null;

			using var resized = image.CloneAs<Rgba32>();
			resized.Mutate(i => i.Resize(newSize.Width, newSize.Height));
			resized.Save(newPath);

			return newFileName;
		}
	}
}