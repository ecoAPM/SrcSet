using SixLabors.Primitives;
using Xunit;

namespace ImageResizer.Tests
{
    public class SizeExtensionsTests
    {
        [Fact]
        public void AspectRatioIsCorrect()
        {
            //arrange
            var size = new Size(3, 2);

            //act
            var aspectRatio = size.AspectRatio();

            //assert
            Assert.Equal(1.5, aspectRatio);
        }
    }
}
