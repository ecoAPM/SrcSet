using System.IO;
using Xunit;

namespace ImageResizer.Tests
{
    public class ArgumentsTests
    {
        [Fact]
        public void CanGetDefaultSizesWhenRecursive()
        {
            //arrange
            var args = new[] {"path", "-r"};

            //act
            var sizes = args.GetSizes();

            //assert
            Assert.Equal(Arguments.DefaultSizes, sizes);
        }

        [Fact]
        public void CanGetDefaultCustomSizesWhenNotRecursive()
        {
            //arrange
            var args = new[] {"path"};

            //act
            var sizes = args.GetSizes();

            //assert
            Assert.Equal(Arguments.DefaultSizes, sizes);
        }

        [Fact]
        public void CanGetCustomSizesWhenRecursive()
        {
            //arrange
            var args = new[] {"path", "-r", "123", "234"};

            //act
            var sizes = args.GetSizes();

            //assert
            Assert.Equal(new ushort[] { 123, 234 }, sizes);
        }

        [Fact]
        public void CanGetCustomSizesWhenNotRecursive()
        {
            //arrange
            var args = new[] {"path", "123", "234"};

            //act
            var sizes = args.GetSizes();

            //assert
            Assert.Equal(new ushort[] { 123, 234 }, sizes);
        }

        [Fact]
        public void IsDirectoryIsTrueWhenGivenADirectory()
        {
            //arrange
            var path = Directory.GetCurrentDirectory();

            //act
            var isDir = path.IsDirectory();

            //assert
            Assert.True(isDir);
        }

        [Fact]
        public void IsDirectoryIsFalseWhenGivenAFile()
        {
            //arrange
            var path = "test.png";

            //act
            var isDir = path.IsDirectory();

            //assert
            Assert.False(isDir);
        }
    }
}
