using Accord.Math;
using SPPR.Abstract;
using System.Configuration;
using System.Windows.Forms;
using TorchSharp;

namespace SPPR
{
    public partial class VisualNeroNet : UserControl
    {
        private MLP mlp;
        NumericUpDown[] inputs;
        VisualNeron[][] levels;
        (Control, Control, float)[][] connections;
        public IOptimazer optimazer;
        public ILoss loss;
        public IRegularization regularization;
        public float n;
        public float l; 
        public float k;
        public MLP MLP
        {
            set
            {
                if (inputs != null)
                    foreach (var input in inputs)
                    {
                        Controls.Remove(input);
                    }
                if (levels != null)
                    foreach (var input in levels)
                    {
                        foreach (var output in input)
                            Controls.Remove(output);
                    }
                mlp = value;
                inputs = new NumericUpDown[mlp.W[0].Length];
                levels = new VisualNeron[mlp.levels.Length][];
                for (int i = 0; i < mlp.W[0].Length; i++)
                {
                    inputs[i] = new NumericUpDown();
                    inputs[i].Increment = (decimal)0.0001;
                    inputs[i].Maximum = 1000;
                    inputs[i].Minimum = -1000;
                    inputs[i].DecimalPlaces = 4;
                    inputs[i].Size = new Size(58, 23);
                    inputs[i].Location = new Point(0, (int)(1050.0 / (mlp.W[0].Length + 1) * (i + 1)) + 63);
                    Controls.Add(inputs[i]);
                }
                int count = 0;
                connections = new (Control, Control, float)[mlp.W.Length][];
                for (int i = 0; i < mlp.levels.Length; i++)
                {
                    levels[i] = new VisualNeron[mlp.levels[i].Length];
                    for (int j = 0; j < mlp.levels[i].Length; j++)
                    {
                        connections[count] = new (Control, Control, float)[mlp.W[count].Length];
                        levels[i][j] = new VisualNeron();
                        levels[i][j].sender = this;
                        levels[i][j].Neron = mlp.levels[i][j];
                        levels[i][j].Location = new Point(550 + i * 550, (int)(1050.0 / (mlp.levels[i].Length + 1) * (j + 1)) + 63);
                        for (int k = 0; k < 10; k++)
                        {
                            if (mlp.W[count].Length > k)
                            {
                                levels[i][j].numericUpDown[k].Value = (decimal)mlp.W[count][k];
                                connections[count][k] = (i == 0 ? inputs[k] as Control :
                                    levels.ToList().ConvertAll(
                                        t => t?.FirstOrDefault(
                                            v => v?.Neron == mlp.levels[i][j].Input[k])).FirstOrDefault(
                                        t => t?.Neron == mlp.levels[i][j].Input[k]) as Control, levels[i][j], mlp.W[count][k]);
                            }
                            else levels[i][j].numericUpDown[k].Visible = false;
                        }
                        count++;
                        levels[i][j].Eventer();
                        Controls.Add(levels[i][j]);
                    }
                }
                Refresh();
            }
        }
        public VisualNeroNet()
        {
            InitializeComponent();
        }

        public void SetW(VisualNeron sender, int index)
        {
            int count = 0;
            for (int i = 0; i < levels.Length; i++)
            {
                for (int j = 0; j < levels[i].Length; j++)
                {
                    if (levels[i][j] == sender)
                    {
                        mlp.W[count][index] = (float)sender.numericUpDown[index].Value;
                        break;
                    }
                    count++;
                }
            }
        }

        private void VisualNeroNet_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen black = new Pen(Color.Black, 3);
            Pen red = new Pen(Color.Red, 3);
            Pen green = new Pen(Color.Green, 3);
            for (int i = 0; i < connections.Length; i++)
            {
                for (int j = 0; j < connections[i].Length; j++)
                {
                    Pen now = null;
                    if (mlp.W[i][j] < connections[i][j].Item3)
                        now = red;
                    if (mlp.W[i][j] > connections[i][j].Item3)
                        now = green;
                    if (mlp.W[i][j] == connections[i][j].Item3)
                        now = black;
                    g.DrawLine(now,
                        new Point(connections[i][j].Item1.Location.X +
                        connections[i][j].Item1.Width / 2,
                        connections[i][j].Item1.Location.Y +
                        connections[i][j].Item1.Height / 2),
                        new Point(connections[i][j].Item2.Location.X +
                        connections[i][j].Item2.Width / 2,
                        connections[i][j].Item2.Location.Y +
                        connections[i][j].Item2.Height / 2));
                }
            }
            for (int i = 0; i < mlp.levels.Length; i++)
            {
                for (int j = 0; j < mlp.levels[i].Length; j++)
                {
                    levels[i][j].button1.BackColor = Color.FromArgb(0, mlp.levels[i][j].Output < 0 ? 0 : mlp.levels[i][j].Output > 1 ? 255 : (int)(mlp.levels[i][j].Output * 255), 0);
                    levels[i][j].button1.Text = mlp.levels[i][j].Output.ToString();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mlp.W = optimazer.Optimazer(mlp.Calc, mlp.W, new float[][] { inputs.ToList().ConvertAll(t => (float)t.Value).ToArray() }, new float[] { (float)numericUpDown1.Value }, n, l, k, loss, regularization);
            Refresh();
            for (int i = 0; i < connections.Length; i++)
            {
                for (int j = 0; j < connections[i].Length; j++)
                {
                    connections[i][j].Item3 = mlp.W[i][j];
                }
            }
            int count = 0;
            for (int i = 0; i < levels.Length; i++)
            {
                for (int j = 0; j < levels[i].Length; j++)
                {
                    for (int k = 0; k < mlp.W[count].Length; k++)
                    {
                        levels[i][j].numericUpDown[k].Value = (decimal)mlp.W[count][k];
                    }
                    count++;
                }
            }
        }
    }
}
