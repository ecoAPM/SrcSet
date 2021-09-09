using System.Drawing;

namespace SrcSet
{
	public static class SizeExtensions
	{
		public static Size Resize(this Size size, ushort width)
			=> new(width, (ushort)(width / size.AspectRatio()));

		public static double AspectRatio(this Size size)
			=> (double)size.Width / size.Height;
	}
}