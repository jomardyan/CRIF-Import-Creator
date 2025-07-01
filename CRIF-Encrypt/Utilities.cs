using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;

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
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"(c) {Configuration.Author}. All rights reserved.  {Configuration.ApplicationVersion} \n");
            Console.WriteLine($"{Configuration.Repository} \n");

            Console.ResetColor();
            Console.WriteLine("Initializing...\n");
        }

        internal static string GetGpgArguments(string inputDir, string fileName)
        {
            string fileBaseName = Path.GetFileNameWithoutExtension(fileName);
            string zipPath = Path.Combine(inputDir, fileBaseName, $"{fileName}.zip");
            return $"-v -se -r {Configuration.GpgRecipient} --passphrase \"{Configuration.GpgPassphrase}\" --local-user {Configuration.GpgLocalUser} \"{zipPath}\"";
        }

        internal static string CreateDatDir(string path, string fileName)
        {
            if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("Path and fileName cannot be null or empty");
            }

            string fullPath = Path.Combine(path, fileName, fileName);
            Console.WriteLine("Creating data directory...");
            
            try
            {
                if (Directory.Exists(fullPath))
                {
                    Console.WriteLine($"Directory already exists: {fullPath}");
                    // Clean existing directory to ensure fresh start
                    Directory.Delete(fullPath, true);
                    Directory.CreateDirectory(fullPath);
                    Console.WriteLine("Existing directory cleaned and recreated.");
                }
                else
                {
                    Directory.CreateDirectory(fullPath);
                    Console.WriteLine($"Directory created successfully at {Directory.GetCreationTime(fullPath)}. Path: {fullPath}");
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new UnauthorizedAccessException($"Access denied creating directory: {fullPath}", ex);
            }
            catch (IOException ex)
            {
                throw new IOException($"I/O error creating directory: {fullPath}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Unexpected error creating directory: {fullPath} - {ex.Message}", ex);
            }
            
            return fullPath;
        }

        internal static async Task ReplaceCrifAndSaveDatAsync(string fileName, string directory)
        {
            try
            {
                string fileBaseName = Path.GetFileNameWithoutExtension(fileName);
                string text = await File.ReadAllTextAsync(fileName);
                text = text.Replace("\t", Configuration.TabReplacement);

                text = RemoveFirstAndLastLines(text);

                string datDir = CreateDatDir(directory, fileBaseName);

                string datOutput = Path.Combine(datDir, $"{fileBaseName}.dat");
                await File.WriteAllTextAsync(datOutput, text);
                Console.WriteLine($"Saved: {datOutput}");

                string zipPath = Path.Combine(directory, fileBaseName, $"{fileBaseName}.zip");
                Console.WriteLine($"Creating ZIP: {zipPath}");
                
                // Create zip asynchronously
                await Task.Run(() => ZipFile.CreateFromDirectory(datDir, zipPath));

                // Clean up temporary directory
                Directory.Delete(datDir, true);
                Console.WriteLine("Temporary files cleaned up.");
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error processing file: {e.Message}");
                Console.ResetColor();
                throw;
            }
        }

        private static string RemoveFirstAndLastLines(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            string[] lines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            
            if (lines.Length <= 2)
            {
                return string.Empty;
            }

            // Use array slicing for better performance
            var middleLines = new string[lines.Length - 2];
            Array.Copy(lines, 1, middleLines, 0, lines.Length - 2);
            
            return string.Join(Environment.NewLine, middleLines);
        }
    }
}
