using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
//using iText.Pdfa;
//using iText.IO;
//using iText.Kernel.Pdf;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace Pdf2Text
{

    public partial class Read : Form
    {
        public Read()
        {
            InitializeComponent();
        }
        public string ExtractTextFromPdf(string pdfFileName)
        {
            StringBuilder result = new StringBuilder();
            // Create a reader for the given PDF file
            using (PdfReader reader = new PdfReader(pdfFileName))
            {
                // Read pages
               for (int page = 1; page <= reader.NumberOfPages; page++)
                {
                    SimpleTextExtractionStrategy strategy =
                        new SimpleTextExtractionStrategy();
                    string pageText =
                        PdfTextExtractor.GetTextFromPage(reader, page, strategy);
                    result.Append(pageText);
                }
            }
            return result.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String fileContent = String.Empty;
            string filePath = string.Empty;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "d:\\";
                openFileDialog.Filter = "pdf files (*.pdf)|*.pdf|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    try
                    {
                        fileContent = ExtractTextFromPdf(filePath);
                    }
                    catch 
                    {
                        //Console.WriteLine(exception);
                        throw;
                    }

                }
            }

            richTextBox1.Text = fileContent;
        }
    }
}
