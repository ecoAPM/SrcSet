using SixLabors.ImageSharp;
using Statiq.Common;
using Statiq.Testing;
using Xunit;

namespace SrcSet.Statiq.Tests
{
	public class NormalizedPathExtensionTests
	{
		[Theory]
		[InlineData("test.png", true)]
		[InlineData("test.jpg", true)]
		[InlineData("test.txt", false)]
		public void CorrectFilesAreImages(string filename, bool expected)
		{
			//arrange
			var path = new NormalizedPath(filename);

			//act
			var isImage = path.IsImage();

			//assert
			Assert.Equal(expected, isImage);
		}

		[Fact]
		public void CanGetDestinationFileName()
		{
			//arrange
			var context = new TestExecutionContext();
			var path = new NormalizedPath("img/test.png");
			var size = new Size(123, 234);

			//act
			var newName = path.GetDestination(size);

			//assert
			Assert.NotNull(context);
			Assert.Equal("img/test-0123.png", newName);
		}
	}
}