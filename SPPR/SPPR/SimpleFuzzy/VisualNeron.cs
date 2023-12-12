using Accord.Math;

namespace SPPR
{
    public partial class VisualNeron : UserControl
    {
        public Neron Neron { get; set; }
        public VisualNeroNet sender { get; set; }
        public VisualNeron()
        {
            InitializeComponent();
        }

        public void Eventer()
        {
            foreach (var value in numericUpDown)
            {
                value.ValueChanged += numericUpDown_ValueChanged;
            }
        }

        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            this.sender.SetW(this, numericUpDown.IndexOf(sender));
        }
    }
}
