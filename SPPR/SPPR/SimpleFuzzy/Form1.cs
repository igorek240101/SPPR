using System.Reflection;

namespace SimpleFuzzy
{
    public partial class Form1 : Form
    {
        UserControl nowPanel;
        Dictionary<string, UserControl> userControls = new Dictionary<string, UserControl>();

        public Form1()
        {
            InitializeComponent();
        }

        private void setsToolStripMenuItem_Click(object sender, EventArgs e)
            => SetPanel("SimpleRegress", typeof(SimpleRegress).GetConstructors()[0]);

        private void simpleClassificationToolStripMenuItem_Click(object sender, EventArgs e)
            => SetPanel("simpleClassification", typeof(SimpleClassification).GetConstructors()[0]);

        private void SetPanel(string key, ConstructorInfo constructor)
        {
            if (nowPanel != null)
            {
                nowPanel.Visible = false;
            }
            UserControl newPanel = null;
            if (!userControls.TryGetValue(key, out newPanel))
            {
                newPanel = constructor.Invoke(null) as UserControl;
                userControls.Add(key, newPanel);
                Controls.Add(newPanel);
            }
            else
            {
                newPanel.Visible = true;
            }
            nowPanel = newPanel;
        }
    }
}