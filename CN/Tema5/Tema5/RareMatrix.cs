using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Tema5
{
    public class RareMatrix
    {
        private readonly int _fillDegree = 10;
        private readonly List<RareMatrixNode>[] _matrix;
        private readonly double _precision = Math.Pow(10, -15);

        public RareMatrix(int size, int fillDegree)
        {
            _matrix = new List<RareMatrixNode>[size];
            _fillDegree = fillDegree;
        }

        public RareMatrix(StreamReader reader, int fillDegree = 10)
        {
            _fillDegree = fillDegree;
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

            CheckFillDegree();
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

        private void CheckFillDegree()
        {
            if (_matrix.Where(x => x != null).Any(nodeList => nodeList.Count(x => x.Value != 0) > _fillDegree))
            {
                throw new InvalidDataException("Too many values");
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
                } while (delta >= _precision && iterations <= maxIterations && delta <= Math.Pow(10, 8));
            }

            if (delta < _precision)
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

        public string NiceToString()
        {
            var builder = new StringBuilder();
            var index = 0;
            foreach (var nodeList in _matrix)
            {
                builder.Append(string.Format("A[{0}] = ", index));

                var elements = nodeList.Select(x => string.Format("({0}, {1})", x.Value, x.ColumnIndex)).ToArray();
                builder.Append("{" + string.Join(", ", elements) + "}");

                index++;
                builder.AppendLine();
            }

            return builder.ToString();
        }

        public void ExportToFile(string filename)
        {
            using (var writer = new StreamWriter(filename))
            {
                writer.WriteLine(_matrix.Count());
                writer.WriteLine();

                var index = 0;
                foreach (var nodeList in _matrix)
                {
                    nodeList.Select(x => string.Format("{0}, {1}, {2}", x.Value, index, x.ColumnIndex))
                        .ToList()
                        .ForEach(x => writer.WriteLine(x));

                    index++;
                }
            }
        }

        public static RareMatrix GenerateSymetric(int size, int fillDegree)
        {
            RareMatrix matrix = null;
            int trials = 3;
            
            while (trials-- > 0)
            {
                try
                {
                    matrix = new RareMatrix(size, fillDegree);
                    var random = new Random(DateTime.Now.Millisecond);
                    int line, column;
                    double value;

                    for (var i = 0; i < size*size; i++)
                    {
                        line = random.Next(3*size);
                        column = random.Next(3*size);

                        if (line >= size || column >= size) continue;
                        if (matrix._matrix[line] != null && matrix._matrix[line].Any(x => x.ColumnIndex == column)) continue;

                        value = random.NextDouble()*size*2;

                        matrix._matrix[line] = matrix._matrix[line] ?? new List<RareMatrixNode>();
                        matrix._matrix[line].Add(new RareMatrixNode()
                        {
                            Value = value,
                            ColumnIndex = column
                        });

                        if (line == column) continue;

                        matrix._matrix[column] = matrix._matrix[column] ?? new List<RareMatrixNode>();
                        matrix._matrix[column].Add(new RareMatrixNode()
                        {
                            Value = value,
                            ColumnIndex = line
                        });
                    }
                    matrix.CheckFillDegree();
                    break;
                }
                catch (InvalidDataException) { }
            }

            return matrix;
        }

        public RareMatrix SumWith(RareMatrix matrix)
        {
            if(_matrix.Count() != matrix._matrix.Count())
                return null;

            var result = new RareMatrix(_matrix.Count(), 16);

            for (var line = 0; line < _matrix.Count(); line++)
            {
                if(_matrix[line] == null && matrix._matrix[line] == null)
                   continue;

                if (_matrix[line] == null)
                {
                    result._matrix[line] = matrix._matrix[line];
                }
                else if (matrix._matrix[line] == null)
                {
                    result._matrix[line] = _matrix[line];
                }
                else
                {
                    result._matrix[line] = result._matrix[line] ?? new List<RareMatrixNode>();
                    var min = Math.Min(_matrix[line].Count, matrix._matrix.Count());
                    
                    for (var i = 0; i < min; i++)
                    {
                        if(_matrix[line][i] == null || matrix._matrix[line][i] == null)
                            continue;

                        var first = _matrix[line][i];
                        var second = matrix._matrix[line][i];

                        if (first.ColumnIndex == second.ColumnIndex)
                        {
                            result._matrix[line].Add(RareMatrixNode.Add(first, second));
                        }
                        else
                        {
                            var node = first.ColumnIndex < second.ColumnIndex ? first : second;
                            result._matrix[line].Add(node);
                        }
                    }
                }
            }

            result.CheckFillDegree();

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
                    result[i] = _matrix[i].Sum(node => node.Value * array[node.ColumnIndex]);
                }
            }

            return result;
        }

        public RareMatrix MultiplyWith(RareMatrix matrix)
        {
            if (_matrix.Count() != matrix._matrix.Count())
                return null;

            var result = new RareMatrix(_matrix.Count(), 0);

            for (var lineA = 0; lineA < _matrix.Count(); lineA++)
            {
                for (var col = 0; col < _matrix.Count(); col++)
                {
                    double value = 0;

                    for (var i = 0; i < _matrix.Count(); i++)
                    {
                        double lineFactor = 0, colFactor = 0;

                        if (_matrix[lineA] != null)
                        {
                            var el = _matrix[lineA].FirstOrDefault(x => x.ColumnIndex == i);
                            lineFactor = el != null ? el.Value : 0;
                        }

                        if (matrix._matrix[i] != null)
                        {
                            var el = matrix._matrix[i].FirstOrDefault(x => x.ColumnIndex == col);
                            colFactor = el != null ? el.Value : 0;
                        }

                        value += lineFactor*colFactor;
                    }

                    if (value != 0)
                    {
                        result._matrix[lineA] = result._matrix[lineA] ?? new List<RareMatrixNode>();
                        result._matrix[lineA].Add(new RareMatrixNode()
                        {
                            Value = value,
                            ColumnIndex = col
                        });
                    }
                }
            }

            return result;
        }
    }
}