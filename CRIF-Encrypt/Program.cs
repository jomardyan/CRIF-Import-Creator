using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace CRIF_Encrypt
{
    internal class Program
    {
        public static bool SendToFTP()
        {
            // Implement send to CRIF FTP SERVER
            return true;
        }

        private static void Main(string[] args)
        {
            Utilities.StartingPoint();
            if (args.Length == 0)
            {
                Console.WriteLine("Please drag a file onto the program or pass the file as an argument.");
                Thread.Sleep(10000);
                Environment.Exit(0);
            }

            string inputFilePath = args[0];
            Console.WriteLine("Passed Argument: " + inputFilePath);

            if (!IsValidTextFile(inputFilePath))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Input is not a .txt file. Use the exported UNICODE TEXT file from Excel. Your file is -> {0}", Path.GetExtension(inputFilePath));
                Console.ReadLine();
                return; 
            }

            string inputText = File.ReadAllText(inputFilePath);

            string inputDirectory = Path.GetDirectoryName(inputFilePath) + Path.DirectorySeparatorChar;
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(inputFilePath);
            string fileWithExtension = inputDirectory + fileNameWithoutExtension + Path.GetExtension(inputFilePath);

            File.WriteAllText(fileWithExtension, inputText);
            Utilities.ReplaceCrifAndSaveDat(fileWithExtension, inputDirectory);

            Thread.Sleep(500);

            if (!SignAndEncryptFile(inputDirectory, fileNameWithoutExtension))
            {
                Console.WriteLine("An error occurred during signing and encryption.");
                return;
            }

            Console.WriteLine("--------------SIGN AND ENCRYPT FINISHED-------------------");
            Console.WriteLine("---------All operations completed successfully------------");
            Console.WriteLine("Type any key to exit...");
            Console.ReadLine();
        }

        private static bool IsValidTextFile(string filePath)
        {
            string extension = Path.GetExtension(filePath);
            return extension.Equals(".txt", StringComparison.OrdinalIgnoreCase);
        }

        private static bool SignAndEncryptFile(string inputDirectory, string fileNameWithoutExtension)
        {
            try
            {
                Console.WriteLine("Sign and encrypt...");
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = Utilities.SignAndEncrypt(inputDirectory, fileNameWithoutExtension),
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();

                return process.ExitCode == 0;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                return false;
            }
        }
    }
}
