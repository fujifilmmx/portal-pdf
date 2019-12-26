using PdfSharp.Pdf;
using PdfSharp.Pdf.Advanced;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace pdfTextExctractorConsole
{
    public class Pdf
    {
        public static List<string> SplitPdfFile(string filename)
        {
            List<string> files = new List<string>();
            PdfDocument inputDocument = PdfReader.Open(filename, PdfDocumentOpenMode.Import);
            string name = Path.GetFileNameWithoutExtension(filename);
            int numberOfFiles = (inputDocument.PageCount / 10) + 1;
            int currentPage = 0;
            for (int idx = 0; idx < numberOfFiles; idx++)
            {
                PdfDocument outputDocument = new PdfDocument();
                outputDocument.Version = inputDocument.Version;
                outputDocument.Info.Title = String.Format(name + "{0}", idx + 1);
                outputDocument.Info.Creator = inputDocument.Info.Creator;
                for (int i = currentPage; i < currentPage + 10; i++)
                {
                    outputDocument.AddPage(inputDocument.Pages[i]);
                }
                string outFileName = name + "-" + (idx + 1) + ".pdf";
                outputDocument.Save(outFileName);
                files.Add(outFileName);
            }
            return files;
        }
        public static void ReplaceTextInPdfFile(string SourceFile, string TargetFile, string sourceText, string targetText)
        {
            PdfDocument inputDocument = PdfReader.Open(SourceFile, PdfDocumentOpenMode.Modify);
            foreach(var page in inputDocument.Pages)
            {
                ReplaceTextInPdfPage(page, sourceText, targetText);
            }
            inputDocument.Save(TargetFile);
        }
        public static void ReplaceTextInPdfPage(PdfPage contentPage, string source, string target)
        {
            ModifyPdfContentStreams(contentPage, stream =>
            {
                if (!stream.TryUnfilter())
                    return false;
                var search = string.Join("\\s*", source.Select(c => c.ToString()));
                var stringStream = Encoding.Default.GetString(stream.Value, 0, stream.Length);
                if (!Regex.IsMatch(stringStream, search))
                    return false;
                stringStream = Regex.Replace(stringStream, search, target);
                stream.Value = Encoding.Default.GetBytes(stringStream);
                stream.Zip();
                return false;
            });
        }


        public static void ModifyPdfContentStreams(PdfPage contentPage, Func<PdfDictionary.PdfStream, bool> Modification)
        {

            for (var i = 0; i < contentPage.Contents.Elements.Count; i++)
                if (Modification(contentPage.Contents.Elements.GetDictionary(i).Stream))
                    return;
            var resources = contentPage.Elements?.GetDictionary("/Resources");
            var xObjects = resources?.Elements.GetDictionary("/XObject");
            if (xObjects == null)
                return;
            foreach (var item in xObjects.Elements.Values.OfType<PdfReference>())
            {
                var stream = (item.Value as PdfDictionary)?.Stream;
                if (stream != null)
                    if (Modification(stream))
                        return;
            }
        }
    }
}
