using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace CRIF_Encrypt
{
    internal class Program
    {


        private static void Main(string[] args)
        {
            if (args.Length == 0)
                return; // return if no file was dragged onto exe
            string text = File.ReadAllText(args[0]);
            text = text.Replace("~", "~\r\n");
            

            //FOR ZIP PURPOSE
            string dir = Path.GetDirectoryName(args[0])
               + Path.DirectorySeparatorChar;
            string filename = Path.GetFileNameWithoutExtension(args[0]);
            //FOR ZIP PURPOSE END 
            
            string path = Path.GetDirectoryName(args[0])
               + Path.DirectorySeparatorChar
               + Path.GetFileNameWithoutExtension(args[0])
               + Path.GetExtension(args[0]);
            File.WriteAllText(path, text);
            Console.WriteLine("(c) Hayk Jomardyan 2021. All rights reserved.\n");
            Console.WriteLine("     ... \n " + "Selected file: " + path + "\n");
            Console.WriteLine("     ... \n " + "Selected dir: " + dir + "\n");
            Console.WriteLine("Command: " + SignAndEncrypt(dir, filename) + "\n");

            //simplified:: System.Diagnostics.Process.Start("CMD.exe", EncryptCommand(path));



            //It's time so save the file into fileserver 

            if (Zipper(dir, filename))
            {
                Thread.Sleep(8000);
                try
                {
                    Console.WriteLine("Sign end encrypt...");
                    Process process = new Process();
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = "cmd.exe";
                    startInfo.Arguments = SignAndEncrypt(dir, filename);
                    process.StartInfo = startInfo;
                    process.Start();


                }
                catch (Exception e)
                {

                    Console.WriteLine("Error: {0}", e.Message);
                }
            }
            else
            {
                Console.WriteLine("Unable to zip");

            }


            Console.WriteLine("Operation completed... ");
            var x = Console.ReadLine();
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

        static bool Zipper(string ImputDir, string FileName)
        {
            //Long codding in order to be readable. 
            StringBuilder cm = new StringBuilder();
            cm.Append("a ");
            cm.Append("\"" + ImputDir + FileName + ".zip" + "\" ");
            cm.Append("\"" + ImputDir + FileName + ".dat" + "\"");

            Console.WriteLine("----------");
            Console.WriteLine(cm.ToString());
            Console.WriteLine("----------");
            try
            {
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "C:\\Program Files\\7-Zip\\7z.exe";
                startInfo.Arguments = cm.ToString();
                process.StartInfo = startInfo;
                process.Start();
                return true;
            }
            catch (Exception e)
            {

                Console.WriteLine("Error: {0}", e.Message);
                return false;
            }


        }


    }
}