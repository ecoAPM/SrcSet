using System;
using System.Threading.Tasks;
using Xunit;

namespace SrcSet.Tests
{
	public sealed class ProgramTests
	{
		[Fact]
		public async Task CanResizeImage()
		{
			//arrange
			var args = new[] { "test.png", "1" };

			//act
			var result = await Program.Main(args);

			//assert
			Assert.Equal(0, result);
		}

		[Fact]
		public async Task CanShowUsage()
		{
			//arrange
			var args = Array.Empty<string>();

			//act
			var result = await Program.Main(args);

			//assert
			Assert.Equal(1, result);
		}

		[Fact]
		public async Task ShowsErrorWhenNotFound()
		{
			//arrange
			var args = new[] { "abc" };

			//act
			var result = await Program.Main(args);

			//assert
			Assert.Equal(1, result);
		}

		[Fact]
		public async Task ShowsErrorWhenRecursiveOnFile()
		{
			//arrange
			var args = new[] { "test.png", "-r" };

			//act
			var result = await Program.Main(args);

			//assert
			Assert.Equal(1, result);
		}
	}
}