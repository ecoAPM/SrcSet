using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
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
		Image image = new Image<Rgba32>(1, 1);
		var manager = new SrcSetManager(_ => Task.FromResult(image), _ => { });

		//act
		await manager.SaveSrcSet("test.png", new ushort[] { 3 });

		//assert
		Assert.True(File.Exists("test-0003.png"));
	}

	public void Dispose()
		=> File.Delete("test-0003.png");
}