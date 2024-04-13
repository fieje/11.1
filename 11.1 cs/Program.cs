using System;
using System.IO;

namespace BinaryFileProcessing
{
    class ProgramTests
    {
        static void Main(string[] args)
        {
            string inputFileName, outputFileName;
            Console.WriteLine("Enter input file name: ");
            inputFileName = Console.ReadLine();
            Console.WriteLine("Enter output file name: ");
            outputFileName = Console.ReadLine();

            CreateDataBinaryFile(inputFileName);
            ProcessDataBinaryFile(inputFileName, outputFileName);
            PrintInputFileContents(inputFileName);
            PrintOutputBinaryFile(outputFileName);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static void CreateDataBinaryFile(string fileName)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(fileName, FileMode.Create)))
            {
                Random rand = new Random();
                for (int i = 0; i < 10; i++)
                {
                    writer.Write(rand.Next(-100, 101)); 
                }
            }
        }

        static void ProcessDataBinaryFile(string inputFileName, string outputFileName)
        {
            using (BinaryReader reader = new BinaryReader(File.Open(inputFileName, FileMode.Open)))
            using (BinaryWriter writer = new BinaryWriter(File.Open(outputFileName, FileMode.Create)))
            {
                int sum = 0;
                int negativeCount = 0;

                while (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    int number = reader.ReadInt32();

                    if (number < 0)
                    {
                        sum += number;
                        negativeCount++;
                        writer.Write(sum);
                    }
                }
            }
        }

        static void PrintInputFileContents(string fileName)
        {
            Console.WriteLine("Input file contents:");
            using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
            {
                while (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    Console.WriteLine(reader.ReadInt32());
                }
            }
        }

        static void PrintOutputBinaryFile(string fileName)
        {
            Console.WriteLine("Output file contents:");
            using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
            {
                while (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    Console.WriteLine(reader.ReadInt32());
                }
            }
        }
    }
}
