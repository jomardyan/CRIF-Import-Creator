using System;
using System.IO;

namespace CRIF_Encrypt
{
    internal static class Configuration
    {
        // GPG Configuration
        internal static readonly string GpgExecutable = "gpg.exe";
        internal static readonly string GpgRecipient = "CRIF-SWO-PROD";
        internal static readonly string GpgLocalUser = "0x9F674BC8";
        internal static readonly string GpgPassphrase = "";

        // File Configuration
        internal static readonly string[] AllowedExtensions = { ".txt" };
        internal static readonly string TabReplacement = "^~";

        // Application Configuration
        internal static readonly string ApplicationVersion = "v17";
        internal static readonly string Author = "Hayk Jomardyan 2022";
        internal static readonly string Repository = "https://github.com/jomardyan/CRIF-Import-Creator";

        /// <summary>
        /// Validates if the file extension is allowed
        /// </summary>
        /// <param name="filePath">Path to the file to validate</param>
        /// <returns>True if the file extension is allowed</returns>
        internal static bool IsAllowedFileExtension(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return false;

            string extension = Path.GetExtension(filePath);
            foreach (string allowedExt in AllowedExtensions)
            {
                if (extension.Equals(allowedExt, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }
    }
}
