using System;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Xunit;

namespace SrcSet.Tests
{
    public class SrcSetManagerTests : IDisposable
    {
        [Fact]
        public void CanResizeImage()
        {
            //arrange
            var manager = new SrcSetManager(s => new Image<Rgba32>(1, 1));

            //act
            manager.SaveSrcSet("test.png", new ushort[] { 3 });

            //assert
            Assert.True(File.Exists("test-0003.png"));
        }

        public void Dispose()
            => File.Delete("test-0003.png");
    }
}
