using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.IO.Compression;

namespace CRIF_Encrypt
{

    internal class Program
    {

        private static void Main(string[] args)
        {
            StartingPoint();
            if (args.Length == 0)
                return; // exit if no file was dragged onto program
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
            ReplaceCrifAndSaveDat(FileWithExtansion, ImputDirectory);

            //It's time so save the file into fileserver
            Thread.Sleep(5000);
            try
            {
                Console.WriteLine("Sign end encrypt...");
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = SignAndEncrypt(ImputDirectory, FileName);
                process.StartInfo = startInfo;
                process.Start();


            }
            catch (Exception e)
            {

                Console.WriteLine("Error: {0}", e.Message);
            }




            Console.WriteLine("Operation completed... ");
            var x = Console.ReadLine();
        }

        private static void StartingPoint()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("(c) Hayk Jomardyan 2021. All rights reserved.\n");
            Console.ResetColor();
            Console.WriteLine("Please wait... \n");
            Thread.Sleep(500);
        }

        static string SignAndEncrypt(string ImputDir, string FileName)
        {
            //Long codding in order to be readable.
            StringBuilder st = new StringBuilder();
            string command;
            string b = "\"";
            command = "/C gpg.exe -v -se  -r CRIF-SWO-PROD  --passphrase \"\"";
            st.Append(command);
            st.Append(" ");
            st.Append(b + ImputDir + FileName + ".zip" + b);
            return st.ToString();
        }
        static void ReplaceCrifAndSaveDat(string FileName, String Directory)
        {

            string y = Path.GetFileNameWithoutExtension(FileName);
            string text = File.ReadAllText(FileName);
            //text = text.ToUTF8();
            text = text.Replace("	", "~^");

            //Remove fisrt and last line from EXCEL export  TXT file. 
            int index = text.IndexOf(System.Environment.NewLine);
            var newText = text.Substring(index + System.Environment.NewLine.Length);
            newText = newText.Remove(newText.TrimEnd().LastIndexOf(Environment.NewLine));

            var datdir = CreateDatDir(Directory);
            Thread.Sleep(500);
            string DatOutput = datdir + y + ".dat";
            Console.WriteLine("Save dir: {0}", DatOutput);
            File.WriteAllText(DatOutput, newText);

            Thread.Sleep(500);
            string startPath = datdir;
            string zipPath = Directory + y + ".zip";

            ZipFile.CreateFromDirectory(startPath, zipPath);

        }
        static string CreateDatDir(String path)
        {
            Console.WriteLine("Creating datdir folder...");
            path = path + @"datdir\";
            try
            {
                // Determine whether the directory exists.
                if (Directory.Exists(path))
                {
                    Console.WriteLine("That path exists already: " + path);
                }

                else
                {
                    DirectoryInfo di = Directory.CreateDirectory(path);
                    Console.WriteLine("The directory was created successfully at {0}. Path: {1}", Directory.GetCreationTime(path), (path));

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
            return path;
        }
    }
    public static class StringExtensions
    {
        public static string ToUTF8(this string text)
        {
            return Encoding.UTF8.GetString(Encoding.Default.GetBytes(text));
        }
    }


}