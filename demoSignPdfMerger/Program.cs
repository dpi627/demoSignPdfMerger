using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Pdf;

namespace demoSignPdfMerger
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string inputPath = @"C:\dev\_tmp";
            string outputFile = @"C:\dev\_tmp\output\merged.pdf";

            // Get a list of all PDF files in the input path
            string[] inputFiles = Directory.GetFiles(inputPath, "*.pdf");
            // Create Document object for output PDF
            Document outputDocument = new Document();
            foreach (string inputFile in inputFiles)
            {
                // Create a new Document object for the input PDF file
                Document inputDocument = new Document(inputFile);

                // Output the extracted text to the console
                Console.WriteLine($"Read file: {inputFile}:");
                outputDocument.Pages.Add(inputDocument.Pages);
            }
            // Save output document
            outputDocument.Save(outputFile);
            Console.WriteLine($"Output: {outputFile}:");
            Console.ReadKey();
        }
    }
}
