using System.IO;
using Xunit;

namespace SrcSet.Tests
{
	public class ArgumentsTests
	{
		[Fact]
		public void CanGetDefaultSizesWhenRecursive()
		{
			//arrange
			var args = new[] { "path", "-r" };

			//act
			var sizes = args.GetSizes();

			//assert
			Assert.Equal(Arguments.DefaultSizes, sizes);
		}

		[Fact]
		public void CanGetDefaultCustomSizesWhenNotRecursive()
		{
			//arrange
			var args = new[] { "path" };

			//act
			var sizes = args.GetSizes();

			//assert
			Assert.Equal(Arguments.DefaultSizes, sizes);
		}

		[Fact]
		public void CanGetCustomSizesWhenRecursive()
		{
			//arrange
			var args = new[] { "path", "-r", "123", "234" };

			//act
			var sizes = args.GetSizes();

			//assert
			Assert.Equal(new ushort[] { 123, 234 }, sizes);
		}

		[Fact]
		public void CanGetCustomSizesWhenNotRecursive()
		{
			//arrange
			var args = new[] { "path", "123", "234" };

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

		[Fact]
		public void CanGetFilesForSingleFile()
		{
			//arrange
			var arg = "test.jpg";

			//act
			var files = arg.GetFiles(false, false);

			//assert
			Assert.Equal(new[] { "test.jpg" }, files);
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
}