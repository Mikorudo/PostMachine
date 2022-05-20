namespace WinPostMachine
{
    partial class CommandInputForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.загрузитьКомандыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьКомандуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.передатьНаМашинуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 28);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(800, 422);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "* Строки, помечанные (*) игнорируются интерпритатором команд\n* Команды для машины" +
    " поста бла-бла-бла\n* 1...\n* 2...\n....\nВвод\n";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.загрузитьКомандыToolStripMenuItem,
            this.сохранитьКомандуToolStripMenuItem,
            this.передатьНаМашинуToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // загрузитьКомандыToolStripMenuItem
            // 
            this.загрузитьКомандыToolStripMenuItem.Name = "загрузитьКомандыToolStripMenuItem";
            this.загрузитьКомандыToolStripMenuItem.Size = new System.Drawing.Size(148, 24);
            this.загрузитьКомандыToolStripMenuItem.Text = "Открыть команды";
            this.загрузитьКомандыToolStripMenuItem.Click += new System.EventHandler(this.загрузитьКомандыToolStripMenuItem_Click);
            // 
            // сохранитьКомандуToolStripMenuItem
            // 
            this.сохранитьКомандуToolStripMenuItem.Name = "сохранитьКомандуToolStripMenuItem";
            this.сохранитьКомандуToolStripMenuItem.Size = new System.Drawing.Size(160, 24);
            this.сохранитьКомандуToolStripMenuItem.Text = "Сохранить команду";
            // 
            // передатьНаМашинуToolStripMenuItem
            // 
            this.передатьНаМашинуToolStripMenuItem.Name = "передатьНаМашинуToolStripMenuItem";
            this.передатьНаМашинуToolStripMenuItem.Size = new System.Drawing.Size(170, 24);
            this.передатьНаМашинуToolStripMenuItem.Text = "Передать на машину";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "txt";
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Тестовые файлы(*.txt) | *.txt";
            // 
            // CommandInputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "CommandInputForm";
            this.Text = "CommandInputForm";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem загрузитьКомандыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьКомандуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem передатьНаМашинуToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}