using System;
using System.Threading.Tasks;
using Xunit;

namespace ImageResizer.Tests
{
    public class ProgramTests
    {
        [Fact]
        public async Task CanResizeImage()
        {
            //arrange
            var args = new [] { "test.png", "1"};

            //act
            var result = await Program.Main(args);

            //assert
            Assert.Equal(0, result);
        }
        [Fact]
        public async Task CanShowUsage()
        {
            //arrange
            var args = new string[0];

            //act
            var result = await Program.Main(args);

            //assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task ShowsErrorWhenNotFound()
        {
            //arrange
            var args = new [] { "abc" };

            //act
            var result = await Program.Main(args);

            //assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task ShowsErrorWhenRecursiveOnFile()
        {
            //arrange
            var args = new [] { "test.png", "-r" };

            //act
            var result = await Program.Main(args);

            //assert
            Assert.Equal(1, result);
        }
    }
}
