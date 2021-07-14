using System;
using System.IO;
using System.Text;

namespace CRIF_Encrypt
{
    internal class Program
    {
        private static string runCrif(string Xpath)
        {
            StringBuilder st = new StringBuilder();
            string command;
            string b = "\"";
            command = "/C gpg.exe -v -se  -r CRIF-SWO-PROD  --passphrase \"\"";
            st.Append(command);
            st.Append(" ");
            st.Append(b);
            st.Append(Xpath);
            st.Append(b);
            return st.ToString();
        }

        private static void Main(string[] args)
        {
            if (args.Length == 0)
                return; // return if no file was dragged onto exe
            string text = File.ReadAllText(args[0]);
            text = text.Replace("~", "~\r\n");
            string path = Path.GetDirectoryName(args[0])
               + Path.DirectorySeparatorChar
               + Path.GetFileNameWithoutExtension(args[0])
               + Path.GetExtension(args[0]);
            File.WriteAllText(path, text);
            Console.WriteLine("(c) Hayk Jomardyan. All rights reserved. \n");
            Console.WriteLine("... \n " + "Selected file: " + path + "\n");
            Console.WriteLine("Command: " + runCrif(path) + "\n");

            //System.Diagnostics.Process.Start("CMD.exe", runCrif(path));

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = runCrif(path);
            process.StartInfo = startInfo;
            process.Start();

            Console.WriteLine("Finish");

            var x = Console.ReadLine();
        }
    }
}