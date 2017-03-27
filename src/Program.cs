using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace ImageResizer
{
    public static class Program
    {
        private static readonly int[] defaultSizes = { 240, 320, 480, 640, 800, 960, 1280, 1600, 1920, 2400 };
        private const string recursive = "-r";

        public static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: resize {filename or directory} [-r] [size1 [size2 [size3 [...]]]] ");
                Console.WriteLine("{0}: recurse subdirectories", recursive);
                Console.WriteLine("Default sizes: " + string.Join(" ", defaultSizes));
                return;
            }

            var fileOrDirectoryArg = args[0];
            if (!File.Exists(fileOrDirectoryArg) && !Directory.Exists(fileOrDirectoryArg))
            {
                Console.WriteLine("Could not find file or directory named \"{0}\"", fileOrDirectoryArg);
                return;
            }

            var attrs = File.GetAttributes(fileOrDirectoryArg);
            var resizeRecursively = args.Contains(recursive);
            var resizeDirectory = attrs.HasFlag(FileAttributes.Directory);
            if (resizeRecursively && !resizeDirectory)
            {
                Console.WriteLine("\"" + recursive + "\" can only be used with a directory");
                return;
            }

            var searchOption = resizeRecursively ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            var validExtensions = new[] { ".jpg", ".jpeg", ".png", ".bmp", ".gif", ".tif", ".tiff" };
            var files = resizeDirectory
                ? Directory.EnumerateFiles(fileOrDirectoryArg, "*.*", searchOption).Where(f => validExtensions.Contains(Path.GetExtension(f).ToLower()))
                : new[] { fileOrDirectoryArg };

            var sizes = getSizes(args);
            foreach (var file in files)
            {
                var t = new Thread(() => Resizer.Resize(file, sizes));
                t.Start();
            }
        }

        private static IEnumerable<int> getSizes(IReadOnlyCollection<string> args)
        {
            var numberOfFilesystemArguments = args.Contains(recursive) ? 2 : 1;
            var sizes = args.Count == numberOfFilesystemArguments
                ? defaultSizes
                : args.Skip(numberOfFilesystemArguments).Select(a => Convert.ToInt32(a));
            return sizes;
        }
    }
}
