using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf.Forms;

namespace demoSignPdfMerger
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string inputPath = @"C:\dev\_tmp";
            string outputFile = @"C:\dev\_tmp\output\merged.pdf";
            string signFile = @"C:\dev\_tmp\Brian.pfx";
            string signPassword = "P@ssw0rd";

            // Get all PDF in input path
            string[] inputFiles = Directory.GetFiles(inputPath, "*.pdf");

            PdfFileEditor editor = new PdfFileEditor();
            editor.Concatenate(inputFiles, outputFile);

            // Create Document object for output PDF
            using (Document outputDocument = new Document())
            {
                // Loop all input files
                foreach (string inputFile in inputFiles)
                {
                    // Create Document object for input PDF
                    using (Document inputDocument = new Document(inputFile))
                    {
                        Console.WriteLine($"Read file: {inputFile}:");
                        // add all pages
                        outputDocument.Pages.Add(inputDocument.Pages);

                        PdfFileSignature signature = new PdfFileSignature();
                        signature.BindPdf(inputFile);
                        Console.WriteLine(signature.VerifySignature("Name"));
                    }
                }

                //PdfFileSignature signature = new PdfFileSignature(outputDocument);
                ////X509Certificate2 cert = new X509Certificate2(signFile, signPassword);
                //PKCS7 pkcs = new PKCS7(signFile, signPassword);
                //signature.Sign(1, true, new System.Drawing.Rectangle(100, 100, 200, 200), pkcs);

                // Save output document
                outputDocument.Save(outputFile);
                Console.WriteLine($"Output: {outputFile}:");
            }
            Console.WriteLine($"Output: {outputFile}:");
            Console.ReadKey();
        }
    }
}
