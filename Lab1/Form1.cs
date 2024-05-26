using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Lab1.Form1;
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
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
            пускToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.Q;
            вызовСправкиToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.P;
            лаборатрнаяРабота6ToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.W;
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
        private bool buttonClicked = false;
        public void пускToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttonClicked = true;
            //FindWordsEndingWithT(); //9
            //FindWordsEndingWithT2(); //17
            //FindTimes();//23
            // 9 17 23
            Scan(); //  ультратоповый парсер 
                    //  Pol(); // 5 лвба

        }
        int Ckd = 0;
        int zcd = 0;
        public void Scan()
        {
            string text = richTextBox1.Text;
            List<Token> tokens = LexicalAnalysis(text);

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
                if (token.Cod == (int)TokenType.Закрывающая_скобка)
                {
                    zcd++;
                }
                if (token.Cod == (int)TokenType.открывающая_скобка)
                {
                    Ckd++;
                }
                dataTable.Rows.Add(token.Cod, token.Type.ToString(), token.Value, token.Column + 1, token.ColumnNext + 1);
            }
            dataGridView1.DataSource = dataTable;

            // Создаем экземпляр парсера
            Parser parser = new Parser();
            // Парсим токены
            //List<ParsedToken> parsedTokens = parser.ParseTokens(tokens);
            //List<ParsedToken> parsedTokens = parser.ParseTokens(tokens);
            //// Отображаем результат парсинга
            //DisplayParsedTokens(parsedTokens);
            List<ParsedToken> parsedTokens = parser.ParseTokens(tokens);

            DisplayParsedTokens(parsedTokens);
            Ckd = 0;
            zcd = 0;
            //// Отображаем ошибки парсинга
            //DisplayParseErrors(parser.GetParseErrors());
        }

        public class HtmlHelper
        {
            public static void OpenInBrowser(string path)
            {
                if (System.IO.File.Exists(path) || IsUrl(path))
                {
                    Process p = new Process
                    {
                        StartInfo = new ProcessStartInfo(path) { UseShellExecute = true }
                    };
                    p.Start();
                }
            }

            private static bool IsUrl(string input)
            {
                string pattern = @"^(https?|ftp)://[^\s/$.?#].[^\s]*$";
                return Regex.IsMatch(input, pattern);
            }
        }
        public List<Token> LexicalAnalysis(string text)
        {
            List<Token> tokens = new List<Token>();
            int position = 0;
            if (buttonClicked)
            {
                while (position < text.Length)
                {
                    char currentChar = text[position];

                    if (char.IsWhiteSpace(currentChar)) // Пропускаем пробелы
                    {
                        position++;
                        continue;
                    }

                    if (position + 1 < text.Length && text.Substring(position, 2) == "::")
                    {
                        tokens.Add(new Token((int)TokenType.оператор, TokenType.оператор, "::", position, position + 1, 2));
                        position += 2;
                    }
                    else if (position + 7 < text.Length && text.Substring(position, 7) == "COMPLEX")
                    {
                        tokens.Add(new Token((int)TokenType.Ключевое_слово, TokenType.Ключевое_слово, "COMPLEX", position, position + 5, 7));
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
                        tokens.Add(new Token((int)TokenType.идетификатор, TokenType.идетификатор, identifier, identifierStart, position - 1, position - identifierStart));
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


                    else if (char.IsDigit(currentChar) || currentChar == '-' || currentChar == '+')
                    {
                        // Флаг наличия знака перед числом
                        bool hasSign = currentChar == '-' || currentChar == '+';
                        char sign = ' ';
                        if (hasSign)
                        {
                            sign = currentChar;
                            position++;
                        }

                        int numberStart = position;
                        while (position < text.Length && (char.IsDigit(text[position]) || text[position] == '.'))
                        {
                            position++;
                        }

                        // Определение типа числа
                        int length = position - numberStart;
                        string number = text.Substring(numberStart, length);
                        TokenType numberType = TokenType.Целое_без_знака;

                        tokens.Add(new Token((int)numberType, numberType, number, numberStart, position - 1, length));
                    }



                    else if (char.IsDigit(currentChar))
                    {
                        // Флаг наличия знака перед числом


                        int numberStart = position;
                        while (position < text.Length && (char.IsDigit(text[position]) || text[position] == '.'))
                        {
                            position++;
                        }

                        // Определение типа числа
                        int length = position - numberStart;
                        string number = text.Substring(numberStart, length);
                        TokenType numberType = TokenType.Целое_без_знака;

                        tokens.Add(new Token((int)numberType, numberType, number, numberStart, position - 1, length));
                    }

                    else if (currentChar == ',')
                    {
                        tokens.Add(new Token((int)TokenType.символ_разделения_параметров, TokenType.символ_разделения_параметров, ",", position, position, 1));
                        position++;
                    }
                    else if (currentChar == ';')
                    {
                        tokens.Add(new Token((int)TokenType.закрывающий_оператор, TokenType.закрывающий_оператор, ";", position, position, 1));
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
            else
            {
                while (position < text.Length)
                {
                    if (char.IsWhiteSpace(text[position]))
                    {
                        // Пропускаем пробелы и другие пробельные символы
                        position++;
                        continue;
                    }
                    char currentChar = text[position];



                    if (position + 5 < text.Length && text.Substring(position, 5) == "begin")
                    {
                        tokens.Add(new Token((int)TokenType.структура1, TokenType.структура1, "begin", position, position + 3, 7));
                        position += 5;
                    }
                    else if (position + 3 < text.Length && text.Substring(position, 3) == "end")
                    {
                        tokens.Add(new Token((int)TokenType.структура2, TokenType.структура2, "end", position, position + 1, 7));
                        position += 3;
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
                        tokens.Add(new Token((int)TokenType.идетификатор, TokenType.идетификатор, identifier, identifierStart, position - 1, position - identifierStart));
                    }
                    else if (position + 1 < text.Length && text.Substring(position, 2) == ":=")
                    {
                        tokens.Add(new Token((int)TokenType.оператор_присваивания, TokenType.оператор_присваивания, ":=", position, position + 1, 1));
                        position += 2;
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
                    else if (currentChar == ';')
                    {
                        tokens.Add(new Token((int)TokenType.закрывающий_оператор, TokenType.закрывающий_оператор, ";", position, position, 1));
                        position++;
                    }
                    else if (char.IsDigit(currentChar))
                    {
                        // Флаг наличия знака перед числом
                        int numberStart = position;
                        while (position < text.Length && (char.IsDigit(text[position]) || text[position] == '.'))
                        {
                            position++;
                        }

                        // Определение типа числа
                        int length = position - numberStart;
                        string number = text.Substring(numberStart, length);
                        TokenType numberType = TokenType.Целое_без_знака;

                        tokens.Add(new Token((int)numberType, numberType, number, numberStart, position - 1, length));
                    }
                    else if (currentChar == ',')
                    {
                        tokens.Add(new Token((int)TokenType.символ_разделения_параметров, TokenType.символ_разделения_параметров, ",", position, position, 1));
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
                    //Для 5 лабы + и -
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
        }
        public enum TokenType
        {
            Ключевое_слово = 1,
            пробел = 4,
            оператор = 3,
            идетификатор = 2,
            оператор_присваивания = 5,
            открывающая_скобка = 6,
            Закрывающая_скобка = 7,
            Целое_без_знака = 11,
            символ_разделения_параметров = 10,
            Минус = 8,
            Плюс = 9,
            ERROR = 12,
            Целое_без_знака2 = 13,
            Подчеркивание,
            Умножить = 16,
            Разделить = 17,
            недостающий_идентификатор,
            закрывающий_оператор,
            структура1,
            структура2

        }

        public class Token
        {
            public int Cod { get; set; }
            public TokenType Type { get; set; }
            public string Value { get; set; }
            public int Column { get; set; }
            public int ColumnNext { get; set; }
            public int quatity { get; set; }
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

        public class ParsedToken
        {
            public int Number { get; set; }
            public int StartPosition { get; set; }
            public int EndPosition { get; set; }
            public string Info { get; set; }

            public ParsedToken(int num, int st, int end, string info)
            {
                Number = num;
                StartPosition = st;
                EndPosition = end;
                Info = info;
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






        public class Parser
        {
            public List<ParsedToken> ParseTokens(List<Token> tokens)
            {
                List<ParsedToken> parsedTokens = new List<ParsedToken>();
                int lineNumber = 1;
                int tokenIndex = 0; // Индекс ожидаемого токена

                // Список для хранения всех найденных ошибок
                List<Token> errorTokens = new List<Token>();

                // Словарь для хранения флагов обработанных типов токенов
                Dictionary<TokenType, bool> processedTypes = new Dictionary<TokenType, bool>();

                // Массив ожидаемой последовательности токенов
                TokenType[] expectedSequence = new TokenType[]
                {
        TokenType.Ключевое_слово,
        TokenType.оператор,
        TokenType.идетификатор,
        TokenType.оператор_присваивания,
        TokenType.открывающая_скобка,
        TokenType.Целое_без_знака,
        TokenType.символ_разделения_параметров,
        TokenType.Целое_без_знака,
        TokenType.Закрывающая_скобка,
        TokenType.закрывающий_оператор
                };

                // Проходим по ожидаемой последовательности токенов
                foreach (TokenType expectedToken in expectedSequence)
                {
                    // Проверяем, была ли уже найдена ошибка для данного типа токена
                    if (processedTypes.ContainsKey(expectedToken) && processedTypes[expectedToken])
                    {
                        continue; // Пропускаем обработку, если уже была найдена ошибка для данного типа токена
                    }

                    bool tokenFound = false;
                    foreach (Token token in tokens)
                    {
                        if (token.Type == expectedToken)
                        {
                            tokenFound = true;
                            break;
                        }
                    }

                    // Если токен не найден, добавляем ошибку с примерными позициями
                    if (!tokenFound)
                    {
                        // Примерные позиции
                        int startPosition = tokens.Count > 0 ? tokens[0].Column : 0;
                        int endPosition = tokens.Count > 0 ? tokens[tokens.Count - 1].ColumnNext + 1 : 0;
                        parsedTokens.Add(new ParsedToken(0, startPosition, endPosition, "Отсутствует ожидаемый токен: " + expectedToken.ToString()));
                        // Устанавливаем флаг ошибки для данного типа токена
                        processedTypes[expectedToken] = true;
                    }
                }

                // Добавляем примерные позиции для токенов типа ERROR
                foreach (Token token in tokens)
                {
                    if (token.Type == TokenType.ERROR)
                    {
                        // Если токен типа ERROR и не является ":" (по условиям вашего кода)
                        if (token.Value != ":")
                        {
                            int startPosition = token.Column;
                            int endPosition = token.ColumnNext + 1;
                            parsedTokens.Add(new ParsedToken(0, startPosition, endPosition, "Ошибка: " + token.Value));
                        }
                    }

                    // Помечаем тип токена как обработанный
                    processedTypes[token.Type] = true;
                }

                return parsedTokens;
            }


        }

        public void DisplayParsedTokens(List<ParsedToken> parsedTokens)
        {
            // Проверка на null перед обращением к dataGridView2
            if (dataGridView2 == null)
            {
                // Обработка случая, когда dataGridView2 не инициализирован
                return;
            }

            dataGridView2.AutoGenerateColumns = false;

            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();
            if (dataGridView2.Columns.Count == 0)
            {
                dataGridView2.Columns.Add("Number", "№");
                dataGridView2.Columns.Add("Location", "Местоположение");
                dataGridView2.Columns.Add("Info", "Неверный фрагмент");

                dataGridView2.Columns["Number"].ValueType = typeof(int);
                dataGridView2.Columns["Location"].ValueType = typeof(string);
                dataGridView2.Columns["Info"].ValueType = typeof(string);
            }
            if (((Ckd >= 2) && Ckd > zcd && zcd > 0) || (Ckd & zcd) > 1)

            {
                parsedTokens.Add(new ParsedToken(0, 0, 0, "лишняя открытая скобочка"));

            }
            if (((Ckd > 0) && Ckd < zcd && zcd >= 2) || (Ckd & zcd) > 1)
            {
                parsedTokens.Add(new ParsedToken(0, 0, 0, "лишняя закрытая скобочка"));

            }
            // Добавляем строки с данными
            int rowNum = 1;
            foreach (var token in parsedTokens)
            {
                string location = $"От {token.StartPosition} до {token.EndPosition}";
                dataGridView2.Rows.Add(rowNum++, location, token.Info);
            }
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


            файлToolStripMenuItem.Text = Resource1.File;
            вставитьToolStripMenuItem.Text = Resource1.CNTRLV;
            методАнализаToolStripMenuItem.Text = Resource1.MetodAnaliza;
            классификацияГрамматикиToolStripMenuItem.Text = Resource1.KlassiGRAMMATIC;
            копироватьToolStripMenuItem.Text = Resource1.CNTRLC;
            правкаToolStripMenuItem.Text = Resource1.Pravka;
            текстToolStripMenuItem.Text = Resource1.TXT;
            пускToolStripMenuItem.Text = Resource1.Pusk;
            справкаToolStripMenuItem.Text = Resource1.Cpravka;
            оПрограммеToolStripMenuItem.Text = Resource1.Oprogamme;
            видToolStripMenuItem.Text = Resource1.VID;
            изменитьШрифтToolStripMenuItem.Text = Resource1.SHRIFT;
            изменитьЯзыкToolStripMenuItem.Text = Resource1.Langvich;
            создатьToolStripMenuItem.Text = Resource1.Cozdat;
            открытьToolStripMenuItem.Text = Resource1.Open;
            сохранитьToolStripMenuItem.Text = Resource1.Safe;
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
            tabPage2.Text = Resource1.Parser;
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
        private void OpenHtmlFileButton_Click(object sender, EventArgs e)
        {
            string htmlFilePath = @"C:\path\to\your\file.html"; // Путь к вашему HTML-файлу
            OpenInBrowser(htmlFilePath);
        }

        private static void OpenInBrowser(string path)
        {
            if (System.IO.File.Exists(path) || IsUrl(path))
            {
                Process p = new Process
                {
                    StartInfo = new ProcessStartInfo(path) { UseShellExecute = true }
                };
                p.Start();
            }
            else
            {
                MessageBox.Show("Файл не найден или неверный URL.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static bool IsUrl(string input)
        {
            string pattern = @"^(https?|ftp)://[^\s/$.?#].[^\s]*$";
            return System.Text.RegularExpressions.Regex.IsMatch(input, pattern);
        }
        private void Task()
        {

            OpenInBrowser(@"C:\Users\ryysl\source\repos\Lab1\Lab1\Properties\Task.html");
        }
        private void грамматикаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gramm();
        }
        private void Gramm()
        {

            OpenInBrowser(@"C:\Users\ryysl\source\repos\Lab1\Lab1\Properties\Grammatic.html");
        }
        private void классификацияГрамматикиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KlassicGramm();
        }
        private void KlassicGramm()
        {

            OpenInBrowser(@"C:\Users\ryysl\source\repos\Lab1\Lab1\Properties\ClassifGRAMM.html");
        }
        private void методАнализаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenInBrowser(@"C:\Users\ryysl\source\repos\Lab1\Lab1\Properties\MetodAnaliz.html");
        }
        private void диагностикаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Diagnostika();
        }
        private void Diagnostika()
        {

            OpenInBrowser(@"C:\\Users\\ryysl\\source\\repos\\Lab1\\Lab1\\Properties\\Netralization.html");
        }
        private void текстовыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TPROMER();
        }
        private void TPROMER()
        {
            richTextBox1.Text = "COMPLEX ::z=(2.0,3.0);";

        }
        private void списокЛитературыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LITER();
        }
        private void LITER()
        {

            OpenInBrowser(@"C:\Users\ryysl\source\repos\Lab1\Lab1\Properties\LITER.html");
        }
        private void исходныйКодПрограммыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KAODPROG();
        }
        private void KAODPROG()
        {
            OpenInBrowser(@"C:\Users\ryysl\source\repos\Lab1\Lab1\Properties\KOD.html");
        }
        private void Pol()
        {
            dataGridView2.Rows.Clear();
            string inputExpression = richTextBox1.Text;

            if (string.IsNullOrEmpty(inputExpression))
            {
                MessageBox.Show("Выражение пусто!");
                return;
            }

            List<Token> tokens = LexicalAnalysis(inputExpression);

            List<string> poliz = ConvertToPoliz(tokens);

            if (poliz == null)
                return;

            richTextBox2.Text = string.Join(" ", poliz);
        }
        private List<string> ConvertToPoliz(List<Token> tokens)
        {
            List<string> poliz = new List<string>();
            Stack<Token> stack = new Stack<Token>();
            int openingBrackets = 0;
            int closingBrackets = 0;

            TokenType[] operators = { TokenType.Плюс, TokenType.Минус, TokenType.Умножить, TokenType.Разделить };

            for (int i = 0; i < tokens.Count; i++)
            {
                Token token = tokens[i];

                switch (token.Type)
                {
                    case TokenType.Целое_без_знака:
                        poliz.Add(token.Value);
                        break;
                    case TokenType.Плюс:
                    case TokenType.Минус:
                    case TokenType.Умножить:
                    case TokenType.Разделить:
                        if (i == 0 || i == tokens.Count - 1 || operators.Contains(tokens[i - 1].Type) || operators.Contains(tokens[i + 1].Type))
                        {
                            DisplayErrors(new List<string> { "Ошибка: Лишний оператор." });
                            return null;
                        }

                        while (stack.Any() &&
                               stack.Peek().Type != TokenType.открывающая_скобка &&
                               GetPriority(stack.Peek().Type) >= GetPriority(token.Type))
                        {
                            poliz.Add(stack.Pop().Value);
                        }
                        stack.Push(token);
                        break;
                    case TokenType.открывающая_скобка:
                        stack.Push(token);
                        openingBrackets++;
                        break;
                    case TokenType.ERROR:
                        DisplayErrors(new List<string> { "Ошибка" });
                        break;
                    case TokenType.Закрывающая_скобка:
                        if (!stack.Any())
                        {
                            DisplayErrors(new List<string> { "Ошибка: Недостаточно открывающих скобок." });
                            return null;
                        }

                        while (stack.Peek().Type != TokenType.открывающая_скобка)
                        {
                            poliz.Add(stack.Pop().Value);
                            if (!stack.Any())
                            {
                                DisplayErrors(new List<string> { "Ошибка: Недостаточно открывающих скобок." });
                                return null;
                            }
                        }
                        stack.Pop(); // Удаляем открывающую скобку
                        closingBrackets++;
                        break;
                }
            }

            while (stack.Any())
            {
                if (stack.Peek().Type == TokenType.открывающая_скобка)
                {
                    DisplayErrors(new List<string> { "Ошибка: Имеется незакрытая открывающая скобка." });
                    return null;
                }
                poliz.Add(stack.Pop().Value);
            }

            if (openingBrackets != closingBrackets)
            {
                DisplayErrors(new List<string> { "Ошибка: Количество открывающих и закрывающих скобок не совпадает." });
                return null;
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
                case TokenType.открывающая_скобка:
                case TokenType.Закрывающая_скобка:
                    return 0; // Приоритет скобок не учитывается при формировании ПОЛИЗа
                default:
                    return 0;
            }
        }
        public void DisplayErrors(List<string> errors)
        {
            dataGridView2.DataSource = null;
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();

            // Проверка на null перед обращением к dataGridView2
            if (dataGridView2 == null)
            {
                // Обработка случая, когда dataGridView2 не инициализирован
                return;
            }

            // Очистка данных в dataGridView2


            // Добавление столбца для вывода ошибок
            dataGridView2.Columns.Add("Error", "Ошибка");

            // Добавление ошибок в DataGridView
            foreach (var error in errors)
            {
                dataGridView2.Rows.Add(error);
            }
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void FindWordsEndingWithT()
        {
            string inputText = richTextBox1.Text;
            List<(string word, int startIndex)> wordsEndingWithT = new List<(string, int)>();

            // Используем регулярное выражение для поиска слов, заканчивающихся на "t" или "T"
            Regex regex = new Regex(@"(\b\w*[^tT]\b)");

            MatchCollection matches = regex.Matches(inputText);

            foreach (Match match in matches)
            {
                wordsEndingWithT.Add((match.Value, match.Index));
            }

            // Выводим найденные слова в RichTextBox2
            richTextBox2.Text = string.Join(Environment.NewLine, wordsEndingWithT.Select(word => $"{word.word} (начальная позиция: {word.startIndex})"));
        }
        private void FindWordsEndingWithT2()
        {
            string inputText = richTextBox1.Text;
            string pattern = @"\b(201[0-9]|202[0-3])\b";
            Regex regex = new Regex(pattern);

            MatchCollection matches = regex.Matches(inputText);

            // Очищаем richTextBox2 перед записью новых результатов
            richTextBox2.Clear();

            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    richTextBox2.AppendText($"Год: {match.Value}, (начальная позиция: {match.Index})" + Environment.NewLine);
                }
            }
            else
            {
                richTextBox2.AppendText("Годы между 2010 и 2024 не найдены.");
            }
        }
        private void FindTimes()
        {
            string inputText = richTextBox1.Text;
            List<(string time, int startIndex)> foundTimes = new List<(string, int)>();

            // Используем регулярное выражение для поиска времени в формате "ЧЧ:ММ:СС"
            Regex regex = new Regex(@"\b(?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9]\b");

            MatchCollection matches = regex.Matches(inputText);

            foreach (Match match in matches)
            {
                foundTimes.Add((match.Value, match.Index));
            }

            // Выводим найденные временные значения в RichTextBox2 с указанием начальной позиции
            richTextBox2.Clear();
            foreach (var time in foundTimes)
            {
                richTextBox2.AppendText($"{time.time} (начальная позиция: {time.startIndex}){Environment.NewLine}");
            }
        }
        private void лаборатрнаяРабота6ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pol();
        }
        private void лаборатрнаяРабота6ToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }
        private void часть1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindWordsEndingWithT();
        }
        private void часть2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindWordsEndingWithT2();
        }
        private void часть3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindTimes();
        }

        private void лабораторнаяРабота7ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox2.Clear();
            Scan();
            string input = richTextBox1.Text;
            RecursiveDescentParser parser = new RecursiveDescentParser(input, richTextBox2);
            parser.Parse();

        }
        public class RecursiveDescentParser
        {
            private List<string> tokens;
            private int currentTokenIndex;
            private RichTextBox output;

            public RecursiveDescentParser(string input, RichTextBox output)
            {
                tokens = new List<string>(input.Split(new char[] { ' ', '\t', '\n', ';' }, StringSplitOptions.RemoveEmptyEntries));
                currentTokenIndex = 0;
                this.output = output;
                output.Clear();
            }

            private string CurrentToken()
            {
                if (currentTokenIndex < tokens.Count)
                {
                    return tokens[currentTokenIndex];
                }
                else
                {
                    return null;
                }
            }

            private void Advance()
            {
                currentTokenIndex++;
            }

            public void Parse()
            {
                try
                {
                    BeginStmt();
                    if (currentTokenIndex < tokens.Count)
                    {
                        throw new Exception("Токен после end");
                    }
                    output.AppendText("Успешно отпарсено.\n");
                }
                catch (Exception e)
                {
                    output.AppendText($"Error: {e.Message}\n");
                }
            }

            private void BeginStmt()
            {
                if (CurrentToken() == "begin")
                {
                    output.AppendText("begin-stmt -> begin stmt-list end\n");
                    Advance();
                    StmtList();
                    if (CurrentToken() == "end")
                    {
                        Advance();
                    }
                    else
                    {
                        throw new Exception("Ожидает 'end'");
                    }
                }
                else
                {
                    throw new Exception("Ожидает 'begin'");
                }
            }

            private void StmtList()
            {
                output.AppendText("stmt-list -> stmt | stmt ; stmt-list\n");
                Stmt();
                while (CurrentToken() == null || CurrentToken() == ";")
                {
                    if (CurrentToken() == ";")
                    {
                        Advance();
                    }
                    if (CurrentToken() == "end")
                    {
                        break;
                    }
                    Stmt();
                }
            }

            private void Stmt()
            {
                output.AppendText("stmt -> begin-stmt | assg-stmt\n");
                if (CurrentToken() == "begin")
                {
                    BeginStmt();
                }
                else
                {
                    AssgStmt();
                }
            }

            private void AssgStmt()
            {
                output.AppendText("assg-stmt -> VAR := arith-expr\n");
                if (IsVar(CurrentToken()))
                {
                    Advance();
                    if (CurrentToken() == ":=")
                    {
                        Advance();
                        ArithExpr();
                    }
                    else
                    {
                        throw new Exception("Ожидает ':='");
                    }
                }
                else
                {
                    throw new Exception("Ожидает переменную");
                }
            }

            private void ArithExpr()
            {
                output.AppendText("arith-expr -> VAR | NUM | ( arith-expr ) | arith-expr + arith-expr | arith-expr * arith-expr\n");
                if (IsVar(CurrentToken()) || IsNum(CurrentToken()))
                {
                    Advance();
                }
                else if (CurrentToken() == "(")
                {
                    Advance();
                    ArithExpr();
                    if (CurrentToken() == ")")
                    {
                        Advance();
                    }
                    else
                    {
                        throw new Exception("Ожидает ')'");
                    }
                }
                else if (CurrentToken() == "+")
                {
                    Advance();
                    ArithExpr();
                }
                else if (CurrentToken() == "*")
                {
                    Advance();
                    ArithExpr();
                }
                else
                {
                    throw new Exception("Ожидает переменную число или '('");
                }
            }

            private bool IsVar(string token)
            {
                if (!string.IsNullOrEmpty(token) && char.IsLetter(token[0]))
                {
                    foreach (char c in token)
                    {
                        if (!char.IsLetterOrDigit(c))
                        {
                            return false;
                        }
                    }
                    return true;
                }
                return false;
            }

            private bool IsNum(string token)
            {
                if (!string.IsNullOrEmpty(token))
                {
                    foreach (char c in token)
                    {
                        if (!char.IsDigit(c))
                        {
                            return false;
                        }
                    }
                    return true;
                }
                return false;
            }
        }
    }
}
