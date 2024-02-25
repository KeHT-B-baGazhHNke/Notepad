using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using System.IO;
using System.Drawing.Printing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Notepad
{
    public partial class Form1 : Form
    {
        public string filename;
        public bool isChanged;
        private string stringToPrint = "";

        public Form1()
        {
            InitializeComponent();
            //Init();
            fontBox.SelectedItem = fontBox.Items[0];
            colorBox.SelectedItem = colorBox.Items[0];
        }
        //public void Init()
        //{
        //    filename = "Безымянный";
        //    isChanged = false;
        //}
        public void CreateFile(object sender, EventArgs e)
        {
            if (isChanged)
            {
                DialogResult result = MessageBox.Show($"Вы хотите сохранить изменения в файле\n\"{filename}\"?", "Notepad", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    SaveFile(filename);
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
            }
            printDocument1.DocumentName = "Безымянный";
            textBox1.Text = "";
            filename = "Безымянный";
            UpdateTextWithTitle();
        }
        public void OpenFile(object sender, EventArgs e)
        {
            if (isChanged)
            {
                DialogResult result = MessageBox.Show($"Вы хотите сохранить изменения в файле\n\"{filename}\"?", "Notepad", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    SaveFile(filename);
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
            }
            openFileDialog1.FileName = "";
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamReader text = new StreamReader(openFileDialog1.FileName);
                    textBox1.Text = text.ReadToEnd();
                    text.Close();
                    filename = openFileDialog1.FileName;
                    isChanged = false;
                }
                catch 
                {
                }
            }
            UpdateTextWithTitle();
            printDocument1.DocumentName = Path.GetFileName(filename);
        }
        public bool SaveFile(string _filename)
        {
            if (_filename == "Безымянный" || _filename == "")
            {
                if(saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    _filename = saveFileDialog1.FileName;
                }
                else
                {
                    printDocument1.DocumentName = Path.GetFileName(filename);
                    return false;
                }
            }
            try
            {
                StreamWriter text = new StreamWriter(_filename);
                text.Write(textBox1.Text);
                text.Close();
                filename = _filename;
                isChanged = false;
                printDocument1.DocumentName = Path.GetFileName(filename);
                UpdateTextWithTitle();
                return true;
            }
            catch
            {
                printDocument1.DocumentName = Path.GetFileName(filename);
                return false;
            }
        }
        public void Save(object sender, EventArgs e)
        {

            SaveFile(filename);
        }
        public void SaveAs(object sender, EventArgs e)
        {
            SaveFile("");
        }
        private void Form1_Activated(object sender, EventArgs e)
        {
            файлToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            справкаToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
        }
        private void Form1_Deactivated(object sender, EventArgs e)
        {
            файлToolStripMenuItem.ForeColor = System.Drawing.SystemColors.GrayText;
            справкаToolStripMenuItem.ForeColor = System.Drawing.SystemColors.GrayText;
        }
        private void Exit(object sender, EventArgs e)
        {
            if (isChanged)
            {
                DialogResult result = MessageBox.Show($"Вы хотите сохранить изменения в файле\n\"{filename}\"?", "Notepad", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    if (SaveFile(filename) == false)
                        return;
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
            }
            Close();
        }
        private void Close(object sender, FormClosingEventArgs e)
        {
            if (isChanged)
            {
                DialogResult result = MessageBox.Show($"Вы хотите сохранить изменения в файле\n\"{filename}\"?", "Notepad", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    if(SaveFile(filename) == false)
                        e.Cancel = true;
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }
        private void textChanged(object sender, EventArgs e)
        {
            if (!isChanged)
            {
                this.Text = this.Text.Replace("*", "");
                isChanged = true;
                this.Text = "*" + this.Text;
            }
        }
        public void UpdateTextWithTitle()
        {
            if (filename != "")
            {
                Text = Path.GetFileName(filename) + " \t– Notepad";
            }
            else
            {
                Text = "Безымянный \t– Notepad";
            }
        }
        private void ToggleBold(object sender, EventArgs e)
        {
            if (textBox1.SelectionFont != null)
            {
                Font currentFont = textBox1.SelectionFont;
                FontStyle newFontStyle;

                if (textBox1.SelectionFont.Bold == true)
                {
                    newFontStyle = FontStyle.Regular;
                }
                else
                {
                    newFontStyle = FontStyle.Bold;
                }

                textBox1.SelectionFont = new Font(
                   currentFont.FontFamily,
                   currentFont.Size,
                   newFontStyle
                );
            }
        }
        private void ToggleItalic(object sender, EventArgs e)
        {
            if (textBox1.SelectionFont != null)
            {
                Font currentFont = textBox1.SelectionFont;
                FontStyle newFontStyle;

                if (textBox1.SelectionFont.Italic == true)
                {
                    newFontStyle = FontStyle.Regular;
                }
                else
                {
                    newFontStyle = FontStyle.Italic;
                }

                textBox1.SelectionFont = new Font(
                   currentFont.FontFamily,
                   currentFont.Size,
                   newFontStyle
                );
            }
        }

        private void ToggleColor(object sender, EventArgs e)
        {
            switch (colorBox.SelectedIndex)
            {
                case 0:
                    textBox1.SelectionColor = Color.Black;
                    break;
                case 1:
                    textBox1.SelectionColor = Color.Red;
                    break;
                case 2:
                    textBox1.SelectionColor = Color.Green;
                    break;
                case 3:
                    textBox1.SelectionColor = Color.Blue;
                    break;
                default:
                    textBox1.SelectionColor = Color.Black;
                    break;
            }
        }

        private void FontSize(object sender, EventArgs e)
        {
            textBox1.SelectionFont = new Font(textBox1.SelectionFont.FontFamily,int.Parse(fontBox.Text),textBox1.SelectionFont.Style);
        }

        private void PrintFile(object sender, EventArgs e)
        {
            stringToPrint = textBox1.Text;
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDialog.Document.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            int charactersOnPage = 0;
            int linesPerPage = 0;

            e.Graphics.MeasureString(stringToPrint, textBox1.SelectionFont, e.MarginBounds.Size, StringFormat.GenericTypographic,
                out charactersOnPage, out linesPerPage);
            e.Graphics.DrawString(stringToPrint, textBox1.SelectionFont, Brushes.Black,
                e.MarginBounds, StringFormat.GenericTypographic);
            stringToPrint = stringToPrint.Substring(charactersOnPage);
            e.HasMorePages = (stringToPrint.Length > 0);
        }

        private void параметрыСтраницыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pageSetupDialog1.ShowDialog();
        }

        private void предпросмотрПечатиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stringToPrint = textBox1.Text;
            printPreviewDialog1.ShowDialog();
        }
    }
}
