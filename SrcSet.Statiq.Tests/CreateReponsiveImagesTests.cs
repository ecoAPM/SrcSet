using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Statiq.Common;
using Statiq.Testing;
using Xunit;

namespace SrcSet.Statiq.Tests
{
	public class CreateReponsiveImagesTests
	{
		[Fact]
		public async Task ImageIsLoadedOnceAndResizedThrice()
		{
			//arrange
			var doc = Substitute.For<IDocument>();
			doc.ContentProvider.GetStream().Returns(new MemoryStream());
			doc.Source.Returns(new NormalizedPath("/input/test.png", PathKind.Absolute));

			var context = new TestExecutionContext();
			context.SetInputs(doc);

			var loader = Substitute.For<Func<Stream, Task<Image>>>();
			loader.Invoke(Arg.Any<Stream>()).Returns(new Image<Rgba32>(4, 4));

			var module = new CreateResponsiveImages(loader, new ushort[] { 1, 2, 3 });

			//act
			var docs = await module.ExecuteAsync(context);

			//assert
			await loader.Received(1).Invoke(Arg.Any<Stream>());
			Assert.Equal(3, docs.Count());
		}
	}
}