using Medallion;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using SimpleFuzzy.Abstract;
using TorchSharp;
using TorchSharp.Modules;
using static TorchSharp.torch;
using static TorchSharp.torch.optim;

namespace SimpleFuzzy
{
    public partial class SimpleRegress : UserControl
    {
        Random random = new Random();
        Module<IGetSets> BaseSet { get; set; }
        Module<IHasFunc> HasFunc { get; set; }

        (float, float)[] TrainCollection { get; set; }
        (float, float)[] TrainCollectionWithErorr { get; set; }
        (float, float)[] TestCollection { get; set; }
        public SimpleRegress()
        {
            InitializeComponent();
            BaseSet = new Module<IGetSets>("Модуль выборок", ModuleChecker);
            BaseSet.Location = new Point(10, 20);
            Controls.Add(BaseSet);
            HasFunc = new Module<IHasFunc>("Модуль регресируемой функции", ModuleChecker);
            HasFunc.Location = new Point(10, 80);
            Controls.Add(HasFunc);
        }

        private void ModuleChecker()
        {
            if (BaseSet.now != null && HasFunc.now != null)
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
        }

        private void SetProperty_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            TrainCollection = BaseSet.now.GetTrainSet((int)numericUpDown1.Value).ToList().ConvertAll(t => ((float)t, (float)HasFunc.now.GetResult(t))).ToArray();
            PlotModel model = new PlotModel();
            model.Title = HasFunc.now.Name;
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left });
            var ls1 = new ScatterSeries();
            foreach (var value in TrainCollection)
            {
                ls1.Points.Add(new ScatterPoint(value.Item1, value.Item2, 3));
            }
            model.Series.Add(ls1);
            plotView1.Model = model;
            button3.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TrainCollectionWithErorr = TrainCollection.ToList().ToArray();
            PlotModel model = new PlotModel();
            model.Title = $"noise {HasFunc.now.Name}";
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left });
            var ls1 = new ScatterSeries();
            var ls2 = new ScatterSeries();
            for (int i = 0; i < TrainCollectionWithErorr.Length; i++)
            {
                double r = random.NextGaussian() / (double)numericUpDown2.Value;
                ls2.Points.Add(new ScatterPoint(TrainCollectionWithErorr[i].Item1, r, 3));
                ls1.Points.Add(new ScatterPoint(TrainCollectionWithErorr[i].Item1, TrainCollectionWithErorr[i].Item2 + r, 3));
            }
            model.Series.Add(ls1);
            model.Series.Add(ls2);
            plotView2.Model = model;
            button4.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TestCollection = BaseSet.now.GetTestSet((int)numericUpDown3.Value).ToList().ConvertAll(t => ((float)t, (float)HasFunc.now.GetResult(t))).ToArray();
            PlotModel model = new PlotModel();
            model.Title = HasFunc.now.Name;
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left });
            var ls1 = new ScatterSeries();
            foreach (var value in TestCollection)
            {
                ls1.Points.Add(new ScatterPoint(value.Item1, value.Item2, 3));
            }
            model.Series.Add(ls1);
            plotView3.Model = model;
            button5.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Tensor trainInput = TrainCollectionWithErorr.Select(t => t.Item1).ToArray();
            trainInput.unsqueeze_(1);
            Tensor trainOutput = TrainCollectionWithErorr.Select(t => t.Item2).ToArray();
            trainOutput.unsqueeze_(1);
            Tensor testInput = TestCollection.Select(t => t.Item1).ToArray();
            testInput.unsqueeze_(1);
            Tensor testOutput = TestCollection.Select(t => t.Item2).ToArray();
            testOutput.unsqueeze_(1);
            var seq = nn.Sequential(("fc1", nn.Linear(1, (int)numericUpDown6.Value)),
                ("act1", nn.Sigmoid()),
                ("fc2", nn.Linear((int)numericUpDown6.Value, 1)));
            OptimizerHelper opt;
            if (radioButton1.Checked) opt = optim.Adam(seq.parameters(), (double)numericUpDown5.Value);
            else opt = optim.SGD(seq.parameters(), (double)numericUpDown5.Value, momentum: (double)numericUpDown7.Value);
            for (int i = 0; i < numericUpDown4.Value; i++)
            {
                opt.zero_grad();
                var y = seq.forward(trainInput);
                var oldLoss = nn.functional.mse_loss(y, trainOutput);
                oldLoss.bytes = BitConverter.GetBytes((float)MAE(y, trainOutput, TrainCollectionWithErorr.Length));
                var loss = radioButton3.Checked ? nn.functional.mse_loss(y, trainOutput) : oldLoss;
                loss.backward();
                opt.step();
            }
            PlotModel model = new PlotModel();
            model.Title = $"net {HasFunc.now.Name}";
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left });
            var ls1 = new ScatterSeries();
            var ls2 = new ScatterSeries();
            var res = seq.forward(testInput);
            for (int i = 0; i < TestCollection.Length; i++)
            {
                ls1.Points.Add(new ScatterPoint(TestCollection[i].Item1, TestCollection[i].Item2, 3));
                ls2.Points.Add(new ScatterPoint(TestCollection[i].Item1, (double)res[i], 3));
            }
            var mseLoss = nn.functional.mse_loss(res, testOutput);
            var maeLoss = MAE(res, testOutput, TestCollection.Length);
            label13.Text = $"MSE : {(double)mseLoss}\r\nMAE : {(double)maeLoss}";
            model.Series.Add(ls1);
            model.Series.Add(ls2);
            plotView4.Model = model;
        }

        private Tensor MAE(Tensor first, Tensor second, int count)
        {
            double mae = 0;
            for(int i = 0; i < count; i++)
            {
                mae += Math.Abs((double)(first[i] - second[i]));
            }
            mae /= count;
            return torch.tensor(mae, requires_grad: true);
        }
    }
}
