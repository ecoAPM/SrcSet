using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Statiq.Common;
using Statiq.Core;
using Statiq.Testing;
using Xunit;

namespace SrcSet.Statiq.Tests;

public class ResponsiveImagesTests
{
	[Fact]
	public async Task FilesAreFilteredByImage()
	{
		//arrange
		var docs = new List<IDocument>
			{
				new TestDocument(new NormalizedPath("test.png")),
				new TestDocument(new NormalizedPath("test.txt")),
				new TestDocument(new NormalizedPath("test.jpg"))
			};
		var context = new TestExecutionContext();
		context.SetInputs(docs);

		var pipeline = new ResponsiveImages();
		var filter = pipeline.ProcessModules.First(m => m is FilterDocuments);

		//act
		var output = await filter.ExecuteAsync(context);

		//assert
		var files = output.Select(doc => doc.Destination.FileName.ToString()).ToArray();
		Assert.Equal(2, files.Length);
		Assert.Equal("test.png", files[0]);
		Assert.Equal("test.jpg", files[1]);
	}

	[Fact]
	public void CanGenerateDefaultHTML()
	{
		//arrange
		var context = new TestExecutionContext();
		const string baseImage = "img/test.png";

		//act
		var html = ResponsiveImages.SrcSet(baseImage);

		//assert
		Assert.NotNull(context);
		Assert.Equal(@"<img src=""/img/test-0640.png"" srcset=""/img/test-0240.png 240w, /img/test-0320.png 320w, /img/test-0480.png 480w, /img/test-0640.png 640w, /img/test-0800.png 800w, /img/test-0960.png 960w, /img/test-1280.png 1280w, /img/test-1600.png 1600w, /img/test-1920.png 1920w, /img/test-2400.png 2400w"" />", html);
	}

	[Fact]
	public void CanGenerateCustomHTML()
	{
		//arrange
		var context = new TestExecutionContext();
		const string baseImage = "img/test.png";
		const ushort defaultWidth = 2;
		var sizes = new ushort[] { 1, 2, 3 };
		var attributes = new Dictionary<string, string>
			{
				{ "attrX", "valX" },
				{ "attrY", "valY" },
				{ "attrZ", "valZ" },
			};

		//act
		var html = ResponsiveImages.SrcSet(baseImage, sizes, defaultWidth, attributes);

		//assert
		Assert.NotNull(context);
		Assert.Equal(@"<img src=""/img/test-0002.png"" srcset=""/img/test-0001.png 1w, /img/test-0002.png 2w, /img/test-0003.png 3w"" attrX=""valX"" attrY=""valY"" attrZ=""valZ"" />", html);
	}
}
