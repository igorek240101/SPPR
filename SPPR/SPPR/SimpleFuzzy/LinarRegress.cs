using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using SimpleFuzzy;
using SPPR.Abstract;
using System.Data;
using static SPPR.SimpleMatrix;

namespace SPPR
{
    public partial class LinarRegress : UserControl
    {
        Random random = new Random();
        Module<IGetClassificationSet> BaseSet { get; set; }

        float[,] TrainCollection { get; set; }
        float[,] TestCollection { get; set; }

        float[,] model;

        float[] res;

        public LinarRegress()
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
            double mse = 0;
            double r2 = 0;
            double mae = 0;
            double subModel = 0;
            int count = TrainCollection.GetLength(0);
            for (int i = 0; i < count; i++)
            {
                subModel += TrainCollection[i, 0];
            }
            subModel /= count;
            float[,] y = new float[count, 1];
            for (int i = 0; i < count; i++)
            {
                y[i, 0] = TrainCollection[i, 0];
                TrainCollection[i, 0] = 1;
            }
            res = new float[TestCollection.GetLength(0)];
            model = Mul(Mul(Reverse(Mul(T(TrainCollection), TrainCollection)), T(TrainCollection)), y);
            for (int i = 0; i < TestCollection.GetLength(0); i++)
            {
                float answer = model[0, 0];
                for (int j = 1; j < TestCollection.GetLength(1); j++)
                    answer += model[j, 0] * TestCollection[i, j];
                res[i] = answer;
                mae += Math.Abs(answer - TestCollection[i, 0]);
                mse += Math.Pow(answer - TestCollection[i, 0], 2);
                r2 += Math.Pow(TestCollection[i, 0] - subModel, 2);
            }
            mse /= TestCollection.GetLength(0);
            mae /= TestCollection.GetLength(0);
            r2 = 1 - ((mse * TestCollection.GetLength(0)) / r2);
            label13.Text = $"MSE: {mse}\r\nMAE: {mae}\r\nR2: {r2}\r\n";
            RePaint();
            numericUpDown2.Enabled = true;
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
            int min = 0, max = 0;
            for (int i = 0; i < TestCollection.GetLength(0); i++)
            {
                if (TestCollection[i, (int)numericUpDown2.Value] < TestCollection[min, (int)numericUpDown2.Value])
                    min = i;
                if (TestCollection[i, (int)numericUpDown2.Value] > TestCollection[max, (int)numericUpDown2.Value])
                    max = i;
                ls.Points.Add(new ScatterPoint(
                    TestCollection[i, (int)numericUpDown2.Value],
                    TestCollection[i, 0], 3));
            }
            plotModel.Series.Add(ls);

            LineSeries lineSeries = new LineSeries();
            lineSeries.Points.Add(new DataPoint(TestCollection[min, (int)numericUpDown2.Value], res[min]));
            lineSeries.Points.Add(new DataPoint(TestCollection[max, (int)numericUpDown2.Value], res[max]));
            plotModel.Series.Add(lineSeries);
            plotView3.Model = plotModel;

            PlotModel plotModel4 = new PlotModel();
            ScatterSeries loss = new ScatterSeries();
            for (int i = 0; i < TestCollection.GetLength(0); i++)
            {
                loss.Points.Add(new ScatterPoint(TestCollection[i, (int)numericUpDown2.Value], res[i] - TestCollection[i, 0], 1));
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
