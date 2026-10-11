using SkiaSharp;

namespace SrcSet.Core;

public static class ImageExtensions
{
	public static async Task<string?> Save(this SKImage image, string filePath)
	{
		var newFileName = FileHelpers.GetFilename(filePath, (ushort)image.Width);
		var newPath = NewPath(filePath, newFileName);
		if (File.Exists(newPath))
			return null;

		await using var stream = new FileStream(newPath, FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite);
		SKBitmap.FromImage(image).Encode(stream, Path.GetExtension(filePath).GetFormat(), 100);
		return newFileName;
	}

	private static string NewPath(string filePath, string newFileName)
	{
		var dir = Path.GetDirectoryName(filePath) ?? string.Empty;
		var newPath = Path.Combine(dir, newFileName);
		return newPath;
	}

	public static SKImage Resize(this SKImage image, SKSizeI newSize)
		=> SKImage.FromBitmap(SKBitmap.FromImage(image).Resize(newSize, SKSamplingOptions.Default));
}