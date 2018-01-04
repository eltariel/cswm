using System;
using System.Windows.Forms;
using NLog.Windows.Forms;

namespace cswm
{
    public partial class LogForm : Form
    {
        public LogForm()
        {
            InitializeComponent();
            RichTextBoxTarget.ReInitializeAllTextboxes(this);
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}