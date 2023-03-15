using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace demoSignPdfMerger
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string inputPath = @"C:\dev\_tmp";
            string outputFile = @"C:\dev\_tmp\output\merged.pdf";
            string[] inputFiles = Directory.GetFiles(inputPath, "*.pdf");
            PdfFileEditor editor = new PdfFileEditor();
            editor.Concatenate(inputFiles, outputFile);
            Console.WriteLine($"Output: {outputFile}:");
            Console.ReadKey();
        }
    }
}
