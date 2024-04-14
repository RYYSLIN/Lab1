using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

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
            изменитьЯзыкToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.L;
            вернутьЯзыкToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.R;
            пускToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.J;
            вызовСправкиToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.P;
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
        public void пускToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scan();
            Pol();
        }

        private void RewriteText()
        {
            string originalText = richTextBox1.Text;

            string pattern = @"\b\w+\b";

            string rewrittenText = Regex.Replace(originalText, pattern, " $&");

            richTextBox1.Clear();
            richTextBox1.AppendText(rewrittenText.Trim());
        }

        private void buttonRewrite_Click(object sender, EventArgs e)
        {
            RewriteText();
        }

        public void Scan()
        {
            string text = richTextBox1.Text;
            List<Token> tokens = LexicalAnalysis(text);

            bool identifierAdded = AddMissingIdentifiers(tokens);

            bool hasAllTypes = HasAllTokenTypes(tokens);

            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Код", typeof(string));
            dataTable.Columns.Add("Тип", typeof(string));
            dataTable.Columns.Add("Значение", typeof(string));
            dataTable.Columns.Add("Начальная Позиция", typeof(int));
            dataTable.Columns.Add("Конечная Позиция", typeof(int));

            foreach (var token in tokens)
            {
                dataTable.Rows.Add(token.Cod, token.Type.ToString(), token.Value, token.Column + 1, token.ColumnNext + 1);
            }

            dataGridView1.DataSource = dataTable;

            DisplayParsedTokens(tokens);

            if (identifierAdded)
            {
                MessageBox.Show("Ваш идентификатор не был найден.\n  Был добавлен 'A'.");
                if (!hasAllTypes)
                {
                    ClearAndDisplayCorrectedText(tokens);
                }
            }

        }

        private bool HasAllTokenTypes(List<Token> tokens)
        {

            foreach (TokenType tokenType in Enum.GetValues(typeof(TokenType)))
            {
                if (!tokens.Exists(t => t.Type == tokenType))
                {
                    return false;
                }
            }
            return true;
        }

        private bool AddMissingIdentifiers(List<Token> tokens)
        {
            if (tokens.Exists(t => t.Type == TokenType.идетификатор))
            {
                return false;
            }

            bool identifierAdded = false;

            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].Type == TokenType.оператор && !tokens.Exists(t => t.Type == TokenType.идетификатор))
                {
                    Token identifierToken = new Token(2, TokenType.идетификатор, "A", tokens[i].ColumnNext, tokens[i].ColumnNext + 1, 2);
                    tokens.Insert(i + 1, identifierToken);
                    identifierAdded = true;
                }
                else if (tokens[i].Type == TokenType.оператор_присваивания && !tokens.Exists(t => t.Type == TokenType.идетификатор))
                {
                    Token identifierToken = new Token(2, TokenType.идетификатор, "A", tokens[i].Column, tokens[i].Column, 2);
                    tokens.Insert(i, identifierToken);
                    identifierAdded = true;
                }
            }

            return identifierAdded;
        }

        private void ClearAndDisplayCorrectedText(List<Token> tokens)
        {
            richTextBox1.Clear();
            foreach (var token in tokens)
            {

                if (token.Value != "missing_identifier")
                {
                    richTextBox1.AppendText(token.Value + "");
                }
            }
        }
        public class HtmlHelper
        {
            public static void OpenInBrowser(string path)
            {
                if (System.IO.File.Exists(path) || IsUrl(path))
                {
                    Process p = new Process();
                    p.StartInfo = new ProcessStartInfo(path) { UseShellExecute = true };
                    p.Start();
                }
            }

            private static bool IsUrl(string input)
            {
                string pattern = @"^(https?|ftp)://[^\s/$.?#].[^\s]*$";
                return Regex.IsMatch(input, pattern);
            }
        }
        private bool IsValidIdentifier(string identifier)
        {
            foreach (char c in identifier)
            {
                if (!char.IsLetterOrDigit(c) && c != '_')
                {
                    return false;
                }
            }
            return true;
        }

        private void CorrectToken(Token token)
        {

            token.Value = "z_" + token.Value;
        }
        public List<Token> LexicalAnalysis(string text)
        {
            List<Token> tokens = new List<Token>();
            int position = 0;

            while (position < text.Length)
            {
                char currentChar = text[position];

                if (position + 1 < text.Length && text.Substring(position, 2) == "::")
                {
                    tokens.Add(new Token((int)TokenType.оператор, TokenType.оператор, "::", position, position + 1, 2));
                    position += 2;

                }
                else if (position + 7 < text.Length && text.Substring(position, 7) == "COMPLEX")
                {
                    tokens.Add(new Token((int)TokenType.структура, TokenType.структура, "COMPLEX", position, position + 5, 7));
                    position += 7;
                }
                else if (text[position] == ' ')
                {
                    tokens.Add(new Token((int)TokenType.пробел, TokenType.пробел, " ", position, position, 1));
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

                    tokens.Add(new Token((int)TokenType.идетификатор, TokenType.идетификатор, identifier, identifierStart, position - 1, position + identifierStart));
                }
                else if (currentChar == '=')
                {
                    tokens.Add(new Token((int)TokenType.оператор_присваивания, TokenType.оператор_присваивания, "=", position, position + 1, 1));
                    position++;
                }
                else if (currentChar == '(')
                {
                    tokens.Add(new Token((int)TokenType.открывающая_скобка, TokenType.открывающая_скобка, "(", position, position, 1));
                    position++;
                }
                else if (currentChar == ')')
                {
                    tokens.Add(new Token((int)TokenType.Закрывающая_скобка, TokenType.Закрывающая_скобка, ")", position, position, 1));
                    position++;
                }
                else if (char.IsDigit(currentChar) || currentChar == '.')
                {

                    bool digitProcessed = false;


                    int numberStart = position;
                    string number = "";

                    while (position < text.Length && (char.IsDigit(text[position]) || text[position] == '.'))
                    {
                        if (text[position] == '.')
                        {
                            position++;
                            if (!char.IsDigit(text[position])) { }
                            while (position < text.Length && char.IsDigit(text[position]))
                            {
                                position++;
                            }

                            if (digitProcessed)
                            {
                                tokens.Add(new Token((int)TokenType.Целое_без_знака2, TokenType.Целое_без_знака2, number, numberStart, position - 1, position - numberStart));
                            }
                            else
                            {

                                number = text.Substring(numberStart, position - numberStart);
                                tokens.Add(new Token((int)TokenType.Целое_без_знака, TokenType.Целое_без_знака, number, numberStart, position - 1, position - numberStart));
                            }

                            digitProcessed = true;
                            break;
                        }

                        position++;
                        digitProcessed = true;
                    }


                    digitProcessed = false;


                    if (!digitProcessed)
                    {
                        number = text.Substring(numberStart, position - numberStart);
                        tokens.Add(new Token((int)TokenType.Целое_без_знака, TokenType.Целое_без_знака, number, numberStart, position - 1, position - numberStart));
                    }
                }

                else if (currentChar == ',')
                {
                    tokens.Add(new Token((int)TokenType.символ_разделения_параметров, TokenType.символ_разделения_параметров, ",", position, position, 1));
                    position++;
                }
                else if (currentChar == '-')
                {
                    tokens.Add(new Token((int)TokenType.Минус, TokenType.Минус, "-", position, position, 1));
                    position++;
                }
                else if (currentChar == '+')
                {
                    tokens.Add(new Token((int)TokenType.Плюс, TokenType.Плюс, "+", position, position, 1));
                    position++;
                }
                else if (currentChar == '*')
                {
                    tokens.Add(new Token((int)TokenType.Умножить, TokenType.Умножить, "*", position, position, 1));
                    position++;
                }
                else if (currentChar == '/')
                {
                    tokens.Add(new Token((int)TokenType.Разделить, TokenType.Разделить, "/", position, position, 1));
                    position++;
                }
                else
                {
                    string currentChar1 = currentChar.ToString();
                    string Error;
                    int numberStart = position;
                    Error = text.Substring(position);

                    tokens.Add(new Token((int)TokenType.ERROR, TokenType.ERROR, currentChar1, numberStart, position, 1));
                    position++;
                }
            }
            return tokens;
        }

        public enum TokenType
        {
            структура = 1,                            // Сomplex
            пробел = 4,                               // пробел
            оператор = 3,                             // двойные двоиточие
            идетификатор = 2,                         // идентификатор
            оператор_присваивания = 5,                // оператор присвяивания
            открывающая_скобка = 6,                   // открывающая скобка
            Закрывающая_скобка = 7,                   // закрывающая скобка
            Целое_без_знака = 11,                     // целое без знака
            символ_разделения_параметров = 10,      // символ разделения параметров (,)
            Минус = 8,                                // Минус
            Плюс = 9,                                // Плюс
            ERROR = 12,
            Целое_без_знака2 = 13,
            Подчеркивание,
            Умножить = 16,    // Оператор умножения
            Разделить = 17   // Оператор деления
        }
        public class Token
        {
            public int Cod { get; set; }
            public TokenType Type { get; set; }   // Тип
            public string Value { get; set; }     // Аргумент
            public int Column { get; set; }       // последователь 
            public int ColumnNext { get; set; }
            public int quatity { get; set; }      // кол-во символов
            public Token(int cod, TokenType type, string value, int column, int columnNext, int quatity)
            {
                Cod = cod;
                Type = type;
                Value = value;
                Column = column;
                ColumnNext = columnNext;
                this.quatity = quatity;
            }
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }



        public class ParsedToken
        {
            public int Number { get; set; }
            public int StartPosition { get; set; } // Начальная позиция токена
            public int EndPosition { get; set; }   // Конечная позиция токена
            public string Info { get; set; }
        }
        public List<Token> tokens;
        private int currentPosition;

        public List<ParsedToken> ParseTokens(List<Token> tokens)
        {
            List<ParsedToken> parsedTokens = new List<ParsedToken>();
            int lineNumber = 1;
            int Scale = 1;
            bool exstuct = true;
            bool expectingIdentifier = false;
            bool expectingOperator = false;
            bool expectingOpenBracket = false;
            bool expectingNumber = false;
            bool expectingNumber2 = false; //Int
            bool expectingComma = false;
            bool expectingCloseBracket = false;
            bool sihn = false;   // =
            bool skipSpace = false;
            bool stuct = false;
            bool Plus = false;
            bool Minus = false;
            bool Space2 = false;
            int d = 0;
            foreach (var token in tokens)
            {
                if (token.Type == TokenType.пробел)
                {
                    if (skipSpace)
                    {
                        parsedTokens.Add(new ParsedToken
                        {
                            Number = lineNumber,
                            StartPosition = token.Column + 1,
                            EndPosition = token.ColumnNext + 1,
                            Info = "Множество пробелов"
                        });
                    }
                    skipSpace = true;
                    continue;
                }
                else
                {
                    skipSpace = false;
                }
                switch (token.Type)
                {
                    case TokenType.структура:
                        if (exstuct)
                        {
                            parsedTokens.Add(new ParsedToken
                            {

                            });

                            stuct = true;
                            exstuct = true;
                            Scale++;
                        }
                        else
                        {

                            parsedTokens.Add(new ParsedToken
                            {
                                Number = lineNumber,
                                StartPosition = token.Column + 1,
                                EndPosition = token.ColumnNext + 1,
                                Info = token.Value
                            });

                            stuct = true;
                            exstuct = true;
                            Scale++;
                        }
                        break;

                    case TokenType.оператор:
                        if (stuct)
                        {
                            parsedTokens.Add(new ParsedToken
                            {

                            });

                            stuct = true;
                            expectingIdentifier = true;
                            Scale++;

                        }
                        else
                        {

                            parsedTokens.Add(new ParsedToken
                            {
                                Number = lineNumber,
                                StartPosition = token.Column + 1,
                                EndPosition = token.ColumnNext + 1,
                                Info = token.Value
                            }); ;

                            stuct = true;
                            expectingIdentifier = true;
                        }
                        break;

                    case TokenType.идетификатор:

                        if (expectingIdentifier)
                        {
                            parsedTokens.Add(new ParsedToken
                            {

                            });
                            expectingIdentifier = false;
                            expectingOperator = false;
                            sihn = true;
                            Scale++;

                        }
                        else
                        {

                            parsedTokens.Add(new ParsedToken
                            {
                                Number = lineNumber,
                                StartPosition = token.Column + 1,
                                EndPosition = token.ColumnNext + 1,
                                Info = token.Value
                            });

                            expectingOperator = false;
                            sihn = true;
                        }
                        break;

                    case TokenType.оператор_присваивания:
                        if (sihn)
                        {

                            parsedTokens.Add(new ParsedToken
                            {



                            });

                            Scale++;
                            sihn = false;
                            expectingIdentifier = false;
                            expectingOpenBracket = true;
                        }
                        else
                        {
                            parsedTokens.Add(new ParsedToken
                            {
                                Number = lineNumber++,
                                StartPosition = token.Column + 1,
                                EndPosition = token.ColumnNext + 1,
                                Info = token.Value
                            });
                            currentPosition += token.Value.Length;

                            expectingIdentifier = false;
                            expectingOpenBracket = true;
                        }
                        break;
                    case TokenType.открывающая_скобка:
                        if (expectingOpenBracket)
                        {

                            parsedTokens.Add(new ParsedToken
                            {



                            });

                            Scale++;
                            expectingOpenBracket = false;
                            expectingNumber = true;
                            sihn = false;
                            Minus = true;
                            Plus = true;
                        }
                        else
                        {

                            parsedTokens.Add(new ParsedToken
                            {
                                Number = lineNumber++,
                                StartPosition = token.Column + 1,
                                EndPosition = token.ColumnNext + 1,
                                Info = token.Value

                            });

                            expectingNumber = true;
                            sihn = false;
                            Minus = true;
                            Plus = true;
                        }
                        break;
                    case TokenType.Минус:
                        {
                            if (Minus)
                            {
                                parsedTokens.Add(new ParsedToken
                                {



                                });

                                Scale++;
                                Minus = false;
                                expectingNumber = true;
                                expectingNumber2 = true;
                            }

                            else
                            {
                                parsedTokens.Add(new ParsedToken
                                {
                                    Number = lineNumber++,
                                    StartPosition = token.Column + 1,
                                    EndPosition = token.ColumnNext + 1,
                                    Info = token.Value
                                });

                                expectingNumber = true;
                                expectingNumber2 = true;
                            }
                        }
                        break;

                    case TokenType.Плюс:
                        {
                            if (Plus)
                            {
                                parsedTokens.Add(new ParsedToken
                                {



                                });

                                Scale++;
                                Plus = false;
                                expectingNumber = true;
                                expectingNumber2 = true;
                            }

                            else
                            {
                                parsedTokens.Add(new ParsedToken
                                {
                                    Number = lineNumber++,
                                    StartPosition = token.Column + 1,
                                    EndPosition = token.ColumnNext + 1,
                                    Info = token.Value
                                });

                                expectingNumber = true;
                                expectingNumber2 = true;
                            }
                        }
                        break;
                    case TokenType.Целое_без_знака:
                        if (expectingNumber)
                        {

                            parsedTokens.Add(new ParsedToken
                            {



                            });

                            Scale++;
                            expectingNumber = false;
                            expectingOpenBracket = false;
                            expectingComma = true;
                        }
                        else
                        {

                            parsedTokens.Add(new ParsedToken
                            {
                                Number = lineNumber++,
                                StartPosition = token.Column + 1,
                                EndPosition = token.ColumnNext + 1,
                                Info = token.Value

                            });

                            expectingOpenBracket = false;
                            expectingComma = true;
                        }
                        break;
                    case TokenType.символ_разделения_параметров:
                        if (expectingComma)
                        {

                            parsedTokens.Add(new ParsedToken
                            {


                            });

                            Scale++;
                            expectingComma = false;
                            expectingNumber = false;
                            expectingNumber2 = true;
                            Minus = true;
                            Plus = true;
                        }
                        else
                        {

                            parsedTokens.Add(new ParsedToken
                            {
                                Number = lineNumber++,
                                StartPosition = token.Column + 1,
                                EndPosition = token.ColumnNext + 1,
                                Info = token.Value

                            });

                            expectingNumber = false;
                            expectingNumber2 = true;
                            Minus = true;
                            Plus = true;
                        }
                        break;
                    case TokenType.Целое_без_знака2:
                        if (expectingNumber2)
                        {
                            expectingNumber = true;

                            parsedTokens.Add(new ParsedToken
                            {

                            });

                            Scale++;
                            expectingNumber2 = false;
                            expectingComma = false;
                            expectingCloseBracket = true;

                        }
                        else
                        {

                            parsedTokens.Add(new ParsedToken
                            {
                                Number = lineNumber++,
                                StartPosition = token.Column + 1,
                                EndPosition = token.ColumnNext + 1,
                                Info = token.Value
                            });

                            expectingComma = false;
                            expectingCloseBracket = true;
                        }
                        break;
                    case TokenType.Закрывающая_скобка:

                        if (expectingCloseBracket)
                        {
                            parsedTokens.Add(new ParsedToken
                            {



                            });

                            Scale++;
                            expectingNumber2 = false;
                            expectingCloseBracket = false;
                            exstuct = false;

                        }
                        else
                        {

                            parsedTokens.Add(new ParsedToken
                            {
                                Number = lineNumber++,
                                StartPosition = token.Column + 1,
                                EndPosition = token.ColumnNext + 1,
                                Info = token.Value
                            });
                            exstuct = true;
                            expectingNumber2 = false;
                            expectingCloseBracket = false;
                        }
                        break;
                    default:
                        if (Scale <= 10 || Scale == 11 || Scale == 12)
                        {
                            parsedTokens.Add(new ParsedToken
                            {
                                Number = lineNumber++,
                                StartPosition = token.Column + 1,
                                EndPosition = token.ColumnNext + 1,
                                Info = $"Ошибка {token.Value}"
                            });
                            currentPosition -= token.Value.Length;
                            break;
                        }
                        else break;
                }
            }

            return parsedTokens;
        }

        public void DisplayParsedTokens(List<Token> tokens)
        {
            List<ParsedToken> parsedTokens = ParseTokens(tokens);

            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.Columns.Clear();

            dataGridView2.Columns.Add("Number", "№");
            dataGridView2.Columns.Add("Location", "Местоположение");
            dataGridView2.Columns.Add("Info", "Неверный фрагмент");


            dataGridView2.Columns["Number"].ValueType = typeof(int);
            dataGridView2.Columns["Location"].ValueType = typeof(string);
            dataGridView2.Columns["Info"].ValueType = typeof(string);

            int classCount = 0;

            int number = 1;
            foreach (var token in parsedTokens)
            {
                string location;
                if (!string.IsNullOrEmpty(token.Info))
                {

                    location = $"Находится от {token.StartPosition} символа до {token.EndPosition} символа";
                    dataGridView2.Rows.Add(number, location, token.Info);
                    number++;
                    classCount++;

                }
            }

            dataGridView2.Columns["Number"].HeaderText = $"№ (Ошибок: {classCount})";
        }

        private void изменитьШрифтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                // Получаем выбранный шрифт из диалога
                Font selectedFont = fontDialog1.Font;

                // Устанавливаем выбранный шрифт для элемента управления (например, для TextBox)
                richTextBox1.Font = selectedFont;

                // Устанавливаем выбранный шрифт для всех столбцов DataGridView
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    row.DefaultCellStyle.Font = selectedFont;
                }
            }
        }

        private void fontDialog1_Apply(object sender, EventArgs e)
        {

        }

        private string previousLanguage = "";

        private void изменитьЯзыкToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Сохраняем текущий язык
            previousLanguage = Thread.CurrentThread.CurrentUICulture.Name;

            // Установка нового языка, например, японского
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("ja");

            // Обновляем тексты элементов управления
            UpdateUI();
        }


        private void UpdateUI1()
        {

       
            файлToolStripMenuItem.Text =Resource1.File;
            вставитьToolStripMenuItem.Text = Resource1.CNTRLV;
            методАнализаToolStripMenuItem.Text = Resource1.MetodAnaliza;
            классификацияГрамматикиToolStripMenuItem.Text =Resource1.KlassiGRAMMATIC;
            копироватьToolStripMenuItem.Text =Resource1.CNTRLC;
            правкаToolStripMenuItem.Text = Resource1.Pravka;
            текстToolStripMenuItem.Text = Resource1.TXT;
            пускToolStripMenuItem.Text = Resource1.Pusk;
            справкаToolStripMenuItem.Text = Resource1.Cpravka;
            оПрограммеToolStripMenuItem.Text= Resource1.Oprogamme;
            видToolStripMenuItem.Text = Resource1.VID;
            изменитьШрифтToolStripMenuItem.Text = Resource1.SHRIFT;
            изменитьЯзыкToolStripMenuItem.Text =Resource1.Langvich;
            создатьToolStripMenuItem.Text = Resource1.Cozdat;
            открытьToolStripMenuItem.Text = Resource1.Open;
            сохранитьToolStripMenuItem.Text =Resource1.Safe;
            сохранитьКакToolStripMenuItem.Text = Resource1.SafeKak;
            выходToolStripMenuItem.Text = Resource1.EXIT;
            отменитьToolStripMenuItem.Text = Resource1.Otmena;
            повторитьToolStripMenuItem.Text = Resource1.Povtor;
            выделитьВсеToolStripMenuItem.Text = Resource1.Videlit;
            удалитьToolStripMenuItem.Text = Resource1.Delet;
            постановкаЗадачиToolStripMenuItem.Text = Resource1.Zadacha;
            грамматикаToolStripMenuItem.Text = Resource1.Grammatiic;
            диагностикаToolStripMenuItem.Text = Resource1.Diagnostika;
            списокЛитературыToolStripMenuItem.Text = Resource1.CpicokLIter;
            текстовыйToolStripMenuItem.Text = Resource1.PrimerTEXT;
            исходныйКодПрограммыToolStripMenuItem.Text = Resource1.KODISVOD;
            tabPage1.Text = Resource1.Ckaner;
            tabPage2.Text =Resource1.Parser;
            tabPage3.Text = Resource1.Vivod;
            вырезатьToolStripMenuItem.Text = Resource1.VIREZAT;
            вернутьЯзыкToolStripMenuItem.Text = Resource1.Vernutiazik;
            вызовСправкиToolStripMenuItem.Text = Resource1.VizovCPravki;
           
            ComponentResourceManager resources = new ComponentResourceManager(this.GetType());
            resources.ApplyResources(this, "$this");
            foreach (Control control in this.Controls)
            {
                resources.ApplyResources(control, control.Name);
                if (control is ToolStrip)
                {
                    foreach (ToolStripItem item in ((ToolStrip)control).Items)
                    {
                        resources.ApplyResources(item, item.Name);
                    }
                }
            }
        }
        private void UpdateUI()
        {


            файлToolStripMenuItem.Text = Properties.Resources.File;
            вставитьToolStripMenuItem.Text = Properties.Resources.CNTRLV;
            методАнализаToolStripMenuItem.Text = Properties.Resources.MetodAnaliza;
            классификацияГрамматикиToolStripMenuItem.Text = Properties.Resources.KlassiGRAMMATIC;
            копироватьToolStripMenuItem.Text = Properties.Resources.CNTRLC;
            правкаToolStripMenuItem.Text = Properties.Resources.Pravka;
            текстToolStripMenuItem.Text = Properties.Resources.TXT;
            пускToolStripMenuItem.Text = Properties.Resources.Pusk;
            справкаToolStripMenuItem.Text = Properties.Resources.Cpravka;
            оПрограммеToolStripMenuItem.Text = Properties.Resources.Oprogamme;
            видToolStripMenuItem.Text = Properties.Resources.VID;
            изменитьШрифтToolStripMenuItem.Text = Properties.Resources.SHRIFT;
            изменитьЯзыкToolStripMenuItem.Text = Properties.Resources.Langvich;
            создатьToolStripMenuItem.Text = Properties.Resources.Cozdat;
            открытьToolStripMenuItem.Text = Properties.Resources.Open;
            сохранитьToolStripMenuItem.Text = Properties.Resources.Safe;
            сохранитьКакToolStripMenuItem.Text = Properties.Resources.SafeKak;
            выходToolStripMenuItem.Text = Properties.Resources.EXIT;
            отменитьToolStripMenuItem.Text = Properties.Resources.Otmena;
            повторитьToolStripMenuItem.Text = Properties.Resources.Povtor;
            выделитьВсеToolStripMenuItem.Text = Properties.Resources.Videlit;
            удалитьToolStripMenuItem.Text = Properties.Resources.Delet;
            постановкаЗадачиToolStripMenuItem.Text = Properties.Resources.Zadacha;
            грамматикаToolStripMenuItem.Text = Properties.Resources.Grammatiic;
            диагностикаToolStripMenuItem.Text = Properties.Resources.Diagnostika;
            списокЛитературыToolStripMenuItem.Text = Properties.Resources.CpicokLIter;
            текстовыйToolStripMenuItem.Text = Properties.Resources.PrimerTEXT;
            исходныйКодПрограммыToolStripMenuItem.Text = Properties.Resources.KODISVOD;
            tabPage1.Text = Properties.Resources.Ckaner;
            tabPage2.Text = Properties.Resources.Parser;
            tabPage3.Text = Properties.Resources.Vivod;
            вырезатьToolStripMenuItem.Text = Properties.Resources.VIREZAT;
            вернутьЯзыкToolStripMenuItem.Text = Properties.Resources.Vernutiazik;
            вызовСправкиToolStripMenuItem.Text = Properties.Resources.VizovCPravki;
            ComponentResourceManager resources = new ComponentResourceManager(this.GetType());
            resources.ApplyResources(this, "$this");
            foreach (Control control in this.Controls)
            {
                resources.ApplyResources(control, control.Name);
                if (control is ToolStrip)
                {
                    foreach (ToolStripItem item in ((ToolStrip)control).Items)
                    {
                        resources.ApplyResources(item, item.Name);
                    }
                }
            }
        }
        private void вернутьЯзыкToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(previousLanguage))
            {
                // Устанавливаем предыдущий язык
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(previousLanguage);

                // Обновляем тексты элементов управления
                UpdateUI1();
            }
            else
            {
                // Если предыдущий язык не сохранен, устанавливаем язык по умолчанию
                Thread.CurrentThread.CurrentUICulture = CultureInfo.DefaultThreadCurrentUICulture;

                // Обновляем тексты элементов управления
                UpdateUI1();
            }
        }

        private void постановкаЗадачиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Task();
        }
        private void Task()
        {

            HtmlHelper.OpenInBrowser("Properties\\Task.html\"");
        }

        private void грамматикаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gramm();
        }
        private void Gramm()
        {

            HtmlHelper.OpenInBrowser("Properties\\Grammatic.html\"");
        }

        private void классификацияГрамматикиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KlassicGramm();
        }
        private void KlassicGramm()
        {

            HtmlHelper.OpenInBrowser("Properties\\ClassifGRAMM.html\"");
        }

        private void методАнализаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HtmlHelper.OpenInBrowser("Properties\\MetodAnaliz.html\"");
        }

        private void диагностикаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Diagnostika();
        }
        private void Diagnostika()
        {

            HtmlHelper.OpenInBrowser("Properties\\Netralization.html\"");
        }

        private void текстовыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TPROMER();
        }
        private void TPROMER()
        {

            HtmlHelper.OpenInBrowser("Properties\\TEST.html\"");
        }

        private void списокЛитературыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LITER();
        }
        private void LITER()
        {

            HtmlHelper.OpenInBrowser("Properties\\LITER.html");
        }

        private void исходныйКодПрограммыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KAODPROG();
        }
        private void KAODPROG()
        {
            HtmlHelper.OpenInBrowser("Properties\\KOD.html");
        }
        private void Pol()
        {
            string inputExpression = richTextBox1.Text;

            if (string.IsNullOrEmpty(inputExpression))
            {
                MessageBox.Show("Выражение пусто!");
                return;
            }

            // Получаем токены из лексического анализатора
            List<Token> tokens = LexicalAnalysis(inputExpression);

            // Преобразуем выражение в ПОЛИЗ
            List<string> poliz = ConvertToPoliz(tokens);

            // Выводим полученный ПОЛИЗ в richTextBox2
            richTextBox2.Text = string.Join(" ", poliz);
        }

        List<string> ConvertToPoliz(List<Token> tokens)
        {
            List<string> poliz = new List<string>();
            Stack<Token> stack = new Stack<Token>();

            foreach (Token token in tokens)
            {
                switch (token.Type)
                {
                    case TokenType.Целое_без_знака:
                        poliz.Add(token.Value);
                        break;
                    case TokenType.Плюс:
                    case TokenType.Минус:
                    case TokenType.Умножить:
                    case TokenType.Разделить:
                        while (stack.Any() && GetPriority(stack.Peek().Type) >= GetPriority(token.Type))
                        {
                            poliz.Add(stack.Pop().Value);
                        }
                        stack.Push(token);
                        break;
                    case TokenType.открывающая_скобка:
                        stack.Push(token);
                        break;
                    case TokenType.Закрывающая_скобка:
                        while (stack.Peek().Type != TokenType.открывающая_скобка)
                        {
                            poliz.Add(stack.Pop().Value);
                        }
                        stack.Pop(); // Удаляем открывающую скобку
                        break;
                }
            }

            while (stack.Any())
            {
                poliz.Add(stack.Pop().Value);
            }

            return poliz;
        }

        private static int GetPriority(TokenType type)
        {
            switch (type)
            {
                case TokenType.Плюс:
                case TokenType.Минус:
                    return 1;
                case TokenType.Умножить:
                case TokenType.Разделить:
                    return 2;
                default:
                    return 0;
            }
        }

        class Parser
        {
            private readonly List<Token> tokens;
            private int currentPosition;

            public Parser(List<Token> tokens)
            {
                this.tokens = tokens;
                currentPosition = 0;
            }

            public void Parse()
            {
                E();
                if (currentPosition != tokens.Count)
                {
                    throw new Exception("Unexpected token at the end of expression");
                }
            }

            private void E()
            {
                T();
                A();
            }

            private void A()
            {
                if (currentPosition < tokens.Count && (tokens[currentPosition].Type == TokenType.Плюс || tokens[currentPosition].Type == TokenType.Минус))
                {
                    Match(tokens[currentPosition].Type);
                    T();
                    A();
                }
            }

            private void T()
            {
                O();
                B();
            }

            private void B()
            {
                if (currentPosition < tokens.Count && (tokens[currentPosition].Type == TokenType.Умножить || tokens[currentPosition].Type == TokenType.Разделить))
                {
                    Match(tokens[currentPosition].Type);
                    O();
                    B();
                }
            }

            private void O()
            {
                if (currentPosition < tokens.Count)
                {
                    if (tokens[currentPosition].Type == TokenType.Целое_без_знака || tokens[currentPosition].Type == TokenType.открывающая_скобка)
                    {
                        Match(tokens[currentPosition].Type);
                    }
                    else
                    {
                        throw new Exception("Unexpected token in expression");
                    }
                }
                else
                {
                    throw new Exception("Unexpected end of expression");
                }
            }

            private void Match(TokenType expectedToken)
            {
                if (currentPosition < tokens.Count && tokens[currentPosition].Type == expectedToken)
                {
                    currentPosition++;
                }
                else
                {
                    throw new Exception($"Unexpected token: {tokens[currentPosition].Value}");
                }
            }
        }
    }
 }
