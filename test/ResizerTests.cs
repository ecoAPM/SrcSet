using System;
using System.IO;
using Xunit;
using SixLabors.ImageSharp;

namespace ImageResizer.Tests
{
    public class ResizerTests
    {

        [Fact]
        public void AbleToResizePngFiles()
        {
            Resizer.Resize("Images/test.png", new[] {200, 300});

            using (var stream = File.OpenRead("Images/test-0200.png"))
            {
                var image = Image.Load(stream);

                Assert.Equal(133, image.Height);
                Assert.Equal(200, image.Width);
            }

            using (var stream = File.OpenRead("Images/test-0300.png"))
            {
                var image = Image.Load(stream);

                Assert.Equal(200, image.Height);
                Assert.Equal(300, image.Width);
            }

            File.Delete("Images/test-0200.png");
            File.Delete("Images/test-0300.png");
        }

        [Fact]
        public void AbleToResizeJpgFiles()
        {
            Resizer.Resize("Images/test.jpg", new[] { 200, 300 });

            using (var stream = File.OpenRead("Images/test-0200.jpg"))
            {
                var image = Image.Load(stream);

                Assert.Equal(133, image.Height);
                Assert.Equal(200, image.Width);
            }

            using (var stream = File.OpenRead("Images/test-0300.jpg"))
            {
                var image = Image.Load(stream);

                Assert.Equal(200, image.Height);
                Assert.Equal(300, image.Width);
            }

            File.Delete("Images/test-0200.jpg");
            File.Delete("Images/test-0300.jpg");
        }

        [Fact]
        public void AbleToResizeJpegFiles()
        {
            Resizer.Resize("Images/test.jpeg", new[] { 200, 300 });

            using (var stream = File.OpenRead("Images/test-0200.jpeg"))
            {
                var image = Image.Load(stream);

                Assert.Equal(133, image.Height);
                Assert.Equal(200, image.Width);
            }

            using (var stream = File.OpenRead("Images/test-0300.jpeg"))
            {
                var image = Image.Load(stream);

                Assert.Equal(200, image.Height);
                Assert.Equal(300, image.Width);
            }

            File.Delete("Images/test-0200.jpeg");
            File.Delete("Images/test-0300.jpeg");
        }

        [Fact]
        public void AbleToResizeGifFiles()
        {
            Resizer.Resize("Images/test.gif", new[] { 200, 300 });

            using (var stream = File.OpenRead("Images/test-0200.gif"))
            {
                var image = Image.Load(stream);

                Assert.Equal(133, image.Height);
                Assert.Equal(200, image.Width);
            }

            using (var stream = File.OpenRead("Images/test-0300.gif"))
            {
                var image = Image.Load(stream);

                Assert.Equal(200, image.Height);
                Assert.Equal(300, image.Width);
            }

            File.Delete("Images/test-0200.gif");
            File.Delete("Images/test-0300.gif");
        }

        [Fact]
        public void AbleToResizeBmpFiles()
        {
            Resizer.Resize("Images/test.bmp", new[] { 200, 300 });

            using (var stream = File.OpenRead("Images/test-0200.bmp"))
            {
                var image = Image.Load(stream);

                Assert.Equal(133, image.Height);
                Assert.Equal(200, image.Width);
            }

            using (var stream = File.OpenRead("Images/test-0300.bmp"))
            {
                var image = Image.Load(stream);

                Assert.Equal(200, image.Height);
                Assert.Equal(300, image.Width);
            }

            File.Delete("Images/test-0200.bmp");
            File.Delete("Images/test-0300.bmp");
        }

        [Fact]
        public void ThrowsExceptionWithUnsupportedFileType()
        {
            var ex = Assert.Throws<NotSupportedException>(() => Resizer.Resize("Images/test.txt", new[] { 200, 300 }));

            Assert.Equal("Image cannot be loaded. Available decoders:\r\n - GIF : GifDecoder\r\n - BMP : BmpDecoder\r\n - PNG : PngDecoder\r\n - JPEG : JpegDecoder\r\n", ex.Message);
        }


    }
}
