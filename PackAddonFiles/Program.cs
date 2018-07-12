using System;
using System.IO;
using System.Text;

namespace PackAddonFiles
{
    class Program
    {
        /// <summary>
        /// This utility is used to create an archive from the specified folder.
        /// </summary>
        // This is done because there isn't a standard way in .NET that can pack files
        // that can be unpacked with every Windows versions, and cross-platform.
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.Error.WriteLine("Usage: PackAddonFiles <addon-folder> <output-file>.");
                Environment.Exit(2);
            }

            Console.WriteLine($"Packing contents of folder '{args[0]}'...");
            Console.WriteLine($"Output will be written to '{args[1]}'.");
            Console.WriteLine();

            using (FileStream Output = new FileStream(args[1], FileMode.OpenOrCreate, FileAccess.Write))
            using (BinaryWriter Writer = new BinaryWriter(Output))
            {
                Output.SetLength(0); // Truncate file if already exists.
                Writer.Write(Encoding.UTF8.GetBytes("WHISPERITY")); // Magic header (10)

                UInt64 Index = 0;
                foreach (string FilePath in Directory.GetFiles(args[0], "*", SearchOption.AllDirectories))
                {
                    string PackPath = FilePath.Replace(Path.GetFullPath(args[0]), String.Empty);

                    ++Index;
                    Writer.Write(Index); // UInt64 (8)
                    Writer.Write(PackPath); // String in Pascal representation (2 + n)

                    using (FileStream Input = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.None))
                    {
                        Writer.Write(Input.Length); // Int64 (8)

                        byte[] Buffer = new byte[Input.Length];
                        Input.Read(Buffer, 0, (int)Input.Length); // Let's hope input files don't exceed 2^31 bytes...
                        Writer.Write(Buffer); // arbitrary bitstream (n)

                        Console.WriteLine($"#{Index}: {PackPath} ({Input.Length} bytes).");
                    }
                }

                Writer.Write((UInt64)0); // EOF bit. (8)
            }

            Console.WriteLine();
            Console.WriteLine("Done.");
        }
    }
}
