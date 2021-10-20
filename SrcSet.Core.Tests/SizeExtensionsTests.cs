using System.Drawing;
using Xunit;

namespace SrcSet.Core.Tests
{
	public sealed class SizeExtensionsTests
	{
		[Fact]
		public void CanResizeImage()
		{
			//arrange
			var size = new Size(6, 4);

			//act
			var newSize = size.Resize(3);

			//assert
			Assert.Equal(new Size(3, 2), newSize);
		}

		[Fact]
		public void CanCalculateLandscapeAspectRatio()
		{
			//arrange
			var size = new Size(3, 2);

			//act
			var aspectRatio = size.AspectRatio();

			//assert
			Assert.Equal(1.5, aspectRatio);
		}

		[Fact]
		public void CanCalculatePortraitAspectRatio()
		{
			//arrange
			var size = new Size(3, 4);

			//act
			var aspectRatio = size.AspectRatio();

			//assert
			Assert.Equal(0.75, aspectRatio);
		}
	}
}