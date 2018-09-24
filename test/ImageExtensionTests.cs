using System;
using System.Drawing;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Xunit;

namespace ImageResizer.Tests
{
    public class ImageExtensionTests : IDisposable
    {
        [Fact]
        public void CanSaveResizedImage()
        {
            //arrange
            var path = Path.Join(Directory.GetCurrentDirectory(), "test.png");
            var image = new Image<Rgba32>(1, 1);

            //act
            var newName = image.SaveResizedImage(path, new Size(2, 2));

            //assert
            Assert.Equal("test-0002.png", newName);
        }

        [Fact]
        public void SkipsExistingFiles()
        {
            //arrange
            var path = Path.Join(Directory.GetCurrentDirectory(), "test.png");
            var image = new Image<Rgba32>(1, 1);

            //act
            var newName = image.SaveResizedImage(path, new Size(1, 1));

            //assert
            Assert.Null(newName);
        }

        public void Dispose()
        {
            File.Delete("test-0002.png");
        }
    }
}
