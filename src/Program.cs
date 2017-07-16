using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace ImageResizer
{
    public static class Program
    {
        private static readonly int[] _defaultSizes = { 240, 320, 480, 640, 800, 960, 1280, 1600, 1920, 2400 };
        private const string _recursive = "-r";

        public static void Main(string[] args)
        {
            var app = new CommandLineApplication();
            var validExtensions = new[] { ".jpg", ".jpeg", ".png", ".bmp", ".gif", ".tif", ".tiff" };
            IEnumerable<string> _files = new string[] { };

            var resize = app.Command("resize", config =>
            {
                config.Description = "Resize a image or multiple images";

                var fileNameOption = config.Option("-f", "Image file name", CommandOptionType.SingleValue);
                fileNameOption.LongName = "fileName";

                var dirNameOption = config.Option("-d", "Image files directory", CommandOptionType.SingleValue);
                dirNameOption.LongName = "dirName";

                var fileReSizes = config.Option("-s", "Result image size", CommandOptionType.MultipleValue);
                fileReSizes.LongName = "size";

                var recur = config.Option(_recursive, "Recursively resize images in a directory", CommandOptionType.NoValue);

                config.HelpOption("-? | -h | --help");

                config.OnExecute(() =>
                {
                    // fileName
                    var fileNameValue = fileNameOption.Value();

                    // dirName
                    var dirNameValue = dirNameOption.Value();

                    if (fileNameOption.HasValue() && dirNameOption.HasValue())
                    {
                        Console.WriteLine("Can specify either file or directory but not both.");
                        return 0;
                    }

                    if (!fileNameOption.HasValue() && !dirNameOption.HasValue())
                    {
                        Console.WriteLine("Should specify either file or directory.");
                        return 0;
                    }

                    if ((string.IsNullOrEmpty(fileNameValue) || !File.Exists(fileNameValue)) && !dirNameOption.HasValue())
                    {
                        Console.WriteLine($"Could not find file named {fileNameValue}");
                        return 0;
                    }

                    if ((string.IsNullOrEmpty(dirNameValue) || !Directory.Exists(dirNameValue)) && !fileNameOption.HasValue())
                    {
                        Console.WriteLine($"Could not find directory named {dirNameValue}");
                        return 0;
                    }

                    // recursive
                    var recursiveValue = recur.HasValue();

                    // cannot have file and recursion
                    if (recursiveValue && fileNameOption.HasValue())
                    {
                        Console.WriteLine("Recursive option cannot be used with files.");
                        return 0;
                    }

                    // if not recursive and file exists
                    if (!recursiveValue && File.Exists(fileNameValue))
                    {
                        var validFile = validExtensions.Contains(Path.GetExtension(fileNameValue).ToLower());
                        _files = validFile ? new[] { fileNameValue } : new string[] { };
                    }

                    // if directory exists
                    if (Directory.Exists(dirNameValue))
                    {
                        var searchOption = recursiveValue ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
                        _files = Directory.EnumerateFiles(dirNameValue, "*.*", searchOption)
                                    .Where(f => validExtensions.Contains(Path.GetExtension(f).ToLower()));
                    }

                    // fileresizes
                    int[] fileReSizesOptions = new int[] { };
                    try
                    {
                        fileReSizesOptions = fileReSizes.Values.Select(size => Convert.ToInt32(size)).ToArray();
                        if (fileReSizesOptions.Length == 0)
                        {
                            fileReSizesOptions = _defaultSizes;
                        }
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine($"Error converting sizes: {ex.Message}");
                    }

                    // resize files
                    foreach (var file in _files)
                    {
                        var t = new Thread(() => Resizer.Resize(file, fileReSizesOptions));
                        t.Start();
                        t.Join();
                    }

                    return 0;
                });
            });

            try
            {
                var result = app.Execute(args);
                Environment.Exit(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
