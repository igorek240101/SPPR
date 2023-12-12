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

namespace SPPR
{
    public partial class TreeClassification : UserControl
    {
        Random random = new Random();
        Module<IGetClassificationSetObject> BaseSet { get; set; }

        Module<ITreeRule> TreeRule { get; set; }

        object[,] TrainCollection { get; set; }
        object[,] TestCollection { get; set; }

        int[] ClassKeys { get; set; }

        int classCount { get; set; } = 0;

        public TreeClassification()
        {
            InitializeComponent();
            BaseSet = new Module<IGetClassificationSetObject>("Модуль выборок", ModuleChecker);
            BaseSet.Location = new Point(10, 20);
            Controls.Add(BaseSet);
            TreeRule = new Module<ITreeRule>("Генератор условий", ModuleChecker);
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
            Dictionary<int, ScatterSeries> ls = new Dictionary<int, ScatterSeries>();
            for (int i = 0; i < TrainCollection.GetLength(0); i++)
            {
                ls.TryAdd((int)TrainCollection[i, 0], new ScatterSeries());
                ls[(int)TrainCollection[i, 0]].Points.Add(new ScatterPoint(
                    TrainCollection[i, 1] is double ? (double)TrainCollection[i, 1] : 0,
                    TrainCollection[i, 2] is double ? (double)TrainCollection[i, 2] : 0, 3));
            }
            foreach (var value in ls.Values)
                model.Series.Add(value);
            ClassKeys = ls.Keys.ToArray();
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
            Dictionary<double, ScatterSeries> ls = new Dictionary<double, ScatterSeries>();
            foreach (var value in ClassKeys)
                ls.Add(value, new ScatterSeries());
            for (int i = 0; i < TestCollection.GetLength(0); i++)
            {
                ls.TryAdd((int)TestCollection[i, 0], new ScatterSeries());
                ls[(int)TestCollection[i, 0]].Points.Add(new ScatterPoint(
                    TrainCollection[i, 1] is double ? (double)TrainCollection[i, 1] : 0,
                    TrainCollection[i, 2] is double ? (double)TrainCollection[i, 2] : 0, 3));
            }
            foreach (var value in ls.Values)
                model.Series.Add(value);
            classCount = ls.Count;
            plotView2.Model = model;
            button5.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            (object[], int)[] train = new (object[], int)[TrainCollection.GetLength(0)];
            for (int i = 0; i < train.Length; i++)
            {
                train[i].Item1 = new object[TrainCollection.GetLength(1) - 1];
                train[i].Item2 = (int)TrainCollection[i, 0];
                for (int j = 0; j < TrainCollection.GetLength(1) - 1; j++)
                {
                    train[i].Item1[j] = TrainCollection[i, j + 1];
                }
            }
            Tree tree = new Tree(train, (int)numericUpDown4.Value, (int)numericUpDown5.Value, train[0].Item1.Length, TreeRule.now);
            label13.Text = tree.Text;
            int trueCount = 0;
            (object[], int)[] test = new (object[], int)[TestCollection.GetLength(0)];
            dataGridView1.RowCount = classCount;
            dataGridView1.ColumnCount = classCount;
            for (int i = 0; i < classCount; i++)
                for (int j = 0; j < classCount; j++)
                    dataGridView1[i, j].Value = 0;
            for (int i = 0; i < test.Length; i++)
            {
                test[i].Item1 = new object[TestCollection.GetLength(1) - 1];
                test[i].Item2 = (int)TestCollection[i, 0];
                for (int j = 0; j < TestCollection.GetLength(1) - 1; j++)
                {
                    test[i].Item1[j] = TestCollection[i, j + 1];
                }
                int res = tree.GetClass(test[i].Item1);
                if (res == test[i].Item2)
                    trueCount++;
                dataGridView1[test[i].Item2 - 1, res - 1].Value = (int)dataGridView1[test[i].Item2 - 1, res - 1].Value + 1;
            }
            label13.Text += $"\r\n{100.0 * trueCount / test.Length}%";
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
