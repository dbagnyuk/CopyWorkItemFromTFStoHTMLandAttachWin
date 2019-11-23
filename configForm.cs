using System;
using System.Windows.Forms;

namespace CopyWorkItemFromTFStoHTMLandAttachWin
{
    public partial class configForm : Form
    {
        public configForm()
        {
            InitializeComponent();
        }
        private Form _frm;
        public configForm(mainForm frm)
        {
            _frm = frm;
            InitializeComponent();
        }
        private void configForm_Load(object sender, EventArgs e)
        {
            if (Config.config != null)
            {
                textBoxLogin.Text = Config.config[0];
                Config.config[1] = null;
                textBoxPath.Text = Config.config[2];
            }
        }

        private void textBoxLogin_Click(object sender, EventArgs e)
        {
            textBoxLogin.Clear();
        }

        private void textBoxPass_Click(object sender, EventArgs e)
        {
            textBoxPass.Clear();
        }

        private void textBoxPath_Click(object sender, EventArgs e)
        {
            textBoxPath.Clear();
        }

        private void textBoxPath_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBoxConfig_Enter(object sender, EventArgs e)
        {

        }

        private void folderBrowserDialog_HelpRequest(object sender, EventArgs e)
        {

        }

        private void buttonDirChoose_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                textBoxPath.Text = folderBrowserDialog.SelectedPath;
        }

        private void buttonConfigSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBoxPass.Text))
                MessageBox.Show("You need to enter the password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                Config.tempDomainName = textBoxLogin.Text;
                Config.tempPassword = textBoxPass.Text;
                Config.tempPathToTasks = textBoxPath.Text;

                this.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;
                Config.editConfigFile(Program.configFile);
                Config.readConfigFile(Program.configFile, ref Config.config);
                Cursor.Current = Cursors.Default;
                this.Enabled = true;

                _frm.Enabled = true;
                Close();
            }
        }

        private void configForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
                _frm.Enabled = true;
        }
    }
}
