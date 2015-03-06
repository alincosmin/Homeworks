using System;

namespace Tema2
{
    class Program
    {
        static void Main(string[] args)
        {
            var lu = new LUDecomposition(-15);
            var matrix = new double[]
            {
                4, 2, 6,
                2, 4, 6,
                2, 2, 5
            };

            lu.SetMatrix(matrix);
            /*
             * expected result:
             *   1   0  0    4 2 6
             *  1/2  1  0    0 3 3
             *  1/2 1/3 1    0 0 1
             * 
             */
            var result = lu.Decompose();

            Console.WriteLine("Initial matrix: ");
            Console.WriteLine(matrix.NiceMatrixDisplay());
            Console.WriteLine("\nLU matrix as one");
            Console.WriteLine(result.NiceMatrixDisplay());
            Console.WriteLine("\nL matrix");
            Console.WriteLine(lu.LMatrix.NiceMatrixDisplay());
            Console.WriteLine("\nU matrix");
            Console.WriteLine(lu.UMatrix.NiceMatrixDisplay());
        }

    }
}
