namespace SimpleFuzzy
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            setsToolStripMenuItem = new ToolStripMenuItem();
            simpleClassificationToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { setsToolStripMenuItem, simpleClassificationToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // setsToolStripMenuItem
            // 
            setsToolStripMenuItem.Name = "setsToolStripMenuItem";
            setsToolStripMenuItem.Size = new Size(95, 20);
            setsToolStripMenuItem.Text = "SimpleRegress";
            setsToolStripMenuItem.Click += setsToolStripMenuItem_Click;
            // 
            // simpleClassificationToolStripMenuItem
            // 
            simpleClassificationToolStripMenuItem.Name = "simpleClassificationToolStripMenuItem";
            simpleClassificationToolStripMenuItem.Size = new Size(125, 20);
            simpleClassificationToolStripMenuItem.Text = "SimpleClassification";
            simpleClassificationToolStripMenuItem.Click += simpleClassificationToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(800, 473);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem setsToolStripMenuItem;
        private ToolStripMenuItem simpleClassificationToolStripMenuItem;
    }
}