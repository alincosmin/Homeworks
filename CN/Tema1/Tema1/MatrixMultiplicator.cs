using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Dynamic;
using System.IO;
using System.Linq;

namespace Tema1
{
    public class MatrixMultiplicator
    {
        private int _n;
        private int[] _secondMatrixLines;
        private bool[] _matrix1;
        private bool[] _matrix2;

        private IDictionary _sums;

        public MatrixMultiplicator(int[] m1, int[] m2)
        {
            _n = (int) Math.Sqrt(m1.Length);
            if (m1.Length != _n*_n || m1.Length != m2.Length)
                throw new InvalidDataException("Matricile au dimensiuni incorecte");

            _matrix1 = m1.Select(x => x != 0).ToArray();
            _matrix2 = m2.Select(x => x != 0).ToArray();
            _sums = new Dictionary<int, int>();
            ComputeLines();
        }

        public int[] GetResult()
        {
            var result = new List<bool>();

            for (int i = 0; i < _n; i++)
            {
                var line = _matrix1.Skip(i*_n).Take(_n).ToArray();
                var computed = GetLineSumForLine(line);
                result.AddRange(IntToBoolArray(computed));
            }

            var intResult = result.Select(x => x ? 1 : 0).ToArray();
            return intResult;
        }

        private void ComputeLines()
        {
            _secondMatrixLines = new int[_n];
            for (var i = 0; i < _n; i++)
            {
                var line = _matrix2.Skip(i*_n).Take(_n).ToArray();
                _secondMatrixLines[i] = BoolArrayToInt(line);
            }
        }

        private int GetLineSumForLine(bool[] line)
        {
            var intLine = BoolArrayToInt(line);
            int sum = 0;
            if (_sums.Contains(intLine))
                sum = (int) _sums[intLine];
            else
            {
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i])
                        sum |= _secondMatrixLines[i];
                }
                _sums.Add(intLine, sum);
            }

            return sum;
        }

        // Utils
        private int BoolArrayToInt(bool[] array)
        {
            var intcontainer = new int[1];
            new BitArray(array.Reverse().ToArray()).CopyTo(intcontainer, 0);
            return intcontainer[0];
        }

        private bool[] IntToBoolArray(int number)
        {
            return new BitArray(new int[] { number }).Cast<bool>().Take(_n).Reverse().ToArray();
        }
    }
}