﻿using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SixLabors.ImageSharp;

namespace SrcSet
{
    public static class Program
    {
        public static async Task<int> Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: resize {filename or directory} [-r] [size1 [size2 [size3 [...]]]] ");
                Console.WriteLine("{0}: recurse subdirectories", Arguments.RecursiveFlag);
                Console.WriteLine("Default sizes: " + string.Join(" ", Arguments.DefaultSizes));
                return 1;
            }

            var fileOrDirectoryArg = args[0];
            if (!File.Exists(fileOrDirectoryArg) && !Directory.Exists(fileOrDirectoryArg))
            {
                Console.WriteLine("Could not find file or directory named \"{0}\"", fileOrDirectoryArg);
                return 1;
            }

            var resizeRecursively = args.Contains(Arguments.RecursiveFlag);
            var resizeDirectory = fileOrDirectoryArg.IsDirectory();
            if (resizeRecursively && !resizeDirectory)
            {
                Console.WriteLine("\"" + Arguments.RecursiveFlag + "\" can only be used with a directory");
                return 1;
            }

            var manager = new SrcSetManager(Image.Load);
            var sizes = args.GetSizes();
            var resizeTasks = fileOrDirectoryArg.GetFiles(resizeRecursively, resizeDirectory)
                .Select(async file => await Task.Run(() => manager.SaveSrcSet(file, sizes)))
                .ToArray();
            await Task.WhenAll(resizeTasks);
            return 0;
        }
    }
}