using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinPostMachine
{
    public partial class Form1 : Form
    {
        private PictureBox[] _pictureBoxes;
        private Label[] _labels;
        private WinMachine _winMachine;
        private VisualTape _visualTape;
        public Form1()
        {
            InitializeComponent();
            InitializeCells();
            ResizeCells();
            _visualTape = new VisualTape(imageList1, _pictureBoxes, _labels);
            _winMachine = new WinMachine(_visualTape);
        }
        private void InitializeCells()
        {
            _pictureBoxes = new PictureBox[11];
            _labels = new Label[11];
            #region FillArray
            _pictureBoxes[0] = pictureBox1;
            _pictureBoxes[1] = pictureBox2;
            _pictureBoxes[2] = pictureBox3;
            _pictureBoxes[3] = pictureBox4;
            _pictureBoxes[4] = pictureBox5;
            _pictureBoxes[5] = pictureBox6;
            _pictureBoxes[6] = pictureBox7;
            _pictureBoxes[7] = pictureBox8;
            _pictureBoxes[8] = pictureBox9;
            _pictureBoxes[9] = pictureBox10;
            _pictureBoxes[10] = pictureBox11;
            _labels[0] = label1;
            _labels[1] = label2;
            _labels[2] = label3;
            _labels[3] = label4;
            _labels[4] = label5;
            _labels[5] = label6;
            _labels[6] = label7;
            _labels[7] = label8;
            _labels[8] = label9;
            _labels[9] = label10;
            _labels[10] = label11;
            #endregion
            for (int i = 0; i < 11; i++)
            {
                _labels[i].Text = (i - 5).ToString();
                _pictureBoxes[i].Image = imageList1.Images[0];
            }
        }
        private void ResizeCells()
        {
            int width = (panel1.Size.Width - 8 * 2 - 2 * 5 - 4) / 11;
            Point point = new Point(0, 0);
            for (int i = 0; i < 11; i++)
            {
                _labels[i].Width = width;
                _labels[i].Height = 20;
                _labels[i].TextAlign = ContentAlignment.MiddleCenter;
                _pictureBoxes[i].Width = width;
                _pictureBoxes[i].Height = width;

                _labels[i].Location = point;
                point.Y += label1.Height;
                _pictureBoxes[i].Location = point;
                point.Y -= label1.Height;

                point.X += width;
                point.X += (i == 4 || i == 5 ? 6 : 2);
            }
        }

        private void Start(object sender, EventArgs e)
        {
            try
            {
                _winMachine.ExecuteCommands();
                button1.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadCommands(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog1.FileName;
                _winMachine.LoadCommands(fileName);
            }
        }


        private void ChangeDelayTime(object sender, EventArgs e)
        {
            ToolStripMenuItem toolStripMenuItem = menuStrip1.Items[1] as ToolStripMenuItem;
            toolStripMenuItem = toolStripMenuItem.DropDownItems[0] as ToolStripMenuItem;

            foreach (ToolStripMenuItem item in toolStripMenuItem.DropDownItems)
            {
                if (item == sender)
                {
                    item.Checked = true;
                }
                else
                    item.Checked = false;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
