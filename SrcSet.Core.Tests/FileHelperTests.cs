using Xunit;

namespace SrcSet.Core.Tests;

public sealed class FileHelperTests
{
	[Fact]
	public void CanGetFilenameWithWidth()
	{
		//arrange
		const string filename = "test.png";

		//act
		var newName = FileHelpers.GetFilename(filename, 123);

		//assert
		Assert.Equal("test-0123.png", newName);
	}
}