using System.Reflection;
using SPPR;

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

        private void neroToolStripMenuItem_Click(object sender, EventArgs e)
            => SetPanel("simpleClassification", typeof(SimpleClassification).GetConstructors()[0]);

        private void treeToolStripMenuItem1_Click(object sender, EventArgs e)
            => SetPanel("TreeClassification", typeof(TreeClassification).GetConstructors()[0]);

        private void forestToolStripMenuItem1_Click(object sender, EventArgs e)
            => SetPanel("RandomForest", typeof(RandomForestClassification).GetConstructors()[0]);

        private void neroToolStripMenuItem1_Click(object sender, EventArgs e)
            => SetPanel("SimpleRegress", typeof(SimpleRegress).GetConstructors()[0]);

        private void constantToolStripMenuItem_Click(object sender, EventArgs e)
            => SetPanel("Constant", typeof(Constant).GetConstructors()[0]);

        private void constantByGroupToolStripMenuItem_Click(object sender, EventArgs e)
            => SetPanel("ConstantByGroup", typeof(ConstantByGroups).GetConstructors()[0]);

        private void oneParamModelToolStripMenuItem_Click(object sender, EventArgs e)
            => SetPanel("OneParamModel", typeof(OneParamModel).GetConstructors()[0]);

        private void linarRegressToolStripMenuItem_Click(object sender, EventArgs e)
            => SetPanel("LinarRegress", typeof(LinarRegress).GetConstructors()[0]);

        private void polyRegressToolStripMenuItem_Click(object sender, EventArgs e)
            => SetPanel("PolyRegress", typeof(PolyRegress).GetConstructors()[0]);

        private void treeToolStripMenuItem_Click(object sender, EventArgs e)
            => SetPanel("RegressTree", typeof(RegressTree).GetConstructors()[0]);

        private void randomForestToolStripMenuItem_Click(object sender, EventArgs e)
            => SetPanel("RegressForest", typeof(RandomForestRegress).GetConstructors()[0]);

        private void simpleNeroToolStripMenuItem_Click(object sender, EventArgs e)
            => SetPanel("SimpleNeroRegress", typeof(SimpleNeroRegress).GetConstructors()[0]);

        private void simpleNeroToolStripMenuItem1_Click(object sender, EventArgs e)
            => SetPanel("SimpleNeroClassification", typeof(SimpleNeroClassification).GetConstructors()[0]);

        private void imageNeroToolStripMenuItem_Click(object sender, EventArgs e)
            => SetPanel("ImageNeroClassification", typeof(ImageNeroClassification).GetConstructors()[0]);
    }
}