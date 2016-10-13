using ImageProcessorCore;

namespace ImageResizer
{
    public static class SizeExtensions
    {
        public static double AspectRatio(this Size size)
        {
            return (double)size.Width / size.Height;
        }
    }
}
