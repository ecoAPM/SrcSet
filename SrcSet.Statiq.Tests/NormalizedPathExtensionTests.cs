using Statiq.Common;
using Statiq.Testing;
using Xunit;

namespace SrcSet.Statiq.Tests;

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

		//act
		var newName = path.GetDestination(123);

		//assert
		Assert.NotNull(context);
		Assert.Equal("img/test-0123.png", newName);
	}
}
