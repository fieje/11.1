using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace BinaryFileProcessing.Tests
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void TestCreateDataBinaryFile()
        {
            string inputFileName = "testInput.bin";

            CreateDataBinaryFile(inputFileName);

            Assert.IsTrue(File.Exists(inputFileName));

            File.Delete(inputFileName);
        }

        [TestMethod]
        public void TestProcessDataBinaryFile()
        {
            string inputFileName = "testInput.bin";
            string outputFileName = "testOutput.bin";

            CreateDataBinaryFile(inputFileName);

            ProcessDataBinaryFile(inputFileName, outputFileName);

            Assert.IsTrue(File.Exists(outputFileName));

         File.Delete(inputFileName);
            File.Delete(outputFileName);
        }

        [TestMethod]
        public void TestPrintInputFileContents()
        {
            string inputFileName = "testInput.bin";
            CreateDataBinaryFile(inputFileName);
            StringWriter sw = new StringWriter();
            Console.SetOut(sw);

            PrintInputFileContents(inputFileName);
            string expectedOutput = GetTestDataContents(inputFileName);

            Assert.AreEqual(expectedOutput, sw.ToString());

            File.Delete(inputFileName);
        }

        [TestMethod]
        public void TestPrintOutputBinaryFile()
        {
            string inputFileName = "testInput.bin";
            string outputFileName = "testOutput.bin";
            CreateDataBinaryFile(inputFileName);
            ProcessDataBinaryFile(inputFileName, outputFileName);
            StringWriter sw = new StringWriter();
            Console.SetOut(sw);

            PrintOutputBinaryFile(outputFileName);
            string expectedOutput = GetTestDataContents(outputFileName); 

            Assert.AreEqual(expectedOutput, sw.ToString());

            File.Delete(inputFileName);
            File.Delete(outputFileName);
        }

        private static void CreateDataBinaryFile(string inputFileName)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(inputFileName, FileMode.Create)))
            {
                Random rand = new Random();
                for (int i = 0; i < 10; i++)
                {
                    writer.Write(rand.Next(-100, 101)); 
                }
            }
        }

        private static string GetTestDataContents(string fileName)
        {
            StringWriter sw = new StringWriter();
            using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
            {
                while (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    sw.WriteLine(reader.ReadInt32());
                }
            }
            return sw.ToString();
        }

        private static void ProcessDataBinaryFile(string inputFileName, string outputFileName)
        {
            using (BinaryReader reader = new BinaryReader(File.Open(inputFileName, FileMode.Open)))
            using (BinaryWriter writer = new BinaryWriter(File.Open(outputFileName, FileMode.Create)))
            {
                while (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    int data = reader.ReadInt32();
                  
                    data *= data;
                    writer.Write(data);
                }
            }
        }

        private static void PrintInputFileContents(string fileName)
        {
            using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
            {
                while (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    Console.WriteLine(reader.ReadInt32());
                }
            }
        }

        private static void PrintOutputBinaryFile(string fileName)
        {
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
