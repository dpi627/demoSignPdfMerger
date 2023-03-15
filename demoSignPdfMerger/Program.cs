using System;
using System.IO;
using Aspose.Pdf;

namespace demoSignPdfMerger
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string inputPath = @"C:\dev\_tmp";
            string outputFile = @"C:\dev\_tmp\output\merged.pdf";

            // Get all PDF in input path
            string[] inputFiles = Directory.GetFiles(inputPath, "*.pdf");

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
                    }
                }
                // Save output document
                outputDocument.Save(outputFile);
                Console.WriteLine($"Output: {outputFile}:");
            }
            Console.ReadKey();
        }
    }
}
