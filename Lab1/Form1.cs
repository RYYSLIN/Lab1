using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
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
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            RedoFile();
        }
        private bool unsavedChanges = false;
        private bool activefile = false;
        private string NameFile;
        private int SafeA = 0;
        private int SafeB = 0;
        private void CreateFile_Click(object sender, EventArgs e)
        {
            ActivFile();
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActivFile();
        }
        private void Form1_SizeChanged(object sender, EventArgs e)
        {

        }
        private void ActivFile()
        {
            if (!activefile)
            {

                richTextBox1.ReadOnly = false;
                richTextBox1.Enabled = true;
                dataGridView1.Enabled = true;
                richTextBox1.Clear();
                activefile = true;
                SafeB = 0;
                SafeA = 0;
            }
            else
            {
                if (SafeA > SafeB)

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
                NameFile = openFileDialog1.FileName;
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
            if (richTextBox1.ReadOnly == true)
            { MessageBox.Show("Нечего сохранять"); }
            else
                SafeKaK();
            unsavedChanges = false;
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
        // тут начинается сканер ------------------------------------------------------------------------------------------
        private void пускToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scan();
        }
        private void Scan()
        {
            string text = richTextBox1.Text;
            List<Token> tokens = LexicalAnalysis(text);
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Код", typeof(string));
            dataTable.Columns.Add("Тип", typeof(string));
            dataTable.Columns.Add("Значение", typeof(string));
            dataTable.Columns.Add("Начальная Позиция", typeof(int));
            dataTable.Columns.Add("Конечная Позиция", typeof(int));

            foreach (var token in tokens)
            {
                dataTable.Rows.Add(token.Cod, token.Type.ToString(), token.Value, token.Column + 1, token.ColumnNext+1);
            }
            dataGridView1.DataSource = dataTable;
        }

        private List<Token> LexicalAnalysis(string text)
        { List<Token> tokens = new List<Token>();
           
            int position=0;

            while (position < text.Length)
            {
                char currentChar = text[position];

                if (position + 1 < text.Length && text.Substring(position, 2) == "::")
                {
                    tokens.Add(new Token((int)TokenType.оператор,TokenType.оператор, "::", position,position+1, 2));
                    position += 2;
                    
                }
                else if (position + 7 < text.Length && text.Substring(position, 7) == "COMPLEX")
                {
                    tokens.Add(new Token((int)TokenType.структура,TokenType.структура, "COMPLEX", position,position+5,7));
                    position += 7;
                }
                else if (text[position] == ' ')
                {
                    tokens.Add(new Token((int)TokenType.пробел,TokenType.пробел, " ", position,position, 1));
                    position++;
                }
                else if (char.IsLetter(currentChar))
                {
                    // Идентификатор
                    int identifierStart = position;
                    while (position < text.Length && (char.IsLetterOrDigit(text[position]) || text[position] == '_'))
                    {
                        position++;
                    }
                    string identifier = text.Substring(identifierStart, position - identifierStart);
                    tokens.Add(new Token((int)TokenType.идетификатор,TokenType.идетификатор, identifier, identifierStart, position -1,position+identifierStart));
                }
                else if (currentChar == '=')
                {
                    tokens.Add(new Token((int)TokenType.оператор_присваивания,TokenType.оператор_присваивания, "=", position,position+1, 1));
                    position++;
                }
                else if (currentChar == '(')
                {
                    tokens.Add(new Token((int)TokenType.открывающая_скобка,TokenType.открывающая_скобка, "(", position, position, 1));
                    position++;
                }
                else if (currentChar == ')')
                {
                    tokens.Add(new Token((int)TokenType.Закрывающая_скобка,TokenType.Закрывающая_скобка, ")", position,position, 1));
                    position++;
                }
                else if (char.IsDigit(currentChar) || currentChar == '.')
                {
                    // Целое или число с плавающей запятой
                    int numberStart = position;
                    string number1 = "";

                    while (position < text.Length && (char.IsDigit(text[position]) || text[position] == '.'))
                    {
                        if (text[position] == '.')
                        {
                            position++;
                            if (!char.IsDigit(text[position])) { }//Логика ошибки если после точки нету цифры
                                

                            // Обрабатываем число с плавающей запятой

                            while (position < text.Length && char.IsDigit(text[position]))
                            {
                                position++;
                            }
                            number1 = text.Substring(numberStart, position - numberStart);
                            break;
                        }
                        position++;
                    }

                    if (string.IsNullOrEmpty(number1))
                    {
                        // Обрабатываем целое число
                        number1 = text.Substring(numberStart, position - numberStart);
                    }

                    tokens.Add(new Token((int)TokenType.Целое_без_знака, TokenType.Целое_без_знака, number1, numberStart, position - 1, position - numberStart));
                }
                else if (currentChar == ',')
                {
                    tokens.Add(new Token((int)TokenType.символ_разделения_параметров,TokenType.символ_разделения_параметров, ",", position,position, 1));
                    position++;
                }
                else if (currentChar == '-')
                {
                    tokens.Add(new Token((int)TokenType.Минус,TokenType.Минус, "-", position, position, 1));
                    position++;
                }
                else if (currentChar == '+')
                {
                    tokens.Add(new Token((int)TokenType.Плюс, TokenType.Плюс, "+", position, position, 1));
                    position++;
                }
                else
                {
                    int numberStart = position;
                    tokens.Add(new Token((int)TokenType.ERROR, TokenType.ERROR, text.Substring(numberStart), position, position,1));
                    position++;
                }
            }
            return tokens;
        }
    
    private enum TokenType
            {
            структура=1,                            // Сomplex
            пробел=4,                               // пробел
            оператор=3,                             // двойные двоиточие
            идетификатор=2,                         // идентификатор
            оператор_присваивания=5,                // оператор присвяивания
            открывающая_скобка=6,                   // открывающая скобка
            Закрывающая_скобка=7,                   // закрывающая скобка
            Целое_без_знака=11,                     // целое без знака
            символ_разделения_параметров = 10,      // символ разделения параметров (,)
            Минус=8,                                // Минус
            Плюс =9,                                // Плюс
                ERROR =12
            }
    class Token
    {
        public int Cod { get; set; }
        public TokenType Type { get; set; }   // Тип
        public string Value { get; set; }     // Аргумент
        public int Column { get; set; }       // последователь 
        public int ColumnNext { get; set; }
        public int quatity { get; set; }      // кол-во символов
        public Token(int cod,TokenType type, string value, int column,int columnNext, int quatity)
        {
            Cod = cod;
            Type = type;
            Value = value;
            Column = column;
            ColumnNext = columnNext;
            this.quatity = quatity;
        }
     }

        //Token Operato = new Token(TokenType.operato,"::",2,2);
        //Token Complex = new Token(TokenType.structure, "COMPLEX",1, 7);
        //Token Probel = new Token(TokenType.probel," ", 2 ,1);
        //Token identificate = new Token(TokenType.idenficakate,"z",5,1);
        //Token Assigment = new Token(TokenType.assigment, "=", 7, 1);
        //Token OpenSkobka = new Token(TokenType.openBroadcastArgument, "(", 9, 1);
        //Token CloaseSkobka = new Token(TokenType.cloaseBroadcastArgument, ")", 16, 1);
        //Token UnintsiunsigmentInt = new Token(TokenType.unintsiunsignedInt, "3", 10, 1);
        //Token SeparatonParam= new Token(TokenType.separationParam, ",", 12, 1);
        //Token separationFractionAndWholes = new Token(TokenType.separationFractionAndWholes, ".", 11, 1);
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
