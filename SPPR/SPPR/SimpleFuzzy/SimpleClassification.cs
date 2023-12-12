using Medallion;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using SimpleFuzzy.Abstract;
using TorchSharp;
using TorchSharp.Modules;
using SPPR.Abstract;
using static TorchSharp.TensorExtensionMethods;
using static TorchSharp.torch;
using static TorchSharp.torch.optim;
using System.Collections.Generic;
using Accord.Math;
using OxyPlot.Annotations;

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

        internal class WineNet : torch.nn.Module<Tensor, Tensor>
        {
            Sequential sequential1;
            Sequential sequential2;

            public WineNet(int count, float[,] trainCollection, int inCount, int classCount) : base(nameof(WineNet))
            {
                (string, nn.Module<Tensor, Tensor>)[] modules1 = new (string, nn.Module<Tensor, Tensor>)[count * 2 + 1];
                (string, nn.Module<Tensor, Tensor>)[] modules2 = new (string, nn.Module<Tensor, Tensor>)[count * 2 + 2];
                modules1[0] = ("fc1", nn.Linear(trainCollection.GetLength(1) - 1, inCount));
                for (int i = 1; i < modules1.Length - 1; i++)
                {
                    if (i % 2 == 0)
                    {
                        modules1[i] = ($"fc{i / 2}", nn.Linear(inCount, inCount));
                        modules2[i] = ($"fc{i / 2}", nn.Linear(inCount, inCount));
                    }
                    else
                    {
                        modules1[i] = ($"act{i / 2 + 1}", nn.Sigmoid());
                        modules2[i] = ($"act{i / 2 + 1}", nn.Sigmoid());
                    }
                }
                modules1[^1] = ($"fc{(modules1.Length - 1) / 2}", nn.Linear(inCount, classCount));
                modules2[^2] = ($"fc{(modules1.Length - 1) / 2}", nn.Linear(inCount, classCount));
                modules2[^1] = ($"sm", nn.Softmax(1));
                sequential1 = nn.Sequential(modules1);
                sequential2 = nn.Sequential(modules2);

                RegisterComponents();
            }

            public override Tensor forward(Tensor x)
            {
                return sequential1.forward(x);
            }

            public Tensor inference(Tensor x)
            {
                return sequential2.forward(x);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            float[] array = new float[TestCollection.GetLength(0) * (TestCollection.GetLength(1) - 1)];
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
            WineNet net = new WineNet((int)numericUpDown2.Value, TrainCollection, (int)numericUpDown6.Value, classCount);
            int batch_size = (int)numericUpDown3.Value;
            OptimizerHelper opt;
            if (radioButton1.Checked) opt = optim.Adam(net.parameters(), (double)numericUpDown5.Value);
            else opt = optim.SGD(net.parameters(), (double)numericUpDown5.Value, momentum: (double)numericUpDown7.Value);
            List<int> order = new List<int>();
            for (int i = 0; i < TrainCollection.GetLength(0); i++)
            {
                order.Add(i);
            }
            for (int i = 0; i < numericUpDown4.Value; i++)
            {
                order = GetOrder(order);
                for (int j = 0; j < TrainCollection.GetLength(0); j += batch_size)
                {
                    opt.zero_grad();

                    array = new float[(TrainCollection.GetLength(1) - 1) * ((j + 1) * batch_size < TrainCollection.GetLength(0) ? batch_size : TrainCollection.GetLength(0) % batch_size)];
                    for (int k = 0; k < ((j + 1) * batch_size < TrainCollection.GetLength(0) ? batch_size : TrainCollection.GetLength(0) % batch_size); k++)
                    {
                        for (int l = 1; l < TrainCollection.GetLength(1); l++)
                        {
                            array[k * (TrainCollection.GetLength(1) - 1) + l - 1] = TrainCollection[order[j + k], l];
                        }
                    }
                    Tensor trainInput = array;
                    trainInput = trainInput.reshape((j + 1) * batch_size < TrainCollection.GetLength(0) ? batch_size : TrainCollection.GetLength(0) % batch_size, TrainCollection.GetLength(1) - 1);
                    array = new float[TrainCollection.GetLength(1) * ((j + 1) * batch_size < TrainCollection.GetLength(0) ? batch_size : TrainCollection.GetLength(0) % batch_size)];
                    for (int k = 0; k < (((j + 1) * batch_size) < TrainCollection.GetLength(0) ? batch_size : TrainCollection.GetLength(0) % batch_size); k++)
                    {
                        for (int l = 0; l < TrainCollection.GetLength(1); l++)
                        {
                            array[k * TrainCollection.GetLength(1) + l] = TrainCollection[order[j + k], l];
                        }
                    }
                    Tensor trainOutput = array;
                    trainOutput = trainOutput.reshape((j + 1) * batch_size < TrainCollection.GetLength(0) ? batch_size : TrainCollection.GetLength(0) % batch_size, TrainCollection.GetLength(1));
                    var y = net.forward(trainInput);
                    var loss = nn.functional.cross_entropy(y, trainOutput);
                    loss.backward();
                    opt.step();
                }
                if (i % 100 == 0)
                {
                    var subRes = net.forward(testInput);
                    int[,] subErrorMatrix = new int[classCount, classCount];
                    for (int j = 0; j < TestCollection.GetLength(0); j++)
                    {
                        int ress = (int)subRes[j][0];
                        subErrorMatrix[(int)TestCollection[j, 0] - 1, (int)subRes[j][0]]++;
                        //ls1.Points.Add(new ScatterPoint(TestCollection[i].Item1, TestCollection[i].Item2, 3));
                        //ls2.Points.Add(new ScatterPoint(TestCollection[i].Item1, (double)res[i], 3));
                    }
                }
            }
            PlotModel model = new PlotModel();
            model.Title = $"Классификация";
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left });
            var res = net.inference(testInput);
            int[,] errorMatrix = new int[classCount, classCount];
            Dictionary<double, (ScatterSeries, RectangleSeries)> ls = new Dictionary<double, (ScatterSeries, RectangleSeries)>();
            foreach (var value in ClassKeys)
                ls.Add(value, (new ScatterSeries(), new RectangleSeries()));
            for (int i = 0; i < TestCollection.GetLength(0); i++)
            {
                var temp = (double)res[i][0];
                errorMatrix[(int)TestCollection[i, 0] - 1, (int)Math.Round((double)res[i][0]) - 1]++;
                ls.TryAdd(TestCollection[i, 0], (new ScatterSeries(), new RectangleSeries()));
                ls[TestCollection[i, 0]].Item1.Points.Add(new ScatterPoint(TestCollection[i, 1], TestCollection[i, 2], 3));
            }
            foreach (var value in ls.Values)
            {
                model.Series.Add(value.Item1);
                model.Series.Add(value.Item2);
            }
            if (TrainCollection.GetLength(1) == 3)
            {
                float[] paint = new float[20000];
                for (int i = 0; i < 100; i++)
                {
                    for (int j = 0; j < 100; j++)
                    {
                        paint[(i * 100 + j) * 2] = i / 100.0f;
                        paint[(i * 100 + j) * 2 + 1] = j / 100.0f;
                    }
                }
                Tensor paintTensor = paint;
                paintTensor = paintTensor.reshape(10000, 2);
                var paintRes = net.forward(paintTensor);
                for(int i = 0; i < 9999; i++)
                {
                    double v3 = (int)Math.Round((double)paintRes[i][0]);
                    v3 = v3 < 1 ? 1 : v3;
                    v3 = v3 > classCount ? classCount : v3;
                    model.Annotations.Add(new RectangleAnnotation()
                    {
                        MinimumX = paint[i * 2 + 1],
                        MinimumY = paint[i * 2 + 2],
                        MaximumX = paint[i * 2 + 1] + 1.01,
                        MaximumY = paint[i * 2 + 2] + 1.01,
                        Fill = ls[v3].Item1.ActualMarkerFillColor
                    });
                }
            }
            model.Annotations.Add(new RectangleAnnotation()
            {
                MinimumX = 0,
                MinimumY = 0,
                MaximumX = 0.5,
                MaximumY = 1,
                Fill = OxyColor.FromArgb(128, 255, 0, 0)
            });

            plotView3.Model = model;
            var mseLoss = nn.functional.mse_loss(res, testOutput);
            var maeLoss = 0;// MAE(res, testOutput, TestCollection.Length);
            label13.Text = $"MSE : {(double)mseLoss}\r\nMAE : {(double)maeLoss}";
        }

        private List<int> GetOrder(List<int> input)
        {
            var order = new List<int>();
            while (input.Count > 0)
            {
                int index = random.Next(input.Count);
                order.Add(input[index]);
                input.RemoveAt(index);
            }
            return order;
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
