using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace ImageResizer
{
    public static class Program
    {
        private static readonly int[] defaultSizes = { 240, 320, 480, 640, 800, 960, 1280, 1600, 1920, 2400 };

        public static void Main(string[] args)
        {
            switch (args.Length)
            {
                case 0:
                case 1:
                    Console.WriteLine("Usage: resize filename [size1 [size2 [size3 [...]]]]");
                    Console.WriteLine("Default sizes: " + string.Join(" ", defaultSizes));
                    break;
                case 2:
                    var attrs = File.GetAttributes(args[0]);
                    if (attrs.HasFlag(FileAttributes.Directory))
                    {
                        foreach (var file in Directory.GetFiles(args[0], "*.jpg"))
                        {
                            var t = new Thread(() =>
                            {
                                var r1 = new Resizer(args[1], file, defaultSizes);
                                r1.Resize();
                            });
                            t.Start();
                        }
                        break;
                    }
                    var r2 = new Resizer(args[1], args[0], defaultSizes);
                    r2.Resize();
                    break;
                default:
                    var r3 = new Resizer(args[1], args[0], args.Skip(2).Select(a => Convert.ToInt32(a)));
                    r3.Resize();
                    break;
            }
        }
    }
}
