using System;
using System.Diagnostics;
using System.IO;
using System.Threading;


namespace CRIF_Encrypt
{
    internal class Program
    {
        // ToDo
        public static bool SentToFTP()
        {
            //Implement send to CRIF FTP SERVER
            return true;
        }

        private static void Main(string[] args)
        {
            if (args.Length != 0)
            {
                Console.WriteLine("Passed Argument: " + args[0]);
            }
            Utilities.StartingPoint();
            if (args.Length == 0)
            {
                Console.WriteLine("Please drag a file onto program or pass the file as an argument.");
                Thread.Sleep(10000);
                Environment.Exit(0);
            }
            string ArgumentInputText = File.ReadAllText(args[0]);

            if (Path.GetExtension(args[0]) is not ".txt" & Path.GetExtension(args[0]) is not ".TXT")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Input is not .txt file. Use the exported UNICODE TEXT file from excel::  Your file is -> {0}", Path.GetExtension(args[0]));
                Console.ReadLine();
                return; // exit if  the file is not txt.
            }

            //Directories
            string InputDirectory = Path.GetDirectoryName(args[0]) + Path.DirectorySeparatorChar;
            string FileName = Path.GetFileNameWithoutExtension(args[0]);
            string FileWithExtansion = Path.GetDirectoryName(args[0]) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(args[0]) + Path.GetExtension(args[0]);

            File.WriteAllText(FileWithExtansion, ArgumentInputText);
            Utilities.ReplaceCrifAndSaveDat(FileWithExtansion, InputDirectory);

            //It's time so save the file into fileserver
            Thread.Sleep(500);
            //Try Sign end encrypt
            try
            {
                Console.WriteLine("Sign end encrypt...");
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = Utilities.SignAndEncrypt(InputDirectory, FileName);
                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }

            Console.WriteLine("--------------SIGN AND ENCRYPT FINISEHD-------------------");
            Console.WriteLine("---------All operations completed successfully------------");
            Console.WriteLine("Type any key to exit...");
            _ = Console.ReadLine();
        }
    }
}