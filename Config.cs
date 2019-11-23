using System;
using System.IO;

namespace CopyWorkItemFromTFStoHTMLandAttachWin
{
    public abstract class Config
    {
        /// <summary>
        /// Create/Edit/Read the Config File.
        /// </summary>

        // string array for read the config file
        public static string[] config = null;
        // temp variable for read value from Config Form
        public static string tempDomainName = null;
        public static string tempPassword = null;
        public static string tempPathToTasks = null;

        // read the config file to memory
        public static bool readConfigFile(string configFile, ref string[] config)
        {
            // check if the config file exist and if it more than 0 bytes
            if (!File.Exists(configFile) || new FileInfo(configFile).Length == 0)
                return false;

            // catch error of reading the config file
            try
            {
                // read config file into string array
                config = System.IO.File.ReadAllLines(configFile);
            }
            catch (Exception ex)
            {
                Program.exExit(ex);
            }
            // chek config if it has less or more than 3 string
            if (config.Length != 3)
                return false;

            // check the string from string array if it's empty or not
            foreach (var s in config)
                if (s.Length == 0)
                    return false;

            return true;
        }
        // function for create and editing the config file
        public static void editConfigFile(string ConfFile)
        {
            string[] tempConfig = null;

            FileStream fileStream = null;
            StreamWriter streamWriter = null;

            // check if config exists or biger than 0 byes
            if (File.Exists(ConfFile) && new FileInfo(ConfFile).Length != 0)
            {
                try
                {
                    tempConfig = System.IO.File.ReadAllLines(ConfFile);
                    fileStream = new FileStream(ConfFile, FileMode.Truncate);
                }
                catch (Exception ex)
                {
                    Program.exExit(ex);
                }
            }
            else
            {
                try
                {
                    File.Delete(ConfFile);
                    fileStream = new FileStream(ConfFile, FileMode.CreateNew);
                }
                catch (Exception ex)
                {
                    Program.exExit(ex);
                }
            }
            streamWriter = new StreamWriter(fileStream);

            if (tempDomainName.CompareTo("") == 0)
                streamWriter.WriteLine(tempConfig[0].ToLower());
            else
                streamWriter.WriteLine(tempDomainName.ToLower());

            // encrypt the password for writing into config file
            streamWriter.WriteLine(Cipher.Encrypt(tempPassword, Program.Key));

            if (tempPathToTasks.CompareTo("") == 0)
                streamWriter.WriteLine(tempConfig[2].ToLower());
            else
                streamWriter.WriteLine(tempPathToTasks.ToLower());

            streamWriter.Close();
            fileStream.Close();
        }
    }
}
