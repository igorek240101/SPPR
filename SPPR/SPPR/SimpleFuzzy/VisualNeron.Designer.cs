namespace SPPR
{
    partial class VisualNeron
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            numericUpDown = new NumericUpDown[5];
            numericUpDown[0] = new NumericUpDown();
            numericUpDown[1] = new NumericUpDown();
            numericUpDown[2] = new NumericUpDown();
            numericUpDown[3] = new NumericUpDown();
            numericUpDown[4] = new NumericUpDown();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)numericUpDown[0]).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown[1]).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown[2]).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown[3]).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown[4]).BeginInit();
            SuspendLayout();
            // 
            // numericUpDown1
            // 
            numericUpDown[0].DecimalPlaces = 4;
            numericUpDown[0].Increment = new decimal(new int[] { 1, 0, 0, 262144 });
            numericUpDown[0].Location = new Point(2, 3);
            numericUpDown[0].Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDown[0].Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            numericUpDown[0].Name = "numericUpDown1";
            numericUpDown[0].Size = new Size(58, 23);
            numericUpDown[0].TabIndex = 0;
            // 
            // numericUpDown2
            // 
            numericUpDown[1].DecimalPlaces = 4;
            numericUpDown[1].Increment = new decimal(new int[] { 1, 0, 0, 262144 });
            numericUpDown[1].Location = new Point(3, 32);
            numericUpDown[1].Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDown[1].Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            numericUpDown[1].Name = "numericUpDown2";
            numericUpDown[1].Size = new Size(58, 23);
            numericUpDown[1].TabIndex = 1;
            // 
            // numericUpDown3
            // 
            numericUpDown[2].DecimalPlaces = 4;
            numericUpDown[2].Increment = new decimal(new int[] { 1, 0, 0, 262144 });
            numericUpDown[2].Location = new Point(3, 90);
            numericUpDown[2].Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDown[2].Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            numericUpDown[2].Name = "numericUpDown3";
            numericUpDown[2].Size = new Size(58, 23);
            numericUpDown[2].TabIndex = 3;
            // 
            // numericUpDown4
            // 
            numericUpDown[3].DecimalPlaces = 4;
            numericUpDown[3].Increment = new decimal(new int[] { 1, 0, 0, 262144 });
            numericUpDown[3].Location = new Point(3, 61);
            numericUpDown[3].Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDown[3].Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            numericUpDown[3].Name = "numericUpDown4";
            numericUpDown[3].Size = new Size(58, 23);
            numericUpDown[3].TabIndex = 2;
            // 
            // numericUpDown5
            // 
            numericUpDown[4].DecimalPlaces = 4;
            numericUpDown[4].Increment = new decimal(new int[] { 1, 0, 0, 262144 });
            numericUpDown[4].Location = new Point(3, 119);
            numericUpDown[4].Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDown[4].Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            numericUpDown[4].Name = "numericUpDown5";
            numericUpDown[4].Size = new Size(58, 23);
            numericUpDown[4].TabIndex = 4;
            // 
            // button1
            // 
            button1.ForeColor = SystemColors.ButtonFace;
            button1.Location = new Point(67, 3);
            button1.Name = "button1";
            button1.Size = new Size(130, 139);
            button1.TabIndex = 5;
            button1.UseVisualStyleBackColor = true;
            // 
            // VisualNeron
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveBorder;
            Controls.Add(button1);
            Controls.Add(numericUpDown[4]);
            Controls.Add(numericUpDown[3]);
            Controls.Add(numericUpDown[2]);
            Controls.Add(numericUpDown[1]);
            Controls.Add(numericUpDown[0]);
            Name = "VisualNeron";
            Size = new Size(200, 150);
            ((System.ComponentModel.ISupportInitialize)numericUpDown[0]).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown[1]).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown[2]).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown[3]).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown[4]).EndInit();
            ResumeLayout(false);
        }

        #endregion

        public NumericUpDown[] numericUpDown;
        public Button button1;
    }
}
