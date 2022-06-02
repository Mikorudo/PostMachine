using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WinPostMachine
{
    public partial class CommandInputForm : Form
    {
        private Form parent;
        public CommandInputForm(Form parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

        private void OpenCommandsFile(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog1.FileName;
                using (StreamReader sr = new StreamReader(fileName))
                {
                    richTextBox1.Text = sr.ReadToEnd();
                }
            }
        }

        private void SaveCommandsFile(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filename = saveFileDialog1.FileName;
                File.WriteAllText(filename, richTextBox1.Text);
            }
        }

        private void SendCommands(object sender, EventArgs e)
        {
            (parent as Form1).LoadCommands(richTextBox1.Text);
            MessageBox.Show("Команды переданы в машину Поста");
        }
    }
}
