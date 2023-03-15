using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

namespace demoSignPdfMerger
{
    internal class Program
    {
        static string inputPath = @"C:\dev\_tmp";
        static string outputFile = @"C:\dev\_tmp\output\merged.pdf";
        static string signFile = @"C:\dev\_tmp\Brian.pfx";
        static string signPassword = "P@ssw0rd";

        static void Main(string[] args)
        {
            string[] inputFiles = Directory.GetFiles(inputPath, "*.pdf");
            using (Document outputDocument = new Document())
            {
                foreach (string inputFile in inputFiles)
                {
                    using (Document inputDocument = new Document(inputFile))
                        outputDocument.Pages.Add(RemoveSign(inputDocument).Pages);
                }
                outputDocument.Save(outputFile);
            }
            ResignAndSave(outputFile);
        }

        static Document RemoveSign(Document inputDocument)
        {
            PdfFileSignature pdfSign = new PdfFileSignature();
            pdfSign.BindPdf(inputDocument);
            var sigNames = pdfSign.GetSignNames();
            foreach (var sigName in sigNames)
                pdfSign.RemoveSignature(sigName);
            return inputDocument;
        }

        static void ResignAndSave(string outputFile)
        {
            Document outputDocument = new Document(outputFile);
            using (PdfFileSignature signature = new PdfFileSignature(outputDocument))
            {
                PKCS7 pkcs = new PKCS7(signFile, signPassword);
                signature.Sign(1, true, new System.Drawing.Rectangle(300, 100, 400, 200), pkcs);
                signature.Save(outputFile);
            }
            outputDocument.Dispose();
        }
    }
}
