using System;
using System.IO;
using Xunit;
using SixLabors.ImageSharp;

namespace ImageResizer.Tests
{
    public class ResizerTests
    {
        private Resizer _resizer;

        public ResizerTests()
        {
            _resizer = new Resizer();
        }


        [Fact]
        public void AbleToResizePngFiles()
        {
            _resizer.Resize("Images/test.png", new[] {200, 300});

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
            _resizer.Resize("Images/test.jpg", new[] { 200, 300 });

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
            _resizer.Resize("Images/test.jpeg", new[] { 200, 300 });

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
            _resizer.Resize("Images/test.gif", new[] { 200, 300 });

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
            _resizer.Resize("Images/test.bmp", new[] { 200, 300 });

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
            var ex = Assert.Throws<NotSupportedException>(() => _resizer.Resize("Images/test.txt", new[] { 200, 300 }));

            Assert.True(ex.Message.Contains("Image cannot be loaded. Available decoders:"));
        }


    }
}
