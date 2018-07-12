using System;
using System.IO;
using System.Text;

namespace ESO_Discord_RichPresence_Client
{
    class UnpackAddonFiles
    {
        /// <summary>
        /// This utility is used to extract the addon file to a given folder.
        /// </summary>
        // This is done because there isn't a standard way in .NET that can pack files
        // that can be unpacked with every Windows versions, and cross-platform.
        public static void UnpackAddon(string OutputPath)
        {
            Console.WriteLine($"Unpacking contents of addon to '{OutputPath}'...");
            Console.WriteLine();

            using (MemoryStream Input = new MemoryStream(Properties.Resources.ADDON))
            using (BinaryReader Reader = new BinaryReader(Input))
            {
                if (Encoding.UTF8.GetString(Reader.ReadBytes(10)) != "WHISPERITY")
                    throw new InvalidDataException("Embedded addon contents are invalid - check build process.");

                UInt64 Index = Reader.ReadUInt64();
                while (Index != 0) // Until not EOF...
                {
                    string PackPath = Reader.ReadString();
                    string FilePath = OutputPath + PackPath;
                    long Length = Reader.ReadInt64();

                    // Ensure that the subdirectory needed exists.
                    if (!Directory.Exists(Path.GetDirectoryName(FilePath)))
                        Directory.CreateDirectory(Path.GetDirectoryName(FilePath));

                    using (FileStream Output = new FileStream(FilePath, FileMode.OpenOrCreate, FileAccess.Write))
                    using (BinaryWriter Writer = new BinaryWriter(Output))
                    {
                        Output.SetLength(0);
                        byte[] Buffer = Reader.ReadBytes((int)Length); // Let's hope input files don't exceed 2^31 bytes...
                        Writer.Write(Buffer);

                        Console.WriteLine($"#{Index}: {FilePath} ({Length} bytes) extracted.");
                    }

                    Index = Reader.ReadUInt64(); // Go for next file, or find EOF.
                }
            }
            
            Console.WriteLine();
            Console.WriteLine("Done.");
        }
    }
}
