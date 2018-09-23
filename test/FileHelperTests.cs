using System.Drawing;
using Xunit;

namespace ImageResizer.Tests
{
    public class FileHelperTests
    {
        [Fact]
        public void CanGetFilenameWithWidth()
        {
            //arrange
            var filename = "test.png";
            var size = new Size(123, 234);

            //act
            var newName = FileHelpers.GetFilename(filename, size);

            //assert
            Assert.Equal("test-0123.png", newName);
        }
    }
}
