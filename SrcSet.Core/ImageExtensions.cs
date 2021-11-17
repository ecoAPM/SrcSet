using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace SrcSet.Core;

public static class ImageExtensions
{
	public static async Task<string?> Save(this Image image, string filePath)
	{
		var newFileName = FileHelpers.GetFilename(filePath, (ushort)image.Width);
		var newPath = NewPath(filePath, newFileName);
		if (File.Exists(newPath))
			return null;

		await image.SaveAsync(newPath);
		return newFileName;
	}

	private static string NewPath(string filePath, string newFileName)
	{
		var dir = Path.GetDirectoryName(filePath) ?? string.Empty;
		var newPath = Path.Combine(dir, newFileName);
		return newPath;
	}

	public static Image Resize(this Image image, Size newSize)
		=> image.Clone(i => i.Resize(newSize.Width, newSize.Height, KnownResamplers.Lanczos8));
}