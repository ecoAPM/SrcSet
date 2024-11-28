using Xunit;

namespace SrcSet.Tests;

public sealed class ArgumentsTests
{
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
		const string path = "test.png";

		//act
		var isDir = path.IsDirectory();

		//assert
		Assert.False(isDir);
	}

	[Fact]
	public void CanGetFilesForSingleFile()
	{
		//arrange
		var arg = "test.jpg";

		//act
		var files = arg.GetFiles(false, false);

		//assert
		var expected = new[] { "test.jpg" };
		Assert.Equal(expected, files);
	}

	[Fact]
	public void CanGetFilesForDirectory()
	{
		//arrange
		var arg = Directory.GetCurrentDirectory();

		//act
		var files = arg.GetFiles(true, true);

		//assert
		Assert.Contains(Path.Join(arg, "test.png"), files);
	}
}