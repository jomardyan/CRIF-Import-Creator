using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading;

namespace CRIF_Encrypt
{
    internal static class Utilities
    {
        internal static string ToUTF8(this string text)
        {
            return Encoding.UTF8.GetString(Encoding.Default.GetBytes(text));
        }

        internal static void StartingPoint()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("(c) Hayk Jomardyan 2022. All rights reserved.  v15 \n");
            Console.WriteLine("https://github.com/jomardyan/CRIF-Import-Creator \n");

            Console.ResetColor();
            Console.WriteLine("Initializing, please wait... \n");
            Thread.Sleep(100);
        }

        internal static string SignAndEncrypt(string inputDir, string fileName)
        {
            string fileBaseName = Path.GetFileNameWithoutExtension(fileName);
            string command = $"/C gpg.exe -v -se -r CRIF-SWO-PROD --passphrase \"\" --local-user 0x9F674BC8 \"{Path.Combine(inputDir, fileBaseName, $"{fileName}.zip")}\"";
            return command;
        }

        internal static string CreateDatDir(string path, string fileName)
        {
            string fullPath = Path.Combine(path, fileName, fileName);
            Console.WriteLine("Creating datdir folder...");
            try
            {
                if (Directory.Exists(fullPath))
                {
                    Console.WriteLine($"That path exists already: {fullPath}");
                }
                else
                {
                    Directory.CreateDirectory(fullPath);
                    Console.WriteLine($"The directory was created successfully at {Directory.GetCreationTime(fullPath)}. Path: {fullPath}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"The process failed: {e}");
            }
            return fullPath;
        }

        internal static void ReplaceCrifAndSaveDat(string fileName, string directory)
        {
            try
            {
                string fileBaseName = Path.GetFileNameWithoutExtension(fileName);
                string text = File.ReadAllText(fileName);
                text = text.Replace("\t", "^~");

                text = RemoveFirstAndLastLines(text);

                string datDir = CreateDatDir(directory, fileBaseName);
                Thread.Sleep(500);

                string datOutput = Path.Combine(datDir, $"{fileBaseName}.dat");
                File.WriteAllText(datOutput, text);
                Console.WriteLine($"Saved: {datOutput}");
                Thread.Sleep(500);

                string zipPath = Path.Combine(directory, fileBaseName, $"{fileBaseName}.zip");
                Console.WriteLine($"ZIP PATH: {zipPath}");
                ZipFile.CreateFromDirectory(datDir, zipPath);

                Directory.Delete(datDir, true);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e}");
            }
        }

        private static string RemoveFirstAndLastLines(string text)
        {
            string[] lines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            if (lines.Length <= 2)
            {
                return string.Empty;
            }

            var stringBuilder = new StringBuilder();
            for (int i = 1; i < lines.Length - 1; i++)
            {
                stringBuilder.AppendLine(lines[i]);
            }

            return stringBuilder.ToString();
        }
    }
}
