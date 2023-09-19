namespace SimpleFuzzy
{
    partial class SimpleRegress
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
            plotView1 = new OxyPlot.WindowsForms.PlotView();
            numericUpDown1 = new NumericUpDown();
            label2 = new Label();
            button2 = new Button();
            plotView2 = new OxyPlot.WindowsForms.PlotView();
            label3 = new Label();
            label4 = new Label();
            button3 = new Button();
            label5 = new Label();
            numericUpDown2 = new NumericUpDown();
            plotView3 = new OxyPlot.WindowsForms.PlotView();
            label6 = new Label();
            button4 = new Button();
            label7 = new Label();
            numericUpDown3 = new NumericUpDown();
            plotView4 = new OxyPlot.WindowsForms.PlotView();
            label8 = new Label();
            button5 = new Button();
            label9 = new Label();
            numericUpDown4 = new NumericUpDown();
            label10 = new Label();
            numericUpDown5 = new NumericUpDown();
            label11 = new Label();
            numericUpDown6 = new NumericUpDown();
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            panel1 = new Panel();
            panel2 = new Panel();
            radioButton3 = new RadioButton();
            radioButton4 = new RadioButton();
            label12 = new Label();
            numericUpDown7 = new NumericUpDown();
            label13 = new Label();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown6).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown7).BeginInit();
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
            // plotView1
            // 
            plotView1.Location = new Point(205, 192);
            plotView1.Name = "plotView1";
            plotView1.PanCursor = Cursors.Hand;
            plotView1.Size = new Size(564, 286);
            plotView1.TabIndex = 16;
            plotView1.Text = "plotView1";
            plotView1.ZoomHorizontalCursor = Cursors.SizeWE;
            plotView1.ZoomRectangleCursor = Cursors.SizeNWSE;
            plotView1.ZoomVerticalCursor = Cursors.SizeNS;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(10, 240);
            numericUpDown1.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numericUpDown1.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(169, 23);
            numericUpDown1.TabIndex = 17;
            numericUpDown1.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(10, 222);
            label2.Name = "label2";
            label2.Size = new Size(169, 15);
            label2.TabIndex = 18;
            label2.Text = "Размер обучающей выборки";
            // 
            // button2
            // 
            button2.Enabled = false;
            button2.Location = new Point(10, 269);
            button2.Name = "button2";
            button2.Size = new Size(111, 33);
            button2.TabIndex = 19;
            button2.Text = "Сформировать";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // plotView2
            // 
            plotView2.Location = new Point(205, 498);
            plotView2.Name = "plotView2";
            plotView2.PanCursor = Cursors.Hand;
            plotView2.Size = new Size(564, 286);
            plotView2.TabIndex = 20;
            plotView2.Text = "plotView2";
            plotView2.ZoomHorizontalCursor = Cursors.SizeWE;
            plotView2.ZoomRectangleCursor = Cursors.SizeNWSE;
            plotView2.ZoomVerticalCursor = Cursors.SizeNS;
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
            label4.Location = new Point(10, 498);
            label4.Name = "label4";
            label4.Size = new Size(148, 15);
            label4.TabIndex = 22;
            label4.Text = "Шаг2 - Добавление шума";
            // 
            // button3
            // 
            button3.Enabled = false;
            button3.Location = new Point(10, 570);
            button3.Name = "button3";
            button3.Size = new Size(111, 33);
            button3.TabIndex = 25;
            button3.Text = "Пошуметь";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(10, 523);
            label5.Name = "label5";
            label5.Size = new Size(127, 15);
            label5.TabIndex = 24;
            label5.Text = "Делитель отклонения";
            // 
            // numericUpDown2
            // 
            numericUpDown2.Location = new Point(10, 541);
            numericUpDown2.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDown2.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(169, 23);
            numericUpDown2.TabIndex = 23;
            numericUpDown2.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // plotView3
            // 
            plotView3.Location = new Point(205, 809);
            plotView3.Name = "plotView3";
            plotView3.PanCursor = Cursors.Hand;
            plotView3.Size = new Size(564, 286);
            plotView3.TabIndex = 26;
            plotView3.Text = "plotView3";
            plotView3.ZoomHorizontalCursor = Cursors.SizeWE;
            plotView3.ZoomRectangleCursor = Cursors.SizeNWSE;
            plotView3.ZoomVerticalCursor = Cursors.SizeNS;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(10, 809);
            label6.Name = "label6";
            label6.Size = new Size(145, 15);
            label6.TabIndex = 30;
            label6.Text = "Шаг3 - Тестовая выборка";
            // 
            // button4
            // 
            button4.Enabled = false;
            button4.Location = new Point(10, 877);
            button4.Name = "button4";
            button4.Size = new Size(111, 33);
            button4.TabIndex = 29;
            button4.Text = "Сформировать";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(10, 830);
            label7.Name = "label7";
            label7.Size = new Size(151, 15);
            label7.TabIndex = 28;
            label7.Text = "Размер тестовой выборки";
            // 
            // numericUpDown3
            // 
            numericUpDown3.Location = new Point(10, 848);
            numericUpDown3.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numericUpDown3.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            numericUpDown3.Name = "numericUpDown3";
            numericUpDown3.Size = new Size(169, 23);
            numericUpDown3.TabIndex = 27;
            numericUpDown3.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // plotView4
            // 
            plotView4.Location = new Point(205, 1121);
            plotView4.Name = "plotView4";
            plotView4.PanCursor = Cursors.Hand;
            plotView4.Size = new Size(564, 286);
            plotView4.TabIndex = 31;
            plotView4.Text = "plotView4";
            plotView4.ZoomHorizontalCursor = Cursors.SizeWE;
            plotView4.ZoomRectangleCursor = Cursors.SizeNWSE;
            plotView4.ZoomVerticalCursor = Cursors.SizeNS;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(10, 1121);
            label8.Name = "label8";
            label8.Size = new Size(189, 15);
            label8.TabIndex = 35;
            label8.Text = "Шаг4 - Обучение и тестирование";
            // 
            // button5
            // 
            button5.Enabled = false;
            button5.Location = new Point(10, 1405);
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
            label9.Location = new Point(10, 1137);
            label9.Name = "label9";
            label9.Size = new Size(101, 15);
            label9.TabIndex = 33;
            label9.Text = "Количество эпох";
            // 
            // numericUpDown4
            // 
            numericUpDown4.Location = new Point(10, 1155);
            numericUpDown4.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numericUpDown4.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown4.Name = "numericUpDown4";
            numericUpDown4.Size = new Size(169, 23);
            numericUpDown4.TabIndex = 32;
            numericUpDown4.Value = new decimal(new int[] { 2000, 0, 0, 0 });
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(10, 1186);
            label10.Name = "label10";
            label10.Size = new Size(147, 15);
            label10.TabIndex = 37;
            label10.Text = "Шаг градиентного спуска";
            // 
            // numericUpDown5
            // 
            numericUpDown5.DecimalPlaces = 5;
            numericUpDown5.Increment = new decimal(new int[] { 1, 0, 0, 327680 });
            numericUpDown5.Location = new Point(10, 1204);
            numericUpDown5.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDown5.Minimum = new decimal(new int[] { 1, 0, 0, 327680 });
            numericUpDown5.Name = "numericUpDown5";
            numericUpDown5.Size = new Size(169, 23);
            numericUpDown5.TabIndex = 36;
            numericUpDown5.Value = new decimal(new int[] { 1, 0, 0, 131072 });
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(10, 1235);
            label11.Name = "label11";
            label11.Size = new Size(99, 15);
            label11.TabIndex = 39;
            label11.Text = "Число нейронов";
            // 
            // numericUpDown6
            // 
            numericUpDown6.Location = new Point(10, 1253);
            numericUpDown6.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numericUpDown6.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown6.Name = "numericUpDown6";
            numericUpDown6.Size = new Size(169, 23);
            numericUpDown6.TabIndex = 38;
            numericUpDown6.Value = new decimal(new int[] { 50, 0, 0, 0 });
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Checked = true;
            radioButton1.Location = new Point(3, 3);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(57, 19);
            radioButton1.TabIndex = 40;
            radioButton1.TabStop = true;
            radioButton1.Text = "Adam";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(3, 28);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(47, 19);
            radioButton2.TabIndex = 41;
            radioButton2.Text = "SGD";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Controls.Add(radioButton1);
            panel1.Controls.Add(radioButton2);
            panel1.Location = new Point(10, 1282);
            panel1.Name = "panel1";
            panel1.Size = new Size(69, 55);
            panel1.TabIndex = 42;
            // 
            // panel2
            // 
            panel2.Controls.Add(radioButton3);
            panel2.Controls.Add(radioButton4);
            panel2.Location = new Point(85, 1282);
            panel2.Name = "panel2";
            panel2.Size = new Size(69, 55);
            panel2.TabIndex = 43;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Checked = true;
            radioButton3.Location = new Point(3, 3);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(48, 19);
            radioButton3.TabIndex = 40;
            radioButton3.TabStop = true;
            radioButton3.Text = "MSE";
            radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            radioButton4.AutoSize = true;
            radioButton4.Location = new Point(3, 28);
            radioButton4.Name = "radioButton4";
            radioButton4.Size = new Size(50, 19);
            radioButton4.TabIndex = 41;
            radioButton4.Text = "MAE";
            radioButton4.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(10, 1340);
            label12.Name = "label12";
            label12.Size = new Size(147, 30);
            label12.TabIndex = 44;
            label12.Text = "Момент (только для SGD \r\nпо умолчанию 0)";
            // 
            // numericUpDown7
            // 
            numericUpDown7.DecimalPlaces = 5;
            numericUpDown7.Increment = new decimal(new int[] { 1, 0, 0, 327680 });
            numericUpDown7.Location = new Point(10, 1376);
            numericUpDown7.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown7.Name = "numericUpDown7";
            numericUpDown7.Size = new Size(169, 23);
            numericUpDown7.TabIndex = 45;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(205, 1423);
            label13.Name = "label13";
            label13.Size = new Size(0, 15);
            label13.TabIndex = 46;
            // 
            // SimpleRegress
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            Controls.Add(label13);
            Controls.Add(numericUpDown7);
            Controls.Add(label12);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(label11);
            Controls.Add(numericUpDown6);
            Controls.Add(label10);
            Controls.Add(numericUpDown5);
            Controls.Add(label8);
            Controls.Add(button5);
            Controls.Add(label9);
            Controls.Add(numericUpDown4);
            Controls.Add(plotView4);
            Controls.Add(label6);
            Controls.Add(button4);
            Controls.Add(label7);
            Controls.Add(numericUpDown3);
            Controls.Add(plotView3);
            Controls.Add(button3);
            Controls.Add(label5);
            Controls.Add(numericUpDown2);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(plotView2);
            Controls.Add(button2);
            Controls.Add(label2);
            Controls.Add(numericUpDown1);
            Controls.Add(plotView1);
            Controls.Add(label1);
            Controls.Add(button1);
            Name = "SimpleRegress";
            Size = new Size(795, 1441);
            Load += SetProperty_Load;
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown5).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown6).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown7).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Label label1;
        private OxyPlot.WindowsForms.PlotView plotView1;
        private NumericUpDown numericUpDown1;
        private Label label2;
        private Button button2;
        private OxyPlot.WindowsForms.PlotView plotView2;
        private Label label3;
        private Label label4;
        private Button button3;
        private Label label5;
        private NumericUpDown numericUpDown2;
        private OxyPlot.WindowsForms.PlotView plotView3;
        private Label label6;
        private Button button4;
        private Label label7;
        private NumericUpDown numericUpDown3;
        private OxyPlot.WindowsForms.PlotView plotView4;
        private Label label8;
        private Button button5;
        private Label label9;
        private NumericUpDown numericUpDown4;
        private Label label10;
        private NumericUpDown numericUpDown5;
        private Label label11;
        private NumericUpDown numericUpDown6;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private Panel panel1;
        private Panel panel2;
        private RadioButton radioButton3;
        private RadioButton radioButton4;
        private Label label12;
        private NumericUpDown numericUpDown7;
        private Label label13;
    }
}
