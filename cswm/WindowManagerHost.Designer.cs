namespace cswm
{
    partial class WindowManagerHost
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.logTarget = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // logTarget
            // 
            this.logTarget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logTarget.Location = new System.Drawing.Point(0, 0);
            this.logTarget.Name = "logTarget";
            this.logTarget.Size = new System.Drawing.Size(1051, 178);
            this.logTarget.TabIndex = 0;
            this.logTarget.Text = "";
            // 
            // WindowManagerHost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1051, 178);
            this.Controls.Add(this.logTarget);
            this.Name = "WindowManagerHost";
            this.Text = "cswm Log Window";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox logTarget;
    }
}

