using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using cswm.HotKeys;
using cswm.Windows;

namespace cswm
{
    public class CswmApp : ApplicationContext
    {
        private readonly NotifyIcon notifyIcon;
        private readonly HotKeyManager keys;
        private readonly WindowListener windowListener;
        private LogForm logForm;

        public CswmApp()
        {
            var cm = new ContextMenuStrip();
            cm.Items.Add("&Settings...", null, (s, e) => MessageBox.Show("TODO"));
            cm.Items.Add("Show &Log Window...", null, ToggleLogWindow);
            cm.Items.Add("-");
            cm.Items.Add("&About...", null, (s, e) => MessageBox.Show("TODO"));
            cm.Items.Add("-");
            cm.Items.Add("E&xit", null, (s, e) => Application.Exit());

            notifyIcon = new NotifyIcon
            {
                Visible = true,
                Text = "cswm",
                Icon = SystemIcons.Application,
                ContextMenuStrip = cm,
            };

            Application.ApplicationExit += OnApplicationExit;

            keys = new HotKeyManager(GetHandle(notifyIcon));
            keys.Register(KeyCombo.Parse("Win+Shift+A"), () => MessageBox.Show("Win+Shift+A"));
            keys.Register(KeyCombo.Parse("Ctrl+Alt+Left"), () => MessageBox.Show("Ctrl+Alt+Left"));
            keys.Register(KeyCombo.Parse("Win+Shift+B"), () => MessageBox.Show("Win+Shift+B"));
            Application.AddMessageFilter(keys);

            windowListener = new WindowListener();
            windowListener.RegisterAutomationEvents();
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            keys?.Dispose();
        }

        private void ToggleLogWindow(object sender, EventArgs e)
        {
            if (logForm == null)
            {
                logForm = new LogForm();
                logForm.VisibleChanged += (o, args) =>
                {
                    if (o is ToolStripMenuItem menuItem)
                    {
                        menuItem.Checked = logForm.Visible;
                    }
                };
                logForm.Show();
            }
            else
            {
                logForm.Visible = !logForm.Visible;
            }
        }

        private static IntPtr GetHandle(NotifyIcon icon)
        {
            var f = typeof(NotifyIcon).GetField("window", BindingFlags.NonPublic | BindingFlags.Instance);
            return (f?.GetValue(icon) as NativeWindow)?.Handle ?? default;
        }
    }
}