using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using AbstractPostMachine;
using System.Threading.Tasks;

namespace WinPostMachine
{
    internal class VisualTape
    {
        ImageList imageList;
        PictureBox[] pictureBoxes;
        Label[] labels;
        public VisualTape(ImageList imageList, PictureBox[] pictureBoxes, Label[] labels)
        {
            this.imageList = imageList;
            this.pictureBoxes = pictureBoxes;
            this.labels = labels;
        }
        public void UpdateTape(int[] indexes, bool[] marks, bool isCmdIfElse)
        {
            for (int i = 0; i < 11; i++)
            {
                labels[i].Text = indexes[i].ToString();
                if (marks[i])
                    pictureBoxes[i].Image = imageList.Images[i == 5 && isCmdIfElse ? 3 : 1];
                else
                    pictureBoxes[i].Image = imageList.Images[i == 5 && isCmdIfElse ? 2 : 0];
            }
        }
    }
   internal class WinMachine : Machine
   {
        private VisualTape visualTape;
        public WinMachine(VisualTape visualTape)
        {
            if (visualTape == null)
                throw new ArgumentNullException();
            this.visualTape = visualTape;
        }
        public async void ExecuteCommandsAsync()
        {
            int currentCommand = 1;
            while (true)
            {
                bool isCmdIfElse = commands[currentCommand].GetType() == typeof(IfElseCmd);
                currentCommand = commands[currentCommand].ExecuteCommand(ref tape);
                int[] indexes;
                bool[] marks;
                tape.GetCellsAroundCurrent(out indexes, out marks);
                visualTape.UpdateTape(indexes, marks, isCmdIfElse);
                if (currentCommand == 0)
                {
                    MessageBox.Show("Алгоритм успешно завершил свою работу");
                    break;
                }
                if (currentCommand == -1)
                {
                    MessageBox.Show("Выполнение недостижимого кода");
                    break;
                }
                await Task.Delay(1000);
            }
        }
        public override void ExecuteCommands()
        {
            int currentCommand = 1;
            while (true)
            {
                //Thread.Sleep(1000);
                bool isCmdIfElse = commands[currentCommand].GetType() == typeof(IfElseCmd);
                currentCommand = commands[currentCommand].ExecuteCommand(ref tape);
                int[] indexes;
                bool[] marks;
                tape.GetCellsAroundCurrent(out indexes, out marks);
                visualTape.UpdateTape(indexes, marks, isCmdIfElse);
                //Thread.Sleep(1000);
                if (currentCommand == 0)
                {
                    MessageBox.Show("Алгоритм успешно завершил свою работу");
                    break;
                }
                if (currentCommand == -1)
                {
                    MessageBox.Show("Выполнение недостижимого кода");
                    break;
                }
            }
        }
   }
}
