using Accord.Math;
using Accord.Statistics;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using SimpleFuzzy;
using SPPR.Abstract;

namespace SPPR
{
    public partial class SimpleNeroClassification : UserControl
    {
        Random random = new Random();
        Module<IGetClassificationSet> BaseSet { get; set; }

        Module<IOptimazer> Optimazer { get; set; }

        Module<ILoss> Loss { get; set; }

        Module<IRegularization> Regularization { get; set; }

        Module<IActivationFunc> Activation { get; set; }

        float[,] TrainCollection { get; set; }
        float[,] TestCollection { get; set; }

        float[] res;

        int[] ClassKeys { get; set; }

        int classCount { get; set; } = 0;

        VisualNeroNet Visual { get; set; }

        public SimpleNeroClassification()
        {
            InitializeComponent();
            BaseSet = new Module<IGetClassificationSet>("Модуль выборок", ModuleChecker);
            BaseSet.Location = new Point(10, 20);
            Controls.Add(BaseSet);
            Optimazer = new Module<IOptimazer>("Оптимизатор", ModuleChecker);
            Optimazer.Location = new Point(600, 20);
            Controls.Add(Optimazer);
            Loss = new Module<ILoss>("Лосс-функция", ModuleChecker);
            Loss.Location = new Point(1200, 20);
            Controls.Add(Loss);
            Regularization = new Module<IRegularization>("Регуляризация", ModuleChecker);
            Regularization.Location = new Point(1800, 20);
            Controls.Add(Regularization);
            Activation = new Module<IActivationFunc>("Функция активации", ModuleChecker);
            Activation.Location = new Point(2400, 20);
            Controls.Add(Activation);
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
            if (Visual != null)
            {
                Visual.Dispose();
                Controls.Remove(Visual);
            }
            int count = TrainCollection.GetLength(0);
            List<int> mlpCount = new List<int>();
            mlpCount.Add(TrainCollection.GetLength(1) - 1);
            if (numericUpDown3.Value > 0) mlpCount.Add((int)numericUpDown3.Value);
            if (numericUpDown7.Value > 0) mlpCount.Add((int)numericUpDown7.Value);
            if (numericUpDown9.Value > 0) mlpCount.Add((int)numericUpDown9.Value);
            mlpCount.Add(classCount);
            MLP mLP = new MLP(mlpCount.ToArray(), Activation.now.ActivationFunc);
            res = new float[TestCollection.GetLength(0)];
            float[][] train = new float[TrainCollection.GetLength(0)][];
            float[] trainAnswers = new float[TrainCollection.GetLength(0)];
            dataGridView1.RowCount = classCount;
            dataGridView1.ColumnCount = classCount;
            for (int i = 0; i < classCount; i++)
                for (int j = 0; j < classCount; j++)
                    dataGridView1[i, j].Value = 0;
            for (int i = 0; i < TrainCollection.GetLength(0); i++)
            {
                train[i] = new float[TrainCollection.GetLength(1) - 1];
                for (int j = 0; j < train[i].Length; j++)
                    train[i][j] = TrainCollection[i, j + 1];
                trainAnswers[i] = TrainCollection[i, 0];
            }
            mLP.W = Optimazer.now.Optimazer(mLP.Calc, mLP.W, train, trainAnswers, (float)numericUpDown4.Value, (float)numericUpDown6.Value, (float)numericUpDown5.Value, Loss.now, Regularization.now);
            int trueCount = 0;
            for (int i = 0; i < TestCollection.GetLength(0); i++)
            {
                float[] values = new float[TestCollection.GetLength(1) - 1];
                for (int j = 0; j < values.Length; j++)
                    values[j] = TestCollection[i, j + 1];
                float[] answer = mLP.Calc(mLP.W, values);
                res[i] = answer.IndexOf(answer.Max()) + 1;
                if (res[i] == TestCollection[i, 0])
                    trueCount++;
                dataGridView1[(int)TestCollection[i, 0] - 1, (int)res[i] - 1].Value = (int)dataGridView1[(int)TestCollection[i, 0] - 1, (int)res[i] - 1].Value + 1;
            }
            label13.Text = $"{100 * trueCount / TestCollection.GetLength(0)}%";
            Visual = new VisualNeroNet();
            Visual.MLP = mLP;
            Visual.optimazer = Optimazer.now;
            Visual.loss = Loss.now;
            Visual.regularization = Regularization.now;
            Visual.n = (float)numericUpDown4.Value;
            Visual.l = (float)numericUpDown6.Value;
            Visual.k = (float)numericUpDown5.Value;
            Visual.Location = new Point(323, 1939);
            Controls.Add(Visual);
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
