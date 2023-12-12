namespace SPPR
{
    partial class RandomForestRegress
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
            button1 = new Button();
            label1 = new Label();
            numericUpDown1 = new NumericUpDown();
            button2 = new Button();
            label3 = new Label();
            label4 = new Label();
            button3 = new Button();
            label8 = new Label();
            button5 = new Button();
            label9 = new Label();
            numericUpDown4 = new NumericUpDown();
            label10 = new Label();
            numericUpDown5 = new NumericUpDown();
            label13 = new Label();
            panel3 = new Panel();
            label2 = new Label();
            trackBar1 = new TrackBar();
            radioButton6 = new RadioButton();
            radioButton5 = new RadioButton();
            plotView1 = new OxyPlot.WindowsForms.PlotView();
            panel4 = new Panel();
            label14 = new Label();
            trackBar2 = new TrackBar();
            radioButton7 = new RadioButton();
            radioButton8 = new RadioButton();
            numericUpDown8 = new NumericUpDown();
            plotView2 = new OxyPlot.WindowsForms.PlotView();
            button6 = new Button();
            label5 = new Label();
            numericUpDown2 = new NumericUpDown();
            label6 = new Label();
            numericUpDown3 = new NumericUpDown();
            checkBox1 = new CheckBox();
            plotView4 = new OxyPlot.WindowsForms.PlotView();
            plotView3 = new OxyPlot.WindowsForms.PlotView();
            numericUpDown6 = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown5).BeginInit();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown6).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Enabled = false;
            button1.Location = new Point(10, 150);
            button1.Name = "button1";
            button1.Size = new Size(111, 33);
            button1.TabIndex = 0;
            button1.Text = "Начать";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.Red;
            label1.Location = new Point(127, 159);
            label1.Name = "label1";
            label1.Size = new Size(151, 15);
            label1.TabIndex = 1;
            label1.Text = "Не все модули загружены";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Enabled = false;
            numericUpDown1.Location = new Point(111, 72);
            numericUpDown1.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numericUpDown1.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(169, 23);
            numericUpDown1.TabIndex = 17;
            numericUpDown1.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // button2
            // 
            button2.Enabled = false;
            button2.Location = new Point(10, 346);
            button2.Name = "button2";
            button2.Size = new Size(111, 33);
            button2.TabIndex = 19;
            button2.Text = "Сформировать";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(10, 201);
            label3.Name = "label3";
            label3.Size = new Size(165, 15);
            label3.TabIndex = 21;
            label3.Text = "Шаг1 - Обучающая выборка";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(19, 907);
            label4.Name = "label4";
            label4.Size = new Size(165, 15);
            label4.TabIndex = 22;
            label4.Text = "Шаг2 - Обучающая выборка";
            // 
            // button3
            // 
            button3.Enabled = false;
            button3.Location = new Point(16, 1039);
            button3.Name = "button3";
            button3.Size = new Size(111, 33);
            button3.TabIndex = 25;
            button3.Text = "Сформиорвать";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(13, 1613);
            label8.Name = "label8";
            label8.Size = new Size(189, 15);
            label8.TabIndex = 35;
            label8.Text = "Шаг3 - Обучение и тестирование";
            // 
            // button5
            // 
            button5.Enabled = false;
            button5.Location = new Point(13, 1854);
            button5.Name = "button5";
            button5.Size = new Size(111, 33);
            button5.TabIndex = 34;
            button5.Text = "Сформировать";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(13, 1629);
            label9.Name = "label9";
            label9.Size = new Size(178, 15);
            label9.TabIndex = 33;
            label9.Text = "Максимальная глубина дерева";
            // 
            // numericUpDown4
            // 
            numericUpDown4.Location = new Point(13, 1647);
            numericUpDown4.Name = "numericUpDown4";
            numericUpDown4.Size = new Size(169, 23);
            numericUpDown4.TabIndex = 32;
            numericUpDown4.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(13, 1678);
            label10.Name = "label10";
            label10.Size = new Size(127, 15);
            label10.TabIndex = 37;
            label10.Text = "Величина разделения";
            // 
            // numericUpDown5
            // 
            numericUpDown5.Location = new Point(13, 1696);
            numericUpDown5.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDown5.Name = "numericUpDown5";
            numericUpDown5.Size = new Size(169, 23);
            numericUpDown5.TabIndex = 36;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(13, 1919);
            label13.Name = "label13";
            label13.Size = new Size(25, 15);
            label13.TabIndex = 46;
            label13.Text = "123";
            // 
            // panel3
            // 
            panel3.Controls.Add(label2);
            panel3.Controls.Add(trackBar1);
            panel3.Controls.Add(radioButton6);
            panel3.Controls.Add(radioButton5);
            panel3.Controls.Add(numericUpDown1);
            panel3.Location = new Point(13, 219);
            panel3.Name = "panel3";
            panel3.Size = new Size(736, 108);
            panel3.TabIndex = 47;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(698, 18);
            label2.Name = "label2";
            label2.Size = new Size(29, 15);
            label2.TabIndex = 51;
            label2.Text = "70%";
            // 
            // trackBar1
            // 
            trackBar1.Location = new Point(114, 14);
            trackBar1.Maximum = 100;
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(584, 45);
            trackBar1.TabIndex = 50;
            trackBar1.Value = 70;
            trackBar1.Scroll += trackBar1_Scroll;
            // 
            // radioButton6
            // 
            radioButton6.AutoSize = true;
            radioButton6.Location = new Point(3, 72);
            radioButton6.Name = "radioButton6";
            radioButton6.Size = new Size(102, 19);
            radioButton6.TabIndex = 49;
            radioButton6.Text = "Точное число";
            radioButton6.UseVisualStyleBackColor = true;
            radioButton6.CheckedChanged += radioButton6_CheckedChanged;
            // 
            // radioButton5
            // 
            radioButton5.AutoSize = true;
            radioButton5.Checked = true;
            radioButton5.Location = new Point(3, 14);
            radioButton5.Name = "radioButton5";
            radioButton5.Size = new Size(73, 19);
            radioButton5.TabIndex = 48;
            radioButton5.TabStop = true;
            radioButton5.Text = "Процент";
            radioButton5.UseVisualStyleBackColor = true;
            radioButton5.CheckedChanged += radioButton5_CheckedChanged;
            // 
            // plotView1
            // 
            plotView1.Location = new Point(185, 340);
            plotView1.Name = "plotView1";
            plotView1.PanCursor = Cursors.Hand;
            plotView1.Size = new Size(564, 564);
            plotView1.TabIndex = 48;
            plotView1.Text = "plotView1";
            plotView1.ZoomHorizontalCursor = Cursors.SizeWE;
            plotView1.ZoomRectangleCursor = Cursors.SizeNWSE;
            plotView1.ZoomVerticalCursor = Cursors.SizeNS;
            // 
            // panel4
            // 
            panel4.Controls.Add(label14);
            panel4.Controls.Add(trackBar2);
            panel4.Controls.Add(radioButton7);
            panel4.Controls.Add(radioButton8);
            panel4.Controls.Add(numericUpDown8);
            panel4.Location = new Point(16, 925);
            panel4.Name = "panel4";
            panel4.Size = new Size(736, 108);
            panel4.TabIndex = 49;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(695, 18);
            label14.Name = "label14";
            label14.Size = new Size(35, 15);
            label14.TabIndex = 51;
            label14.Text = "100%";
            // 
            // trackBar2
            // 
            trackBar2.Location = new Point(114, 14);
            trackBar2.Maximum = 100;
            trackBar2.Name = "trackBar2";
            trackBar2.Size = new Size(584, 45);
            trackBar2.TabIndex = 50;
            trackBar2.Value = 100;
            trackBar2.Scroll += trackBar2_Scroll;
            // 
            // radioButton7
            // 
            radioButton7.AutoSize = true;
            radioButton7.Location = new Point(3, 72);
            radioButton7.Name = "radioButton7";
            radioButton7.Size = new Size(102, 19);
            radioButton7.TabIndex = 49;
            radioButton7.Text = "Точное число";
            radioButton7.UseVisualStyleBackColor = true;
            // 
            // radioButton8
            // 
            radioButton8.AutoSize = true;
            radioButton8.Checked = true;
            radioButton8.Location = new Point(3, 14);
            radioButton8.Name = "radioButton8";
            radioButton8.Size = new Size(73, 19);
            radioButton8.TabIndex = 48;
            radioButton8.TabStop = true;
            radioButton8.Text = "Процент";
            radioButton8.UseVisualStyleBackColor = true;
            // 
            // numericUpDown8
            // 
            numericUpDown8.Enabled = false;
            numericUpDown8.Location = new Point(111, 72);
            numericUpDown8.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numericUpDown8.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            numericUpDown8.Name = "numericUpDown8";
            numericUpDown8.Size = new Size(169, 23);
            numericUpDown8.TabIndex = 17;
            numericUpDown8.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // plotView2
            // 
            plotView2.Location = new Point(188, 1039);
            plotView2.Name = "plotView2";
            plotView2.PanCursor = Cursors.Hand;
            plotView2.Size = new Size(564, 564);
            plotView2.TabIndex = 50;
            plotView2.Text = "plotView2";
            plotView2.ZoomHorizontalCursor = Cursors.SizeWE;
            plotView2.ZoomRectangleCursor = Cursors.SizeNWSE;
            plotView2.ZoomVerticalCursor = Cursors.SizeNS;
            // 
            // button6
            // 
            button6.Location = new Point(284, 150);
            button6.Name = "button6";
            button6.Size = new Size(144, 33);
            button6.TabIndex = 51;
            button6.Text = "Перезагрузить данные";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(13, 1730);
            label5.Name = "label5";
            label5.Size = new Size(124, 15);
            label5.TabIndex = 59;
            label5.Text = "Количество деревьев";
            // 
            // numericUpDown2
            // 
            numericUpDown2.Location = new Point(13, 1748);
            numericUpDown2.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numericUpDown2.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(169, 23);
            numericUpDown2.TabIndex = 58;
            numericUpDown2.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(13, 1782);
            label6.Name = "label6";
            label6.Size = new Size(205, 15);
            label6.TabIndex = 61;
            label6.Text = "Количество случайных параметров";
            // 
            // numericUpDown3
            // 
            numericUpDown3.Location = new Point(13, 1800);
            numericUpDown3.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numericUpDown3.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown3.Name = "numericUpDown3";
            numericUpDown3.Size = new Size(169, 23);
            numericUpDown3.TabIndex = 60;
            numericUpDown3.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(13, 1829);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(168, 19);
            checkBox1.TabIndex = 62;
            checkBox1.Text = "Взвешенное голосование";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // plotView4
            // 
            plotView4.Location = new Point(188, 2529);
            plotView4.Name = "plotView4";
            plotView4.PanCursor = Cursors.Hand;
            plotView4.Size = new Size(564, 564);
            plotView4.TabIndex = 64;
            plotView4.Text = "plotView4";
            plotView4.ZoomHorizontalCursor = Cursors.SizeWE;
            plotView4.ZoomRectangleCursor = Cursors.SizeNWSE;
            plotView4.ZoomVerticalCursor = Cursors.SizeNS;
            // 
            // plotView3
            // 
            plotView3.Location = new Point(188, 1919);
            plotView3.Name = "plotView3";
            plotView3.PanCursor = Cursors.Hand;
            plotView3.Size = new Size(564, 564);
            plotView3.TabIndex = 63;
            plotView3.Text = "plotView3";
            plotView3.ZoomHorizontalCursor = Cursors.SizeWE;
            plotView3.ZoomRectangleCursor = Cursors.SizeNWSE;
            plotView3.ZoomVerticalCursor = Cursors.SizeNS;
            // 
            // numericUpDown6
            // 
            numericUpDown6.Location = new Point(13, 1893);
            numericUpDown6.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDown6.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown6.Name = "numericUpDown6";
            numericUpDown6.Size = new Size(169, 23);
            numericUpDown6.TabIndex = 65;
            numericUpDown6.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown6.ValueChanged += numericUpDown6_ValueChanged;
            // 
            // RandomForestRegress
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            Controls.Add(numericUpDown6);
            Controls.Add(plotView4);
            Controls.Add(plotView3);
            Controls.Add(checkBox1);
            Controls.Add(label6);
            Controls.Add(numericUpDown3);
            Controls.Add(label5);
            Controls.Add(numericUpDown2);
            Controls.Add(button6);
            Controls.Add(plotView2);
            Controls.Add(panel4);
            Controls.Add(plotView1);
            Controls.Add(panel3);
            Controls.Add(label13);
            Controls.Add(label10);
            Controls.Add(numericUpDown5);
            Controls.Add(label8);
            Controls.Add(button5);
            Controls.Add(label9);
            Controls.Add(numericUpDown4);
            Controls.Add(button3);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(button2);
            Controls.Add(label1);
            Controls.Add(button1);
            Name = "RandomForestRegress";
            Size = new Size(795, 3178);
            Load += SetProperty_Load;
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown5).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown8).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown6).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Label label1;
        private NumericUpDown numericUpDown1;
        private Button button2;
        private Label label3;
        private Label label4;
        private Button button3;
        private Label label8;
        private Button button5;
        private Label label9;
        private NumericUpDown numericUpDown4;
        private Label label10;
        private NumericUpDown numericUpDown5;
        private Label label13;
        private Panel panel3;
        private RadioButton radioButton6;
        private RadioButton radioButton5;
        private Label label2;
        private TrackBar trackBar1;
        private OxyPlot.WindowsForms.PlotView plotView1;
        private Panel panel4;
        private Label label14;
        private TrackBar trackBar2;
        private RadioButton radioButton7;
        private RadioButton radioButton8;
        private NumericUpDown numericUpDown8;
        private OxyPlot.WindowsForms.PlotView plotView2;
        private Button button6;
        private Label label5;
        private NumericUpDown numericUpDown2;
        private Label label6;
        private NumericUpDown numericUpDown3;
        private CheckBox checkBox1;
        private OxyPlot.WindowsForms.PlotView plotView4;
        private OxyPlot.WindowsForms.PlotView plotView3;
        private NumericUpDown numericUpDown6;
    }
}
