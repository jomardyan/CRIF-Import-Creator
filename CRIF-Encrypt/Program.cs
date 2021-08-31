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
            Utilities.StartingPoint();
            if (args.Length == 0)
            {
                Console.WriteLine("Please drag a file onto program or pass the file as an argument.");
                Thread.Sleep(10000);
                Environment.Exit(0);
            }
            string ArgumentImputText = File.ReadAllText(args[0]);

            if (Path.GetExtension(args[0]) is not ".txt" & Path.GetExtension(args[0]) is not ".TXT")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Imput is not .txt file. Use the exported UNICODE TEXT file from excel::  Your file -> {0}", Path.GetExtension(args[0]));
                Console.ReadLine();
                return; // exit if  the file is not txt.
            }

            //Directories
            string ImputDirectory = Path.GetDirectoryName(args[0]) + Path.DirectorySeparatorChar;
            string FileName = Path.GetFileNameWithoutExtension(args[0]);
            string FileWithExtansion = Path.GetDirectoryName(args[0]) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(args[0]) + Path.GetExtension(args[0]);

            File.WriteAllText(FileWithExtansion, ArgumentImputText);
            Utilities.ReplaceCrifAndSaveDat(FileWithExtansion, ImputDirectory);

            //It's time so save the file into fileserver
            Thread.Sleep(3000);
            //Try Sign end encrypt
            try
            {
                Console.WriteLine("Sign end encrypt...");
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = Utilities.SignAndEncrypt(ImputDirectory, FileName);
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