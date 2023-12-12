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
            neroToolStripMenuItem1 = new ToolStripMenuItem();
            constantToolStripMenuItem = new ToolStripMenuItem();
            constantByGroupToolStripMenuItem = new ToolStripMenuItem();
            oneParamModelToolStripMenuItem = new ToolStripMenuItem();
            linarRegressToolStripMenuItem = new ToolStripMenuItem();
            polyRegressToolStripMenuItem = new ToolStripMenuItem();
            treeToolStripMenuItem = new ToolStripMenuItem();
            randomForestToolStripMenuItem = new ToolStripMenuItem();
            simpleNeroToolStripMenuItem = new ToolStripMenuItem();
            simpleClassificationToolStripMenuItem = new ToolStripMenuItem();
            neroToolStripMenuItem = new ToolStripMenuItem();
            treeToolStripMenuItem1 = new ToolStripMenuItem();
            forestToolStripMenuItem1 = new ToolStripMenuItem();
            simpleNeroToolStripMenuItem1 = new ToolStripMenuItem();
            imageNeroToolStripMenuItem = new ToolStripMenuItem();
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
            setsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { neroToolStripMenuItem1, constantToolStripMenuItem, constantByGroupToolStripMenuItem, oneParamModelToolStripMenuItem, linarRegressToolStripMenuItem, polyRegressToolStripMenuItem, treeToolStripMenuItem, randomForestToolStripMenuItem, simpleNeroToolStripMenuItem });
            setsToolStripMenuItem.Name = "setsToolStripMenuItem";
            setsToolStripMenuItem.Size = new Size(59, 20);
            setsToolStripMenuItem.Text = "Regress";
            // 
            // neroToolStripMenuItem1
            // 
            neroToolStripMenuItem1.Name = "neroToolStripMenuItem1";
            neroToolStripMenuItem1.Size = new Size(168, 22);
            neroToolStripMenuItem1.Text = "Nero";
            neroToolStripMenuItem1.Click += neroToolStripMenuItem1_Click;
            // 
            // constantToolStripMenuItem
            // 
            constantToolStripMenuItem.Name = "constantToolStripMenuItem";
            constantToolStripMenuItem.Size = new Size(168, 22);
            constantToolStripMenuItem.Text = "Constant";
            constantToolStripMenuItem.Click += constantToolStripMenuItem_Click;
            // 
            // constantByGroupToolStripMenuItem
            // 
            constantByGroupToolStripMenuItem.Name = "constantByGroupToolStripMenuItem";
            constantByGroupToolStripMenuItem.Size = new Size(168, 22);
            constantByGroupToolStripMenuItem.Text = "ConstantByGroup";
            constantByGroupToolStripMenuItem.Click += constantByGroupToolStripMenuItem_Click;
            // 
            // oneParamModelToolStripMenuItem
            // 
            oneParamModelToolStripMenuItem.Name = "oneParamModelToolStripMenuItem";
            oneParamModelToolStripMenuItem.Size = new Size(168, 22);
            oneParamModelToolStripMenuItem.Text = "OneParamModel";
            oneParamModelToolStripMenuItem.Click += oneParamModelToolStripMenuItem_Click;
            // 
            // linarRegressToolStripMenuItem
            // 
            linarRegressToolStripMenuItem.Name = "linarRegressToolStripMenuItem";
            linarRegressToolStripMenuItem.Size = new Size(168, 22);
            linarRegressToolStripMenuItem.Text = "LinarRegress";
            linarRegressToolStripMenuItem.Click += linarRegressToolStripMenuItem_Click;
            // 
            // polyRegressToolStripMenuItem
            // 
            polyRegressToolStripMenuItem.Name = "polyRegressToolStripMenuItem";
            polyRegressToolStripMenuItem.Size = new Size(168, 22);
            polyRegressToolStripMenuItem.Text = "PolyRegress";
            polyRegressToolStripMenuItem.Click += polyRegressToolStripMenuItem_Click;
            // 
            // treeToolStripMenuItem
            // 
            treeToolStripMenuItem.Name = "treeToolStripMenuItem";
            treeToolStripMenuItem.Size = new Size(168, 22);
            treeToolStripMenuItem.Text = "Tree";
            treeToolStripMenuItem.Click += treeToolStripMenuItem_Click;
            // 
            // randomForestToolStripMenuItem
            // 
            randomForestToolStripMenuItem.Name = "randomForestToolStripMenuItem";
            randomForestToolStripMenuItem.Size = new Size(168, 22);
            randomForestToolStripMenuItem.Text = "RandomForest";
            randomForestToolStripMenuItem.Click += randomForestToolStripMenuItem_Click;
            // 
            // simpleNeroToolStripMenuItem
            // 
            simpleNeroToolStripMenuItem.Name = "simpleNeroToolStripMenuItem";
            simpleNeroToolStripMenuItem.Size = new Size(168, 22);
            simpleNeroToolStripMenuItem.Text = "SimpleNero";
            simpleNeroToolStripMenuItem.Click += simpleNeroToolStripMenuItem_Click;
            // 
            // simpleClassificationToolStripMenuItem
            // 
            simpleClassificationToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { neroToolStripMenuItem, treeToolStripMenuItem1, forestToolStripMenuItem1, simpleNeroToolStripMenuItem1, imageNeroToolStripMenuItem });
            simpleClassificationToolStripMenuItem.Name = "simpleClassificationToolStripMenuItem";
            simpleClassificationToolStripMenuItem.Size = new Size(89, 20);
            simpleClassificationToolStripMenuItem.Text = "Classification";
            // 
            // neroToolStripMenuItem
            // 
            neroToolStripMenuItem.Name = "neroToolStripMenuItem";
            neroToolStripMenuItem.Size = new Size(180, 22);
            neroToolStripMenuItem.Text = "Nero";
            neroToolStripMenuItem.Click += neroToolStripMenuItem_Click;
            // 
            // treeToolStripMenuItem1
            // 
            treeToolStripMenuItem1.Name = "treeToolStripMenuItem1";
            treeToolStripMenuItem1.Size = new Size(180, 22);
            treeToolStripMenuItem1.Text = "Tree";
            treeToolStripMenuItem1.Click += treeToolStripMenuItem1_Click;
            // 
            // forestToolStripMenuItem1
            // 
            forestToolStripMenuItem1.Name = "forestToolStripMenuItem1";
            forestToolStripMenuItem1.Size = new Size(180, 22);
            forestToolStripMenuItem1.Text = "Forest";
            forestToolStripMenuItem1.Click += forestToolStripMenuItem1_Click;
            // 
            // simpleNeroToolStripMenuItem1
            // 
            simpleNeroToolStripMenuItem1.Name = "simpleNeroToolStripMenuItem1";
            simpleNeroToolStripMenuItem1.Size = new Size(180, 22);
            simpleNeroToolStripMenuItem1.Text = "SimpleNero";
            simpleNeroToolStripMenuItem1.Click += simpleNeroToolStripMenuItem1_Click;
            // 
            // imageNeroToolStripMenuItem
            // 
            imageNeroToolStripMenuItem.Name = "imageNeroToolStripMenuItem";
            imageNeroToolStripMenuItem.Size = new Size(180, 22);
            imageNeroToolStripMenuItem.Text = "ImageNero";
            imageNeroToolStripMenuItem.Click += imageNeroToolStripMenuItem_Click;
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
        private ToolStripMenuItem neroToolStripMenuItem;
        private ToolStripMenuItem treeToolStripMenuItem1;
        private ToolStripMenuItem forestToolStripMenuItem1;
        private ToolStripMenuItem neroToolStripMenuItem1;
        private ToolStripMenuItem constantToolStripMenuItem;
        private ToolStripMenuItem constantByGroupToolStripMenuItem;
        private ToolStripMenuItem oneParamModelToolStripMenuItem;
        private ToolStripMenuItem linarRegressToolStripMenuItem;
        private ToolStripMenuItem polyRegressToolStripMenuItem;
        private ToolStripMenuItem treeToolStripMenuItem;
        private ToolStripMenuItem randomForestToolStripMenuItem;
        private ToolStripMenuItem simpleNeroToolStripMenuItem;
        private ToolStripMenuItem simpleNeroToolStripMenuItem1;
        private ToolStripMenuItem imageNeroToolStripMenuItem;
    }
}