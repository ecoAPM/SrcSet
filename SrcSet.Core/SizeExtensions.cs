using SkiaSharp;

namespace SrcSet.Core;

public static class SizeExtensions
{
	public static SKSizeI Resize(this SKSizeI size, ushort width)
		=> new(width, (ushort)(width / size.AspectRatio()));

	public static double AspectRatio(this SKSizeI size)
		=> (double)size.Width / size.Height;
}