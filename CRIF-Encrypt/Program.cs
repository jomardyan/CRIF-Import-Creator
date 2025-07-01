using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace CRIF_Encrypt
{
    internal class Program
    {
        public static bool SendToFTP()
        {
            // Implement send to CRIF FTP SERVER
            return true;
        }

        private static async Task Main(string[] args)
        {
            Utilities.StartingPoint();
            if (args.Length == 0)
            {
                Console.WriteLine("Please drag a file onto the program or pass the file as an argument.");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                Environment.Exit(0);
            }

            string inputFilePath = args[0];
            Console.WriteLine($"Processing file: {inputFilePath}");

            if (!IsValidTextFile(inputFilePath))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Input is not a .txt file. Use the exported UNICODE TEXT file from Excel. Your file extension is: {Path.GetExtension(inputFilePath)}");
                Console.ResetColor();
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                return; 
            }

            try
            {
                string inputText = await File.ReadAllTextAsync(inputFilePath);
                string inputDirectory = Path.GetDirectoryName(inputFilePath) + Path.DirectorySeparatorChar;
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(inputFilePath);

                // Process the file directly without unnecessary write
                await Utilities.ReplaceCrifAndSaveDatAsync(inputFilePath, inputDirectory);

                if (!await SignAndEncryptFileAsync(inputDirectory, fileNameWithoutExtension))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("An error occurred during signing and encryption.");
                    Console.ResetColor();
                    return;
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("--------------SIGN AND ENCRYPT FINISHED-------------------");
                Console.WriteLine("---------All operations completed successfully------------");
                Console.ResetColor();
            }
            catch (FileNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"File not found: {inputFilePath}");
                Console.ResetColor();
            }
            catch (UnauthorizedAccessException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Access denied to file: {inputFilePath}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                Console.ResetColor();
            }
            
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static bool IsValidTextFile(string filePath)
        {
            return Configuration.IsAllowedFileExtension(filePath);
        }

        private static async Task<bool> SignAndEncryptFileAsync(string inputDirectory, string fileNameWithoutExtension)
        {
            try
            {
                Console.WriteLine("Signing and encrypting file...");
                
                var startInfo = new ProcessStartInfo
                {
                    FileName = Configuration.GpgExecutable,
                    Arguments = Utilities.GetGpgArguments(inputDirectory, fileNameWithoutExtension),
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using var process = new Process { StartInfo = startInfo };
                process.Start();
                
                // Read output and error streams to prevent deadlock
                var outputTask = process.StandardOutput.ReadToEndAsync();
                var errorTask = process.StandardError.ReadToEndAsync();
                
                await process.WaitForExitAsync();
                
                var output = await outputTask;
                var error = await errorTask;

                if (process.ExitCode != 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"GPG process failed with exit code: {process.ExitCode}");
                    if (!string.IsNullOrEmpty(error))
                    {
                        Console.WriteLine($"Error output: {error}");
                    }
                    Console.ResetColor();
                    return false;
                }

                if (!string.IsNullOrEmpty(output))
                {
                    Console.WriteLine($"GPG output: {output}");
                }

                return true;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error during signing and encryption: {e.Message}");
                Console.ResetColor();
                return false;
            }
        }
    }
}
