using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Lab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            this.FormClosing += Form1_FormClosing;
            StartPosition = FormStartPosition.Manual; 
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.Sizable;
            SizeGripStyle = SizeGripStyle.Show;
            ;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            RedoFile();
        }
        private bool unsavedChanges = false;
        private bool activefile = false;
        private string NameFile;
        private int SafeA=0;
        private int SafeB=0;
        private void CreateFile_Click(object sender, EventArgs e)
        {
            ActivFile();
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActivFile();
        }
        private void Form1_SizeChanged(object sender,EventArgs e)
        {
           
        }
        private void ActivFile()
        {
            if (!activefile)
            {
                
                richTextBox1.ReadOnly = false;
                richTextBox1.Enabled = true;
                richTextBox1.Clear();
                activefile = true;
                SafeB= 0;
                SafeA= 0;
            }
            else
            {     
                if (SafeA>SafeB)
                
                {
                    MessageBox.Show("Сохраните предыдущий чтобы перейти к следующему");
                    SafeB = 1;

                    activefile = false;
                    string path = @"C:\Users\ryysl\source\repos\Lab1\Lab1\NewFILE";

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                }
            }
        }
        private void OpenFile()
        {
            var path = @"C:\Users\ryysl\source\repos\Lab1\Lab1\NewFILE";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = path;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                NameFile=openFileDialog1.FileName;
                richTextBox1.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.PlainText);
            }
        }
        private void SafeFile()
        {
            richTextBox1.SelectAll();
            string text = richTextBox1.Text;

            string path = @"C:\Users\ryysl\source\repos\Lab1\Lab1\NewFILE\File" + ".txt";

            if (!Directory.Exists(path))
            {
                string pathNew = @"C:\Users\ryysl\source\repos\Lab1\Lab1\NewFILE\File" + 1 + ".txt";
                File.WriteAllText(pathNew, text);
            }
                SafeA = 1;
            MessageBox.Show("Файл сохранен.");
            unsavedChanges = true;

        }
        private void UndoFile() 
        { 
            this.richTextBox1.Undo(); 
        }
        private void RedoFile() 
        {
            this.richTextBox1.Redo(); 
        }
        private void CopyFile()
        {
            this.richTextBox1.Copy(); 
        }
        private void CutFile()
        {
            this.richTextBox1.Cut();
        }
        private void DeleteFile()
        {
            this.richTextBox1.Clear();
        }
        private void SelectFile()
        {
            this.richTextBox1.SelectAll();
        }
        private void PasteFile() 
        {
            this.richTextBox1.Paste();
        }
        private void Help()
        {
            System.Diagnostics.Process.Start("file:///C:/Users/ryysl/OneDrive/Рабочий%20стол/Lab/Help.html");
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            OpenFile();
        }
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }
        private void Safe_Click(object sender, EventArgs e)
        {
            SafeFile();
            unsavedChanges = false;
    }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (!unsavedChanges)
            {
                // Показать диалоговое окно с запросом на сохранение изменений
                DialogResult result = MessageBox.Show("Сохранить изменения перед закрытием?", "Сохранение изменений", MessageBoxButtons.YesNoCancel);

                switch (result)
                {
                    case DialogResult.Yes:
                        SafeKaK();
                        break;
                    case DialogResult.No:

                        break;
                    case DialogResult.Cancel:

                        e.Cancel = true;
                        break;
                }

            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExitProgramm();
        }
        private void ExitProgramm()
        {
            Close();
        }

        private void SafeKaK()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "File (*.txt)|*.txt|Все файлы (*.*)|*.*";

            DialogResult result = saveFileDialog.ShowDialog();

            
            if (result == DialogResult.OK)
            {
                unsavedChanges = true;
                string filePath = saveFileDialog.FileName;
                File.WriteAllText(filePath, richTextBox1.Text);
            }
        }
        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(richTextBox1.ReadOnly==true)
            { MessageBox.Show("Нечего сохранять"); }
            else
            SafeKaK();
            unsavedChanges=false; 
        }

        private void Undo_Click(object sender, EventArgs e)
        {
            UndoFile();
        }

        private void отменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UndoFile();
        }

        private void повторитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RedoFile();
        }

        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CutFile();
        }

        private void Cut_Click(object sender, EventArgs e)
        {
            CutFile();
        }

        private void Copy_Click(object sender, EventArgs e)
        {
            CopyFile();
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyFile();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteFile();
        }

        private void выделитьВсеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectFile();
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasteFile();
        }

        private void AboutProgramm_Click(object sender, EventArgs e)
        {
            Help();
         }

        private void вызовСправкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
         }

        private void AboutProgram()
        {
            MessageBox.Show("Назввние программы:\nКомпилятор 1.00Аlfa\nАвтор:Гаврилов Руслан\nГод: 2024");
        }
        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutProgram();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
