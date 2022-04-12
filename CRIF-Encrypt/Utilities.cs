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
            Console.WriteLine("Initializing, please  wait... \n");
			
            Thread.Sleep(100);
        }

        internal static string SignAndEncrypt(string InputDir, string FileName)
        {
            string y = Path.GetFileNameWithoutExtension(FileName);
            //Long codding in order to be readable.
            StringBuilder st = new StringBuilder();
            string command;
            string b = "\"";
            command = "/C gpg.exe -v -se  -r CRIF-SWO-PROD  --passphrase \"\"";
            st.Append(command);
            st.Append(" ");
            st.Append(b + InputDir + y + @"\" + FileName + ".zip" + b);
            return st.ToString();
        }

        internal static string CreateDatDir(String path, string FileName)
        {
            Console.WriteLine("Creating datdir folder...");
            path = path + FileName + @"\"+ FileName + @"\";
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

        internal static void ReplaceCrifAndSaveDat(string FileName, String Directory)
        {
            try
            {
                string y = Path.GetFileNameWithoutExtension(FileName);
                string text = File.ReadAllText(FileName);
                //text = text.ToUTF8();
                text = text.Replace("	", "^~");

                //Remove first and last line from EXCEL export  TXT file.
                int index = text.IndexOf(System.Environment.NewLine);
                var newText = text.Substring(index + System.Environment.NewLine.Length);
                newText = newText.Remove(newText.TrimEnd().LastIndexOf(Environment.NewLine));

                var datdir = CreateDatDir(Directory, y);
                Thread.Sleep(500);
                string DatOutput = datdir + y + ".dat";
                File.WriteAllText(DatOutput, newText);
                Console.WriteLine("Saved: {0}", DatOutput);
                Thread.Sleep(500);
                string startPath = datdir;
                string zipPath = Directory + y + @"\" + y + ".zip";
                
                Console.WriteLine("ZIP PATH:  " + zipPath);
                ZipFile.CreateFromDirectory(startPath, zipPath);
                
                System.IO.Directory.Delete(datdir,true);
            }
            catch (System.Exception e)
            {
                Console.WriteLine("Error: {0}", e.ToString());
            }
        }
    }
}
