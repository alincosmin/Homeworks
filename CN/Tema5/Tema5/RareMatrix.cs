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
            var maxIterations = 1000000;
            var iterations = 0;
            double delta;
            var result = new double[array.Length];

            if (DiagonalHasZeros())
            {
                Console.WriteLine("--- Fail Gauss-Seidell: Matricea are elemente nule pe diagonala!");
                return null;
            }
            else
            {
                do
                {
                    delta = 0;
                    for (var i = 0; i < array.Length; i++)
                    {
                        var prev = result[i];

                        var firstSum =
                            _matrix[i].Where(node => node.ColumnIndex < i)
                                .Sum(node => node.Value*result[node.ColumnIndex]);

                        var secondSum =
                            _matrix[i].Where(node => node.ColumnIndex > i)
                                .Sum(node => node.Value*result[node.ColumnIndex]);

                        result[i] = (array[i] - firstSum - secondSum)/Get(i, i);
                        delta += Math.Abs(result[i] - prev);
                    }

                    iterations++; 
                } while (delta >= Precision && iterations <= maxIterations && delta <= Math.Pow(10, 8));
            }

            if (delta < Precision)
                return result;
            else
            {
                Console.WriteLine("--- Fail Gauss-Seidell: Matricea este divergenta!");
                return new double[0];
            }
        }

        private bool DiagonalHasZeros()
        {
            for (var i = 0; i < _matrix.Length; i++)
            {
                if(Get(i, i) == 0)
                    return true;
            }
            
            return false;
        }

        private double Get(int i, int j)
        {
            var result = _matrix[i] != null ? 
                    _matrix[i].First(x => x.ColumnIndex == j).Value : 0;

            return result;
        }

        public double[] Multiply(double[] array)
        {
            if (array == null || array.Length != _matrix.Length) return null;
            var result = new double[_matrix.Length];

            for (var i = 0; i < _matrix.Length; i++)
            {
                if (_matrix[i] == null)
                {
                    result[i] = 0;
                }
                else
                {
                    result[i] = _matrix[i].Sum(node => node.Value*array[node.ColumnIndex]);
                }
            }

            return result;
        }

        public static double ComputeNorm(double[] firstArray, double[] secondArray)
        {
            double result;

            if (firstArray == null || secondArray == null || firstArray.Length != secondArray.Length)
            {
                result = -1;
            }
            else
            {
                result = firstArray.Select((x, i) => Math.Abs(x - secondArray[i])).Sum();
            }

            return result;
        }
    }
}