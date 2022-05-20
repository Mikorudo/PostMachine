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
        public CommandInputForm()
        {
            InitializeComponent();
        }

        private void загрузитьКомандыToolStripMenuItem_Click(object sender, EventArgs e)
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
    }
}
