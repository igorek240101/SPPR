using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using SimpleFuzzy;
using SPPR.Abstract;
using System.Data;

namespace SPPR
{
    public partial class OneParamModel : UserControl
    {
        Random random = new Random();
        Module<IGetClassificationSet> BaseSet { get; set; }

        float[,] TrainCollection { get; set; }
        float[,] TestCollection { get; set; }

        public OneParamModel()
        {
            InitializeComponent();
            BaseSet = new Module<IGetClassificationSet>("Модуль выборок", ModuleChecker);
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
                    TrainCollection[i, 1],
                    TrainCollection[i, 0], 3));
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
                    TrainCollection[i, 1],
                    TrainCollection[i, 0], 3));
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
            (double, double) model;
            int count = TrainCollection.GetLength(0);
            double sumX = 0, sumY = 0, sumXY = 0, sumXX = 0;
            for (int i = 0; i < count; i++)
            {
                subModel += TrainCollection[i, 0];
                sumX += TrainCollection[i, parametr];
                sumXX += TrainCollection[i, parametr] * TrainCollection[i, parametr];
                sumY += TrainCollection[i, 0];
                sumXY += TrainCollection[i, parametr] * TrainCollection[i, 0];
            }
            subModel /= count;
            model.Item2 = ((sumXY / count) - (sumX / count * sumY / count)) / ((sumXX / count) - Math.Pow(sumX / count, 2));
            model.Item1 = (sumY / count) - (model.Item2 * (sumX / count));
            PlotModel plotModel = new PlotModel();
            plotModel.Title = BaseSet.now.Name;
            plotModel.Axes.Add(new CategoryAxis { Position = AxisPosition.Bottom });
            plotModel.Axes.Add(new CategoryAxis { Position = AxisPosition.Left });
            ScatterSeries ls = new ScatterSeries();
            float? min = null, max = null;
            for (int i = 0; i < TestCollection.GetLength(0); i++)
            {
                if (min == null || TestCollection[i, parametr] < min)
                    min = TestCollection[i, parametr];
                if (max == null || TestCollection[i, parametr] > max)
                    max = TestCollection[i, parametr];
                ls.Points.Add(new ScatterPoint(TestCollection[i, parametr], TestCollection[i, 0], 3));
                mae += Math.Abs(model.Item1 + model.Item2 * TestCollection[i, parametr] - TestCollection[i, 0]);
                mse += Math.Pow(model.Item1 + model.Item2 * TestCollection[i, parametr] - TestCollection[i, 0], 2);
                r2 += Math.Pow(TestCollection[i, 0] - subModel, 2);
            }
            plotModel.Series.Add(ls);
            LineSeries line = new LineSeries();
            line.Points.Add(new DataPoint(min.Value, model.Item1 + model.Item2 * min.Value));
            line.Points.Add(new DataPoint(max.Value, model.Item1 + model.Item2 * max.Value));
            plotView3.Model = plotModel;
            plotModel.Series.Add(line);
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
