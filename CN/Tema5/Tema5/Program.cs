using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Tema5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Demo Tema 4:");
            Console.WriteLine("------------");
            DemoTema4();

            Console.WriteLine();

            //Console.WriteLine("Demo Tema 5:");
            //Console.WriteLine("------------");
            //DemoTema5();
        }

        private static void DemoTema4()
        {
            var generatedMatrix = RareMatrix.GenerateSymetric(50, 15);
            if (generatedMatrix != null)
            {
                generatedMatrix.ExportToFile("simetrica.txt");
                Console.WriteLine("--- Matricea a fost generata si exportata");
            }
            else
            {
                Console.WriteLine("--- Nu s-a putut genera matricea");
            }

            var files = new[]
            {
                "input_4/test.txt",
                //"input_4/m_rar_2015_1.txt",
                //"input_4/m_rar_2015_2.txt",
                //"input_4/m_rar_2015_3.txt"
            };

            foreach (var path in files)
            {
                Console.WriteLine("Fisierul: {0}", path);

                try
                {
                    var matrix = new RareMatrix(new StreamReader(path));
                    var array = ReadArray(new StreamReader(path));
                    Console.WriteLine("--- {0} elemente", array.Length);
                    Console.WriteLine(matrix.NiceToString());
                }
                catch (InvalidDataException ex)
                {
                    Console.WriteLine("Eroare: {0}", ex.Message);
                }

                Console.WriteLine();
            }
        }

        private static void DemoTema5()
        {
            var files = new[]
            {
                "input_5/m_rar_2015_1.txt",
                "input_5/m_rar_2015_2.txt",
                "input_5/m_rar_2015_3.txt",
                "input_5/m_rar_2015_4.txt"
            };

            foreach (var path in files)
            {
                Console.WriteLine("Fisierul: {0}", path);

                try
                {
                    var matrix = new RareMatrix(new StreamReader(path));
                    var array = ReadArray(new StreamReader(path));
                    Console.WriteLine("--- {0} elemente", array.Length);

                    var result = matrix.SolveGaussSeidell(array);
                    Console.WriteLine("--- {0} elemente in rezultat Gauss-Seidell", result.Length);

                    var prod = matrix.Multiply(array);
                    var norm = RareMatrix.ComputeNorm(prod, result);
                    Console.WriteLine("--- Norma ||AxGS - b||: {0}", norm);
                }
                catch (InvalidDataException ex)
                {
                    Console.WriteLine("Eroare: {0}", ex.Message);
                }

                Console.WriteLine();
            }
        }

        private static double[] ReadArray(StreamReader reader)
        {
            var result = new List<double>();

            var line = reader.ReadLine();
            long size;
            if (!Int64.TryParse(line, out size))
            {
                throw new InvalidDataException("Invalid value on first line of the file");
            }

            while (!reader.EndOfStream)
            {
                while (!reader.EndOfStream)
                {
                    var currentLine = reader.ReadLine();
                    if (!string.IsNullOrWhiteSpace(currentLine))
                    {
                        var items = currentLine.Split(',').Select(x => x.Trim()).ToArray();
                        if (items.Count() == 1)
                        {
                            result.Add(Double.Parse(items[0]));
                        }
                    }
                }
            }

            if (result.Count != size)
            {
                throw new InvalidDataException(string.Format("File contains {0} values than specified in header",
                    result.Count > size ? "more" : "less"));
            }

            return result.ToArray();
        }
    }
}
