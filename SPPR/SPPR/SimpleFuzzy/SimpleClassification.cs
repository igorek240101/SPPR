using Medallion;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using SimpleFuzzy.Abstract;
using TorchSharp;
using TorchSharp.Modules;
using SPPR.Abstract;
using static TorchSharp.torch;
using static TorchSharp.torch.optim;

namespace SimpleFuzzy
{
    public partial class SimpleClassification : UserControl
    {
        Random random = new Random();
        Module<IGetClassificationSet> BaseSet { get; set; }

        float[,] TrainCollection { get; set; }
        float[,] TestCollection { get; set; }

        double[] ClassKeys { get; set; }

        int classCount = 0;

        public SimpleClassification()
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
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left });
            Dictionary<double, ScatterSeries> ls = new Dictionary<double, ScatterSeries>();
            for (int i = 0; i < TrainCollection.GetLength(0); i++)
            {
                ls.TryAdd(TrainCollection[i, 0], new ScatterSeries());
                ls[TrainCollection[i, 0]].Points.Add(new ScatterPoint(TrainCollection[i, 1], TrainCollection[i, 2], 3));
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
                ls.TryAdd(TestCollection[i, 0], new ScatterSeries());
                ls[TestCollection[i, 0]].Points.Add(new ScatterPoint(TestCollection[i, 1], TestCollection[i, 2], 3));
            }
            foreach (var value in ls.Values)
                model.Series.Add(value);
            classCount = ls.Count;
            plotView2.Model = model;
            button5.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            float[] array = new float[TrainCollection.GetLength(0) * (TrainCollection.GetLength(1) - 1)];
            for (int i = 0; i < TrainCollection.GetLength(0); i++)
            {
                for (int j = 1; j < TrainCollection.GetLength(1); j++)
                {
                    array[i * (TrainCollection.GetLength(1) - 1) + j - 1] = TrainCollection[i, j];
                }
            }
            Tensor trainInput = array;
            trainInput = trainInput.reshape(TrainCollection.GetLength(0), TrainCollection.GetLength(1) - 1);
            array = new float[TrainCollection.GetLength(0) * TrainCollection.GetLength(1)];
            for (int i = 0; i < TrainCollection.GetLength(0); i++)
            {
                for (int j = 0; j < TrainCollection.GetLength(1); j++)
                {
                    array[i * TrainCollection.GetLength(1) + j] = TrainCollection[i, j];
                }
            }
            Tensor trainOutput = array;
            trainOutput = trainOutput.reshape(TrainCollection.GetLength(0), TrainCollection.GetLength(1));
            array = new float[TestCollection.GetLength(0) * (TestCollection.GetLength(1) - 1)];
            for (int i = 0; i < TestCollection.GetLength(0); i++)
            {
                for (int j = 1; j < TestCollection.GetLength(1); j++)
                {
                    array[i * (TestCollection.GetLength(1) - 1) + j - 1] = TestCollection[i, j];
                }
            }
            Tensor testInput = array;
            testInput = testInput.reshape(TestCollection.GetLength(0), TestCollection.GetLength(1) - 1);
            array = new float[TestCollection.GetLength(0) * TestCollection.GetLength(1)];
            for (int i = 0; i < TestCollection.GetLength(0); i++)
            {
                for (int j = 0; j < TestCollection.GetLength(1); j++)
                {
                    array[i * TestCollection.GetLength(1) + j] = TestCollection[i, j];
                }
            }
            Tensor testOutput = array;
            testOutput = testOutput.reshape(TestCollection.GetLength(0), TestCollection.GetLength(1));


            (string, nn.Module<Tensor, Tensor>)[] modules = new (string, nn.Module<Tensor, Tensor>)[(int)numericUpDown2.Value * 2 + 2];
            modules[0] = ("fc1", nn.Linear(TrainCollection.GetLength(1) - 1, (int)numericUpDown6.Value));
            for (int i = 1; i < modules.Length - 2; i++)
            {
                if (i % 2 == 0)
                {
                    modules[i] = ($"fc1{i / 2}", nn.Linear((int)numericUpDown6.Value, (int)numericUpDown6.Value));
                }
                else
                {
                    modules[i] = ($"act{i / 2 + 1}", nn.Sigmoid());
                }
            }
            modules[^2] = ($"fc1{(modules.Length - 1) / 2}", nn.Linear((int)numericUpDown6.Value, classCount));
            modules[^1] = ("sm", nn.Softmax(1));
            var seq = nn.Sequential(modules);


            OptimizerHelper opt;
            if (radioButton1.Checked) opt = optim.Adam(seq.parameters(), (double)numericUpDown5.Value);
            else opt = optim.SGD(seq.parameters(), (double)numericUpDown5.Value, momentum: (double)numericUpDown7.Value);
            for (int i = 0; i < numericUpDown4.Value; i++)
            {
                opt.zero_grad();
                var y = seq.forward(trainInput);
                var oldLoss = nn.functional.mse_loss(y, trainOutput);
                //oldLoss.bytes = BitConverter.GetBytes((float)MAE(y, trainOutput, TrainCollection.GetLength(0)));
                var loss = nn.functional.cross_entropy(y, trainOutput); //radioButton3.Checked ? nn.functional.mse_loss(y, trainOutput) : oldLoss;
                string s = ((float)loss).ToString();
                loss.backward();
                opt.step();
            }
            PlotModel model = new PlotModel();
            //model.Title = $"net {HasFunc.now.Name}";
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left });
            var ls1 = new ScatterSeries();
            var ls2 = new ScatterSeries();
            var res = seq.forward(testInput);
            for (int i = 0; i < TestCollection.Length; i++)
            {
                //ls1.Points.Add(new ScatterPoint(TestCollection[i].Item1, TestCollection[i].Item2, 3));
                //ls2.Points.Add(new ScatterPoint(TestCollection[i].Item1, (double)res[i], 3));
            }
            var mseLoss = nn.functional.mse_loss(res, testOutput);
            var maeLoss = 0;// MAE(res, testOutput, TestCollection.Length);
            label13.Text = $"MSE : {(double)mseLoss}\r\nMAE : {(double)maeLoss}";
            model.Series.Add(ls1);
            model.Series.Add(ls2);
        }

        private Tensor MAE(Tensor first, Tensor second, int count)
        {
            double mae = 0;
            for (int i = 0; i < count; i++)
            {
                mae += Math.Abs((double)(first[i] - second[i]));
            }
            mae /= count;
            return torch.tensor(mae, requires_grad: true);
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
