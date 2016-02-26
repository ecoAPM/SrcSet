using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace ImageResizer
{
    public static class Program
    {
        private static readonly int[] defaultSizes = { 240, 320, 480, 640, 800, 960, 1280, 1600, 1920, 2400 };
        const string RECURSIVE_OPTION= "-r";


        public static void Main(string[] args)
        {

            
            if (args.Length < 1)
            {

                Console.WriteLine("Usage: resize filename/directory -s [size1 [size2 [size3 [...]]]] ");
                Console.WriteLine(string.Format("{0}: search also subdirectories",RECURSIVE_OPTION));
                Console.WriteLine("Default sizes: " + string.Join(" ", defaultSizes));
                return;
            }


            var attrs = File.GetAttributes(args[0]);
            if (attrs.HasFlag(FileAttributes.Directory))
            {

                SearchOption searchOption = SearchOption.TopDirectoryOnly;
                int skip = 1;
                if (args.Contains(RECURSIVE_OPTION))
                {
                    searchOption = SearchOption.AllDirectories;
                    skip++;
                }

                var files = Directory.EnumerateFiles(args[0], "*.*", searchOption)
                .Where(s =>
                s.EndsWith(".tiff", StringComparison.InvariantCultureIgnoreCase) ||
                s.EndsWith(".tif", StringComparison.InvariantCultureIgnoreCase) ||
                s.EndsWith(".bmp", StringComparison.InvariantCultureIgnoreCase) ||
                s.EndsWith(".png", StringComparison.InvariantCultureIgnoreCase) ||
                s.EndsWith(".jpg", StringComparison.InvariantCultureIgnoreCase));


                var sizes = args.Length == skip ? defaultSizes : args.Skip(skip).Select(a => Convert.ToInt32(a));
                foreach (var file in files)
                {
                    var t = new Thread(() =>
                    {
                        Resizer.Resize(file, sizes);
                    });
                    t.Start();
                }
                return;
            }
            if (args.Contains(RECURSIVE_OPTION))
            {
                Console.WriteLine(RECURSIVE_OPTION+" option cannot be used with single file, input a directory as first argument to use this option");
                return;
            }
            
            var filePath = args[0];

            var sizes2 = args.Length == 1 ? defaultSizes : args.Skip(1).Select(a => Convert.ToInt32(a));
            Resizer.Resize(filePath, sizes2);



        }
    }
}
