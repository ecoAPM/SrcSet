using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Xunit;

namespace SrcSet.Core.Tests;

public sealed class ImageExtensionTests : IDisposable
{
	[Fact]
	public async Task CanSaveResizedImage()
	{
		//arrange
		var path = Path.Join(Directory.GetCurrentDirectory(), "test.png");
		var image = new Image<Rgba32>(2, 1);

		//act
		var newName = await image.Save(path);

		//assert
		Assert.Equal("test-0002.png", newName);
	}

	[Fact]
	public async Task SkipsExistingFiles()
	{
		//arrange
		var path = Path.Join(Directory.GetCurrentDirectory(), "test.png");
		var image = new Image<Rgba32>(1, 1);

		//act
		var newName = await image.Save(path);

		//assert
		Assert.Null(newName);
	}

	[Fact]
	public void CanResizeImage()
	{
		//arrange
		var image = new Image<Rgba32>(1, 1);

		//act
		var resized = image.Resize(new Size(2, 3));

		//assert
		Assert.Equal(2, resized.Width);
		Assert.Equal(3, resized.Height);
	}

	public void Dispose()
	{
		File.Delete("test-0002.png");
	}
}