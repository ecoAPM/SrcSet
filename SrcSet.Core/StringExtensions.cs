using SkiaSharp;

namespace SrcSet.Core;

public static class StringExtensions
{
	public static SKEncodedImageFormat GetFormat(this string extension)
		=> extension switch
		{
			".jpg" => SKEncodedImageFormat.Jpeg,
			".png" => SKEncodedImageFormat.Png,
			".webp" => SKEncodedImageFormat.Webp,
			_ => throw new ArgumentOutOfRangeException(nameof(extension), extension, "Unsupported file extension")
		};
}