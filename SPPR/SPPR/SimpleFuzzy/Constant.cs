using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot;
using SimpleFuzzy;
using SPPR.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPPR
{
    public partial class Constant : UserControl
    {
        Random random = new Random();
        Module<IGetClassificationSetObject> BaseSet { get; set; }

        object[,] TrainCollection { get; set; }
        object[,] TestCollection { get; set; }

        int[] ClassKeys { get; set; }

        int classCount { get; set; } = 0;

        public Constant()
        {
            InitializeComponent();
            BaseSet = new Module<IGetClassificationSetObject>("Модуль выборок", ModuleChecker);
            BaseSet.Location = new Point(10, 20);
            Controls.Add(BaseSet);
        }

        private void ModuleChecker()
        {
            if (BaseSet.now != null)
            {
                button1.Enabled = true;
                label1.Text = "";
            }
            else
            {
                label1.Text = "Не все модули загружены";
                button1.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            button6.Enabled = true;
        }

        private void SetProperty_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            TrainCollection = BaseSet.now.GetTrainSet(radioButton5.Checked ?
                                trackBar1.Value / 100.0 : (int)numericUpDown1.Value);
            PlotModel model = new PlotModel();
            model.Title = BaseSet.now.Name;
            model.Axes.Add(new CategoryAxis { Position = AxisPosition.Bottom });
            model.Axes.Add(new CategoryAxis { Position = AxisPosition.Left });
            ScatterSeries ls = new ScatterSeries();
            for (int i = 0; i < TrainCollection.GetLength(0); i++)
            {
                ls.Points.Add(new ScatterPoint(
                    TrainCollection[i, 1] is double ? (double)TrainCollection[i, 1] : 0,
                    TrainCollection[i, 0] is double ? (double)TrainCollection[i, 0] : 0, 3));
            }
            model.Series.Add(ls);
            plotView1.Model = model;
            button3.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TestCollection = BaseSet.now.GetTestSet(radioButton8.Checked ?
                                trackBar2.Value / 100.0 : (int)numericUpDown8.Value);
            PlotModel model = new PlotModel();
            model.Title = BaseSet.now.Name;
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left });
            ScatterSeries ls = new ScatterSeries();
            for (int i = 0; i < TrainCollection.GetLength(0); i++)
            {
                ls.Points.Add(new ScatterPoint(
                    TrainCollection[i, 1] is double ? (double)TrainCollection[i, 1] : 0,
                    TrainCollection[i, 0] is double ? (double)TrainCollection[i, 0] : 0, 3));
            }
            model.Series.Add(ls);
            plotView2.Model = model;
            button5.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            double model = 0;
            for (int i = 0; i < TrainCollection.GetLength(0); i++)
            {
                model += (double)TrainCollection[i, 0];
            }
            model /= TrainCollection.GetLength(0);
            double mse = 0;
            double r2 = 0;
            double mae = 0;
            for (int i = 0; i < TestCollection.GetLength(0); i++)
            {
                mae += Math.Abs(model - (double)TestCollection[i, 0]);
                mse += Math.Pow(model - (double)TestCollection[i, 0], 2);
                r2 += Math.Pow((double)TestCollection[i, 0] - model, 2);
            }
            mse /= TestCollection.GetLength(0);
            mae /= TestCollection.GetLength(0);
            r2 = 1 - ((mse * TestCollection.GetLength(0)) / r2);
            label13.Text = $"MSE: {mse}\r\nMAE: {mae}\r\nR2: {r2}\r\n";
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label2.Text = $"{trackBar1.Value}%";
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            trackBar1.Enabled = false;
            numericUpDown1.Enabled = true;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            trackBar1.Enabled = true;
            numericUpDown1.Enabled = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            BaseSet.now.Reload();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            label14.Text = $"{trackBar2.Value}%";
        }
    }
}
