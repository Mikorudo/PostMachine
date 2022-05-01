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
        private PictureBox[] pictureBoxes;
        private Label[] labels;
        public Form1()
        {
            InitializeComponent();
            InitializeCells();
            ResizeCells();
        }
        private void InitializeCells()
        {
            pictureBoxes = new PictureBox[11];
            labels = new Label[11];
            #region FillArray
            pictureBoxes[0] = pictureBox1;
            pictureBoxes[1] = pictureBox2;
            pictureBoxes[2] = pictureBox3;
            pictureBoxes[3] = pictureBox4;
            pictureBoxes[4] = pictureBox5;
            pictureBoxes[5] = pictureBox6;
            pictureBoxes[6] = pictureBox7;
            pictureBoxes[7] = pictureBox8;
            pictureBoxes[8] = pictureBox9;
            pictureBoxes[9] = pictureBox10;
            pictureBoxes[10] = pictureBox11;
            labels[0] = label1;
            labels[1] = label2;
            labels[2] = label3;
            labels[3] = label4;
            labels[4] = label5;
            labels[5] = label6;
            labels[6] = label7;
            labels[7] = label8;
            labels[8] = label9;
            labels[9] = label10;
            labels[10] = label11;
            #endregion
                for (int i = 0; i < 11; i++)
                {
                    labels[i].Text = (i - 5).ToString();
                    pictureBoxes[i].Image = imageList1.Images[0];
                }
        }
        private void ResizeCells()
        {
            int width = (panel1.Size.Width - 8 * 2 - 2 * 5) / 11;
            Point point = new Point(0, 0);
            for (int i = 0; i < 11; i++)
            {
                labels[i].Width = width;
                labels[i].Height = 20;
                labels[i].TextAlign = ContentAlignment.MiddleCenter;
                pictureBoxes[i].Width = width;
                pictureBoxes[i].Height = width;

                labels[i].Location = point;
                point.Y += label1.Height;
                pictureBoxes[i].Location = point;
                point.Y -= label1.Height;

                point.X += width;
                point.X += (i == 4 || i == 5 ? 6 : 2);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            var filePath = Path.Combine(desktopPath, "commands.txt");
            VisualTape visualTape = new VisualTape(imageList1, pictureBoxes, labels);
            WinMachine winMachine = new WinMachine(visualTape);
            winMachine.LoadCommands(filePath);
            winMachine.ExecuteCommandsAsync();
            button1.Enabled = false;
        }
    }
}
