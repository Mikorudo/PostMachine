using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WinPostMachine
{
    public partial class Form1 : Form
    {
        private PictureBox[] _pictureBoxes;
        private Label[] _labels;
        private PostMachine _postMachine;
        private PostMachine _virtualPostMachine;
        private int Count
        {
            get
            {
                return _count;
            }
            set
            {
                _count = value;
                commandCountLabel.Text = "Выполнено команд: " + _count;
            }
        }
        private int _count;
        private void UpdateTape(Tape tape, bool isIfElseCmd)
        {
            int[] indexes;
            bool[] marks;
            tape.GetCellsAroundCurrent(out indexes, out marks);
            for (int i = 0; i < 11; i++)
            {
                _labels[i].Text = indexes[i].ToString();
                if (marks[i])
                    _pictureBoxes[i].Image = imageList1.Images[i == 5 && isIfElseCmd ? 3 : 1];
                else
                    _pictureBoxes[i].Image = imageList1.Images[i == 5 && isIfElseCmd ? 2 : 0];
            }
        }
        public void PrintToTextBox(string txt)
        {
            richTextBox1.Text += txt + "\n";
            Count++;
        }
        public void PrintToMessageBox(string txt)
        {
            MessageBox.Show(txt);
            startButton.Enabled = true;
        }
        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
            CheckForIllegalCrossThreadCalls = false;

            _postMachine = new PostMachine();
            _postMachine.Delay = 1000;
            _postMachine.updateTape += UpdateTape;
            _postMachine.commandMessage += PrintToTextBox;
            _postMachine.workEnd += PrintToMessageBox;

            _virtualPostMachine = new PostMachine();
            _virtualPostMachine.Delay = 1;
            Count = 0;

            InitializeCells();
            ResizeCells(null, null);
        }
        private void InitializeCells()
        {
            _pictureBoxes = new PictureBox[12];
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
            _pictureBoxes[11] = pictureBox12; //Каретка
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
            _pictureBoxes[11].Image = imageList1.Images[4];
        }

        private void ResizeCells(object sender, EventArgs e)
        {
            int width = (panel1.Size.Width - 8 * 2 - 2 * 5 - 4) / 11;
            Point point = new Point(2, 0);
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
            point = _pictureBoxes[5].Location;
            point.X -= 3;
            point.Y -= 3;
            _pictureBoxes[11].Location = point;
            _pictureBoxes[11].Size = new Size(_pictureBoxes[5].Size.Width + 6, _pictureBoxes[5].Size.Height + 6);
        }

        private void Start(object sender, EventArgs e)
        {
            try
            {
                startButton.Enabled = false;
                Reset(null, null);
                _postMachine.ExecuteCommands();
                _virtualPostMachine.Reset();
                new Thread(CheckEndlessAlgoritm).Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Stop(object sender, EventArgs e)
        {
            _postMachine.StopExecutingCommands();
            startButton.Enabled = true;
        }
        public void LoadCommands(string text)
        {
            _postMachine.LoadCommands(CommandInterpreter.TextToCommands(text));
            _virtualPostMachine.LoadCommands(CommandInterpreter.TextToCommands(text));
        }
        private void LoadCommandsDialog(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string fileName = openFileDialog1.FileName;
                    _postMachine.LoadCommands(fileName);
                    _virtualPostMachine.LoadCommands(fileName);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
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
                    _postMachine.Delay = int.Parse(item.Text);
                }
                else
                    item.Checked = false;
            }
        }

        private void CreateCommands(object sender, EventArgs e)
        {
            CommandInputForm commandInputForm = new CommandInputForm(this);
            commandInputForm.Show();
        }

        private void Reset(object sender, EventArgs e)
        {
            Stop(null, null);
            _postMachine.Reset();
            richTextBox1.Text = "";
            Count = 0;
            InitializeCells();
        }
        private void CheckEndlessAlgoritm()
        {
            _virtualPostMachine.ExecuteCommands();
            Thread.Sleep(3000);
            if (_virtualPostMachine.Condition)
            {
                _virtualPostMachine.StopExecutingCommands();
                MessageBox.Show("Возможно алгоритм зацикленный, рекомендуем приостановить процесс", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }

        private void ShowHelp(object sender, EventArgs e)
        {
            string path = "PostMachine.chm";
            Help.ShowHelp(this, Application.StartupPath + path);
        }

    }
}
