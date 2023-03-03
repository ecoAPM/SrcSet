using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Statiq.Testing;
using Xunit;

namespace SrcSet.Statiq.Tests;

public class CreateResponsiveImagesTests
{
	[Fact]
	public async Task ImageIsLoadedOnceAndResizedCorrectNumberOfTimes()
	{
		//arrange
		var doc = new TestDocument(new NormalizedPath("input/test.png"));
		var context = new TestExecutionContext();
		context.SetInputs(doc);

		Image image = new Image<Rgba32>(4, 4);
		var loaded = 0;

		Task<Image> Loader(Stream stream)
		{
			loaded++;
			return Task.FromResult(image);
		}

		var module = new CreateResponsiveImages(Loader, new ushort[] { 1, 2, 3 });

		//act
		var docs = await module.ExecuteAsync(context);

		//assert
		Assert.Equal(1, loaded);
		Assert.Equal(3, docs.Count());
	}

	[Fact]
	public async Task ImageIsNotLoadedIfAllSizesExist()
	{
		//arrange
		var doc = new TestDocument(new NormalizedPath("input/test.png"));
		var context = new TestExecutionContext();
		context.SetInputs(doc);
		context.FileSystem.GetOutputFile("input/test-0001.png").OpenWrite();
		context.FileSystem.GetOutputFile("input/test-0002.png").OpenWrite();
		context.FileSystem.GetOutputFile("input/test-0003.png").OpenWrite();

		var module = new CreateResponsiveImages(_ => throw new Exception(), new ushort[] { 1, 2, 3 });

		//act
		var docs = await module.ExecuteAsync(context);

		//assert
		Assert.Equal(3, docs.Count());
	}

	[Fact]
	public async Task ImageIsOnlyResizedForNewSizes()
	{
		//arrange
		var doc = new TestDocument(new NormalizedPath("input/test.png"));
		var context = new TestExecutionContext();
		context.SetInputs(doc);
		context.FileSystem.GetOutputFile("input/test-0002.png").OpenWrite();

		Image image = new Image<Rgba32>(4, 4);

		var module = new CreateResponsiveImages(_ => Task.FromResult(image), new ushort[] { 1, 2, 3 });

		//act
		await module.ExecuteAsync(context);

		//assert
		Assert.DoesNotContain(context.LogMessages, l => l.FormattedMessage.Contains("Skipping input/test-0001.png"));
		Assert.Contains(context.LogMessages, l => l.FormattedMessage.Contains("Skipping input/test-0002.png"));
		Assert.DoesNotContain(context.LogMessages, l => l.FormattedMessage.Contains("Skipping input/test-0003.png"));
	}
}