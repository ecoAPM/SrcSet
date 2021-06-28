using System;
using System.IO;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Xunit;

namespace SrcSet.Tests
{
    public class SrcSetManagerTests : IDisposable
    {
        [Fact]
        public async Task CanResizeImage()
        {
            //arrange
            var manager = new SrcSetManager(s => new Image<Rgba32>(1, 1), s => {});

            //act
            await manager.SaveSrcSet("test.png", new ushort[] { 3 });

            //assert
            Assert.True(File.Exists("test-0003.png"));
        }

        public void Dispose()
            => File.Delete("test-0003.png");
    }
}
