using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spire.Pdf;

using IronOcr;
using Spire.Pdf.General.Find;
using Spire.Pdf.Graphics;
using System.Drawing;
using System.IO;
using PdfSharp.Pdf.IO;

namespace pdfTextExctractorConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //List<string> pdfFiles = PdfSplitter.SplitPdfFile(@"C:\Users\lgonzalez\Downloads\FFM20191121PROV.pdf");
            //foreach (var file in pdfFiles)
            //{
            //    ReplacePdfText("Concepto de pago", "Motivo de pago", file);
            //}
            //Pdf.ReplaceTextInPdfFile(@"C:\Users\lgonzalez\Downloads\FFM20191121PROV.pdf", "Concepto de pago", "Motivo de pago");
            //Console.ReadKey();
        }
        static void ReplacePdfText(string OldText, string NewText, string PdfFile)
        {
            PdfDocument doc = new PdfDocument();
            doc.LoadFromFile(PdfFile);
            List<PdfTextFindCollection> finds = new List<PdfTextFindCollection>();
            foreach(PdfPageBase page in doc.Pages)
            {
                finds.Add(page.FindText(OldText, TextFindParameter.IgnoreCase));
            }
            foreach(var _finds in finds)
            {
                _finds.Finds[0].ApplyRecoverString(NewText, true);
            }
            doc.SaveToFile(PdfFile.Replace(".pdf","") + "_output.pdf");
        }        
    }
}
