using SixLabors.ImageSharp;

namespace SrcSet.Core;

public sealed class SrcSetManager
{
	public static readonly ushort[] DefaultSizes
		= { 240, 320, 480, 640, 800, 960, 1280, 1600, 1920, 2400 };

	public static readonly IReadOnlyCollection<string> ValidExtensions
		= SupportedFormats.Default
			.SelectMany(i => i.FileExtensions)
			.Select(e => $".{e.ToLower()}")
			.ToArray();

	private readonly Func<Stream, Task<Image>> _loadImage;
	private readonly Action<string> _log;

	/// <summary>
	/// Create a SrcSet Manager with the default parameters:
	/// - ImageSharp's async image loading
	/// - Output resulting filenames to console
	/// </summary>
	public SrcSetManager() : this(Image.LoadAsync, Console.WriteLine)
	{
	}

	/// <summary>
	/// Create a SrcSet Manager
	/// </summary>
	/// <param name="loadImage">Function that turns a stream into an ImageSharp image</param>
	/// <param name="log">Action that logs resulting output</param>
	public SrcSetManager(Func<Stream, Task<Image>> loadImage, Action<string> log)
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
		var stream = File.OpenRead(filePath);
		var image = await _loadImage(stream);
		var size = image.Size();
		var tasks = widths.Select(width => Resize(filePath, image, size.Resize(width)));
		await Task.WhenAll(tasks);
	}

	private async Task Resize(string filePath, Image image, Size newSize)
	{
		var resized = image.Resize(newSize);
		var newFile = await resized.Save(filePath);
		if (newFile != null)
			_log(newFile);
	}
}