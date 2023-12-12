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
    public partial class ConstantByGroups : UserControl
    {
        Random random = new Random();
        Module<IGetClassificationSetObject> BaseSet { get; set; }

        object[,] TrainCollection { get; set; }
        object[,] TestCollection { get; set; }

        int[] ClassKeys { get; set; }

        int classCount { get; set; } = 0;

        public ConstantByGroups()
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
            int parametr = (int)numericUpDown2.Value;
            double mse = 0;
            double r2 = 0;
            double mae = 0;
            double subModel = 0;
            for (int i = 0; i < TrainCollection.GetLength(0); i++)
            {
                subModel += (double)TrainCollection[i, 0];
            }
            subModel /= TrainCollection.GetLength(0);
            if (TrainCollection[0, parametr] is double)
            {
                Dictionary<double, (double, int)> trainer = new Dictionary<double, (double, int)>();
                for (int i = 0; i < TrainCollection.GetLength(0); i++)
                {
                    if (trainer.ContainsKey((double)TrainCollection[i, parametr]))
                    {
                        trainer[(double)TrainCollection[i, parametr]] =
                            (trainer[(double)TrainCollection[i, parametr]].Item1 + (double)TrainCollection[i, 0],
                            trainer[(double)TrainCollection[i, parametr]].Item2 + 1);
                    }
                    else
                    {
                        trainer.Add((double)TrainCollection[i, parametr], ((double)TrainCollection[i, 0], 1));
                    }
                }
                List<(double, double)> model = new List<(double, double)>();
                foreach (var values in trainer)
                    model.Add((values.Key, values.Value.Item1 / values.Value.Item2));
                model = model.OrderBy(t => t.Item1).ToList();
                for (int i = 0; i < TestCollection.GetLength(0); i++)
                {
                    int minIndex = 0;
                    for (int j = 1; j < model.Count; j++)
                    {
                        if (Math.Abs(model[j].Item1 - (double)TestCollection[i, parametr]) < Math.Abs(model[minIndex].Item1 - (double)TestCollection[i, parametr]))
                            minIndex = j;
                    }
                    mae += Math.Abs(model[minIndex].Item2 - (double)TestCollection[i, 0]);
                    mse += Math.Pow(model[minIndex].Item2 - (double)TestCollection[i, 0], 2);
                    r2 += Math.Pow((double)TestCollection[i, 0] - subModel, 2);
                }
                mse /= TestCollection.GetLength(0);
                mae /= TestCollection.GetLength(0);
                r2 = 1 - ((mse * TestCollection.GetLength(0)) / r2);
            }
            else
            {
                Dictionary<object, (double, int)> trainer = new Dictionary<object, (double, int)>();
                for (int i = 0; i < TrainCollection.GetLength(0); i++)
                {
                    if (trainer.ContainsKey(TrainCollection[i, parametr]))
                    {
                        trainer[TrainCollection[i, parametr]] =
                            (trainer[TrainCollection[i, parametr]].Item1 + (double)TrainCollection[i, 0],
                            trainer[TrainCollection[i, parametr]].Item2 + 1);
                    }
                    else
                    {
                        trainer.Add(TrainCollection[i, parametr], ((double)TrainCollection[i, 0], 1));
                    }
                }
                Dictionary<object, double> model = new Dictionary<object, double>();
                foreach (var values in trainer)
                    model.Add(values.Key, values.Value.Item1 / values.Value.Item2);
                for (int i = 0; i < TestCollection.GetLength(0); i++)
                {
                    if (model.ContainsKey(TestCollection[i, parametr]))
                    {
                        mae += Math.Abs(model[TestCollection[i, parametr]] - (double)TestCollection[i, 0]);
                        mse += Math.Pow(model[TestCollection[i, parametr]] - (double)TestCollection[i, 0], 2);
                        r2 += Math.Pow((double)TestCollection[i, 0] - subModel, 2);
                    }
                    else
                    {
                        mae += Math.Abs(subModel - (double)TestCollection[i, 0]);
                        mse += Math.Pow(subModel - (double)TestCollection[i, 0], 2);
                        r2 += Math.Pow((double)TestCollection[i, 0] - subModel, 2);
                    }
                }
                mse /= TestCollection.GetLength(0);
                mae /= TestCollection.GetLength(0);
                r2 = 1 - ((mse * TestCollection.GetLength(0)) / r2);
            }
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
