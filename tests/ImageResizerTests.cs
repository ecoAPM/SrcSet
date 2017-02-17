using Xunit;
using ImageResizer;

namespace ImageResizerTests
{
    public class SizeExtensionsTests
    {
        [Fact]
        public void AspectRatioTest()
        {
            ImageProcessorCore.Size size = new ImageProcessorCore.Size(50, 10);
            double testAspectRatio = SizeExtensions.AspectRatio(size);
            Assert.Equal(5, testAspectRatio );
        }


        /*  
               [Theory]
               [InlineData(3)]
               [InlineData(5)]
               [InlineData(6)]
               public void MyFirstTheory(int value)
               {
                   Assert.True(IsOdd(value));
               }

               bool IsOdd(int value)
               {
                   return value % 2 == 1;
               }
           */
    }
}