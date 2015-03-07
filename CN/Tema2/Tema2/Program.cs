using System;

namespace Tema2
{
    class Program
    {
        static void Main(string[] args)
        {
            var lu = new LUDecomposer(-15);
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

            Console.WriteLine("\nDeterminant: {0}", lu.ComputeDeterminant());

            var es = new EcuationSolver(-15);
            var vector = new double[] {14, 16, 12};
            es.Prepare(result, vector);
            var solution = es.ComputeSolution();

            Console.WriteLine("\nEcuation solution:");
            Console.WriteLine(solution.NiceSolutionDisplay());

            var norm = ComputeNorm(matrix, vector, solution);
            Console.WriteLine("\nFinal: {0}", norm);
        }

        private static double ComputeNorm(double[] matrix, double[] vector, double[] solution)
        {
            var resVector = new double[vector.Length];

            for (int i = 0; i < vector.Length; i++)
            {
                double sum = 0;

                for (int j = 0; j < vector.Length; j++)
                {
                    sum += matrix[i*vector.Length + j]*solution[j];
                }

                resVector[i] = sum;
            }

            double anotherSum = 0;
            for (int i = 0; i < vector.Length; i++)
            {
                anotherSum += Math.Pow(resVector[i]-vector[i], 2);
            }

            return Math.Sqrt(anotherSum);
        }

    }
}
