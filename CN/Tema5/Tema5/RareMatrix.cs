using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Tema5
{
    public class RareMatrix
    {
        private List<RareMatrixNode>[] _matrix; 

        public static double Precision
        {
            get { return Math.Pow(10, -5); }
        }

        public RareMatrix(StreamReader reader)
        {
            var line = reader.ReadLine();
            long size;
            if (!Int64.TryParse(line, out size))
            {
                throw new InvalidDataException("Invalid value on first line of the file");
            }

            _matrix = new List<RareMatrixNode>[size];

            while (!reader.EndOfStream)
            {
                var currentLine = reader.ReadLine();
                if (!string.IsNullOrWhiteSpace(currentLine))
                {
                    var items = currentLine.Split(',').Select(x => x.Trim()).ToArray();
                    if (items.Count() == 3)
                    {
                        var node = new RareMatrixNode()
                        {
                            Value = Double.Parse(items[0]),
                            ColumnIndex = Int32.Parse(items[2])
                        };
                        var firstIndex = Int64.Parse(items[1]);
                        
                        if(_matrix[firstIndex] == null)
                            _matrix[firstIndex] = new List<RareMatrixNode>();
                        _matrix[firstIndex].Add(node);
                    }
                }
            }

            SortMatrix();

            foreach (var nodeList in _matrix.Where(x => x != null))
            {
                var index = 0;
                index = nodeList.First().ColumnIndex;

                for (var i = 1; i < nodeList.Count; i++)
                {
                    if (index == nodeList[i].ColumnIndex)
                    {
                        nodeList.RemoveAt(--i);
                    }

                    index = nodeList[i].ColumnIndex;
                }
            }
        }

        private void SortMatrix()
        {
            foreach (var nodeList in _matrix.Where(nodeList => nodeList != null))
            {
                nodeList.Sort((firstNode, secondNode) => firstNode.ColumnIndex < secondNode.ColumnIndex ? -1 : 1);
            }
        }

        public double[] SolveGaussSeidell(double[] array)
        {
            
        }
    }
}