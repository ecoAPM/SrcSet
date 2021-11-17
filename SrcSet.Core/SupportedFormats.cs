using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Tga;

namespace SrcSet.Core;

public static class SupportedFormats
{
	public static readonly IImageFormat[] Default =
	{
			BmpFormat.Instance,
			JpegFormat.Instance,
			GifFormat.Instance,
			PngFormat.Instance,
			TgaFormat.Instance
		};
}