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
            var files = new []
            {
                "m_rar_2015_1.txt",
                "m_rar_2015_2.txt",
                "m_rar_2015_3.txt",
                "m_rar_2015_4.txt"
            };

            foreach (var path in files)
            {
                Console.WriteLine("Fisierul: {0}", path);
                var matrix = new RareMatrix(new StreamReader(path));
                var array = ReadArray(new StreamReader(path));
                Console.WriteLine("--- {0} elemente", array.Length);

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
