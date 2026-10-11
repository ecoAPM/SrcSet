using SkiaSharp;

namespace SrcSet.Core;

public sealed class SrcSetManager
{
	public static readonly ushort[] DefaultSizes
		= [240, 320, 480, 640, 800, 960, 1280, 1600, 1920, 2400];

	public static readonly string[] ValidExtensions
		= [".jpg", ".png", ".webp"];

	private readonly Func<Stream, SKImage> _loadImage;
	private readonly Action<string> _log;

	/// <summary>
	/// Create a SrcSet Manager with the default parameters:
	/// - SkiaSharp's encoded image data loading
	/// - Output resulting filenames to console
	/// </summary>
	public SrcSetManager() : this(SKImage.FromEncodedData, Console.WriteLine)
	{
	}

	/// <summary>
	/// Create a SrcSet Manager
	/// </summary>
	/// <param name="loadImage">Function that turns a stream into a SkiaSharp image</param>
	/// <param name="log">Action that logs resulting output</param>
	public SrcSetManager(Func<Stream, SKImage> loadImage, Action<string> log)
	{
		_loadImage = loadImage;
		_log = log;
	}

	/// <summary>
	/// Saves a set
	/// </summary>
	/// <param name="filePath">The file path of the image to be resized</param>
	/// <param name="widths">The list of widths (in pixels) </param>
	public async Task SaveSrcSet(string filePath, IEnumerable<ushort> widths)
	{
		await using var stream = File.OpenRead(filePath);
		var image = _loadImage(stream);
		var tasks = widths.Select(width => Resize(filePath, image, new SKSizeI(image.Width, image.Height).Resize(width)));
		await Task.WhenAll(tasks);
	}

	private async Task Resize(string filePath, SKImage image, SKSizeI newSize)
	{
		using var resized = image.Resize(newSize);
		var newFile = await resized.Save(filePath);
		if (newFile != null)
			_log(newFile);
	}
}