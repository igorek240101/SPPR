using SimpleFuzzy.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleFuzzy
{
    public partial class Module<T> : UserControl where T : class, INameable
    {
        Dictionary<string, T> dictionary;
        public T now;
        Action allLoaded;
        public Module(string name = "", Action allLoaded = null)
        {
            InitializeComponent();
            label1.Text = name;
            this.allLoaded = allLoaded;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(textBox1.Text))
            {
                Assembly assembly;
                try
                {
                    assembly = Assembly.LoadFile(textBox1.Text);
                }
                catch
                {
                    comboBox1.Visible = false;
                    now = null;
                    allLoaded();
                    label2.Text = "Ну удалось загрузить сборку";
                    return;
                }
                var types = assembly.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(T)));
                dictionary = new Dictionary<string, T>();
                comboBox1.Items.Clear();
                if (types.Count() < 1)
                {
                    label2.Text = "В сборке нет подходящих типов";
                    comboBox1.Visible = false; now = null;
                    allLoaded();
                    return;
                }
                foreach (var type in types)
                {
                    T item = type.GetConstructors()[0].Invoke(null) as T;
                    dictionary.Add(item.Name, item);
                    comboBox1.Items.Add(item.Name);
                }
                comboBox1.SelectedIndex = 0;
                now = dictionary[comboBox1.Items[comboBox1.SelectedIndex].ToString()];
                comboBox1.Visible = true;
                label2.Text = "";
            }
            else { comboBox1.Visible = false; now = null; label2.Text = "Некорректное имя файла"; }
            allLoaded();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog(this);
            textBox1.Text = openFileDialog.FileName;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dictionary.TryGetValue(comboBox1.SelectedItem.ToString(), out now);
            allLoaded();
        }
    }
}
