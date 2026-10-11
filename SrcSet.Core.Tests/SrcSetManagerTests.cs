using SkiaSharp;
using Xunit;

namespace SrcSet.Core.Tests;

public sealed class SrcSetManagerTests : IDisposable
{
	[Fact]
	public void CanCreateDefaultManager()
	{
		//act
		var manager = new SrcSetManager();

		//assert
		Assert.IsType<SrcSetManager>(manager);
	}

	[Fact]
	public async Task CanResizeImage()
	{
		//arrange
		var image = SKImage.FromBitmap(new SKBitmap(1, 1));
		var manager = new SrcSetManager(_ => image, _ => { });

		//act
		await manager.SaveSrcSet("test.png", [3]);

		//assert
		Assert.True(File.Exists("test-0003.png"));
	}

	public void Dispose()
		=> File.Delete("test-0003.png");
}