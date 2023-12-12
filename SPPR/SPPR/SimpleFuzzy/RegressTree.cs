using OxyPlot.Annotations;
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
using TorchSharp.Modules;
using TorchSharp;
using System.Runtime.Intrinsics.X86;
using OxyPlot.WindowsForms;

namespace SPPR
{
    public partial class RegressTree : UserControl
    {
        Random random = new Random();
        Module<IGetClassificationSetObject> BaseSet { get; set; }

        Module<ITreeRegressRule> TreeRule { get; set; }

        object[,] TrainCollection { get; set; }
        object[,] TestCollection { get; set; }

        float[] res;

        public RegressTree()
        {
            InitializeComponent();
            BaseSet = new Module<IGetClassificationSetObject>("Модуль выборок", ModuleChecker);
            BaseSet.Location = new Point(10, 20);
            Controls.Add(BaseSet);
            TreeRule = new Module<ITreeRegressRule>("Генератор условий", ModuleChecker);
            TreeRule.Location = new Point(600, 20);
            Controls.Add(TreeRule);

        }

        private void ModuleChecker()
        {
            if (BaseSet.now != null && TreeRule.now != null)
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
            numericUpDown2.Maximum = TrainCollection.GetLength(1) - 1;
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
            (object[], float)[] train = new (object[], float)[TrainCollection.GetLength(0)];
            for (int i = 0; i < train.Length; i++)
            {
                train[i].Item1 = new object[TrainCollection.GetLength(1) - 1];
                train[i].Item2 = (float)(double)TrainCollection[i, 0];
                for (int j = 0; j < TrainCollection.GetLength(1) - 1; j++)
                {
                    train[i].Item1[j] = TrainCollection[i, j + 1];
                }
            }
            TreeRegress tree = new TreeRegress(train, (int)numericUpDown4.Value, (int)numericUpDown5.Value, train[0].Item1.Length, TreeRule.now);
            //label13.Text = tree.Text;
            double mse = 0;
            double r2 = 0;
            double mae = 0;
            double subModel = 0;
            int count = TrainCollection.GetLength(0);
            for (int i = 0; i < count; i++)
            {
                subModel += (float)(double)TrainCollection[i, 0];
            }
            subModel /= count;
            res = new float[TestCollection.GetLength(0)];
            (object[], float)[] test = new (object[], float)[TestCollection.GetLength(0)];
            for (int i = 0; i < test.Length; i++)
            {
                test[i].Item1 = new object[TestCollection.GetLength(1) - 1];
                test[i].Item2 = (float)(double)TestCollection[i, 0];
                for (int j = 0; j < TestCollection.GetLength(1) - 1; j++)
                {
                    test[i].Item1[j] = TestCollection[i, j + 1];
                }
                res[i] = tree.GetValue(test[i].Item1);
                mae += Math.Abs(res[i] - test[i].Item2);
                mse += Math.Pow(res[i] - test[i].Item2, 2);
                r2 += Math.Pow(test[i].Item2 - subModel, 2);
            }
            mse /= TestCollection.GetLength(0);
            mae /= TestCollection.GetLength(0);
            r2 = 1 - ((mse * TestCollection.GetLength(0)) / r2);
            label13.Text = $"\r\nMSE: {mse}\r\nMAE: {mae}\r\nR2: {r2}\r\n";
            RePaint();
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

        private void RePaint()
        {
            PlotModel plotModel = new PlotModel();
            plotModel.Title = BaseSet.now.Name;
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom });
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left });
            ScatterSeries ls = new ScatterSeries();
            ScatterSeries lineSeries = new ScatterSeries();
            for (int i = 0; i < TestCollection.GetLength(0); i++)
            {
                lineSeries.Points.Add(new ScatterPoint(
                    TestCollection[i, (int)numericUpDown2.Value] is double ? (double)TestCollection[i, (int)numericUpDown2.Value] : 0,
                    res[i], 2));
                ls.Points.Add(new ScatterPoint(
                    TestCollection[i, (int)numericUpDown2.Value] is double ? (double)TestCollection[i, (int)numericUpDown2.Value] : 0,
                    (float)(double)TestCollection[i, 0], 3));
            }
            plotModel.Series.Add(ls);

            plotModel.Series.Add(lineSeries);
            plotView3.Model = plotModel;

            PlotModel plotModel4 = new PlotModel();
            ScatterSeries loss = new ScatterSeries();
            for (int i = 0; i < TestCollection.GetLength(0); i++)
            {
                loss.Points.Add(new ScatterPoint(TestCollection[i, (int)numericUpDown2.Value] is double ? (float)(double)TestCollection[i, (int)numericUpDown2.Value] : 0, res[i] - (float)(double)TestCollection[i, 0], 1));
            }
            plotModel4.Series.Add(loss);
            plotView4.Model = plotModel4;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            RePaint();
        }
    }
}
