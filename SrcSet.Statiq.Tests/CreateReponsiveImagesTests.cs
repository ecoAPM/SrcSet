using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
	}
}