using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf.Forms;
using System.Xml.Linq;
using System.Collections.Generic;

namespace demoSignPdfMerger
{
    internal class Program
    {
        static string inputPath = @"C:\dev\_tmp";
        static string outputFile = @"C:\dev\_tmp\output\merged.pdf";
        static string signFile = @"C:\dev\_tmp\Brian.pfx";
        static string signPassword = "P@ssw0rd";
        static string[] inputFiles = Directory.GetFiles(inputPath, "*.pdf");

        static void Main(string[] args)
        {
            using (Document outputDocument = new Document())
            {
                foreach (string inputFile in inputFiles)
                {
                    using (Document inputDocument = new Document(inputFile))
                    {
                        Console.WriteLine($"Read file: {inputFile}:");
                        outputDocument.Pages.Add(RemoveSign(inputDocument).Pages);
                    }
                }
                outputDocument.Save(outputFile);
            }
            ResignAndSave(outputFile);
            Console.WriteLine($"Output: {outputFile}:");
            Console.ReadKey();
        }

        static Document RemoveSign(Document inputDocument)
        {
            // verify sign
            PdfFileSignature pdfSign = new PdfFileSignature();
            pdfSign.BindPdf(inputDocument);
            //Console.WriteLine(pdfSign.VerifySignature("Signature2"));
            var sigNames = pdfSign.GetSignNames();
            // remove sign
            foreach (var sigName in sigNames)
            {
                pdfSign.RemoveSignature(sigName);
            }
            return inputDocument;
        }

        static void ResignAndSave(string outputFile)
        {
            Document outputDocument = new Document(outputFile);
            using (PdfFileSignature signature = new PdfFileSignature(outputDocument))
            {
                PKCS7 pkcs = new PKCS7(signFile, signPassword); // Use PKCS7/PKCS7Detached objects
                signature.Sign(1, true, new System.Drawing.Rectangle(300, 100, 400, 200), pkcs);
                // Save output PDF file
                signature.Save(outputFile);
            }
            outputDocument.Dispose();
        }
    }
}
