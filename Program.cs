using System;
using System.Windows.Forms;

namespace CopyWorkItemFromTFStoHTMLandAttachWin
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        public const string Key = "pass"; // key word for Encrypt/Decrypt

        public static string configFile = @"CopyWorkItemFromTFStoHTMLandAttachWin.exe.conf"; // config file Name
        public static bool downConfirm = false; // attachments download confirm
        public static int itemId; // TFS Item id

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new mainForm());
        }

        public static void exExit(Exception ex)
        {
            //MessageBox.Show(ex.ToString());
            MessageBox.Show(ex.ToString(), "Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            System.Environment.Exit(1);
        }
    }
}
