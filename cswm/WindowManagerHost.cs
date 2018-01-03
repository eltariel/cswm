using System;
using System.Windows.Automation;
using System.Windows.Forms;
using cswm.HotKeys;
using cswm.Windows;
using NLog;
using NLog.Windows.Forms;

namespace cswm
{
    public partial class WindowManagerHost : Form
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        private HotKeyManager hotKeyManager;
        private WindowListener windowListener;

        public WindowManagerHost()
        {
            InitializeComponent();
            RichTextBoxTarget.ReInitializeAllTextboxes(this);
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            hotKeyManager = new HotKeyManager(Handle);
            hotKeyManager.Register(KeyCombo.Parse("Win+Shift+A"), () => MessageBox.Show("Win+Shift+A"));

            windowListener = new WindowListener();
            windowListener.RegisterAutomationEvents();
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            hotKeyManager.Dispose();
            hotKeyManager = null;
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            hotKeyManager?.Handle(m);
        }
    }
}