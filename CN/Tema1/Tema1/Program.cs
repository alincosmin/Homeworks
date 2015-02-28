using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Tema1
{
    static class Program
    {
        static void Main(string[] args)
        {
            var precision = GetMachinePrecision();
            Console.WriteLine("Ex 1: u = 10^(-{0})", precision);
            var assoc1 = IsAssocAdd(Math.Pow(10, -precision));
            Console.WriteLine("Ex 2: a) {0}", assoc1);
            var ex = AssocMulExample(Math.Pow(10, -precision));
            Console.WriteLine("      b) (x * v) * v != x * (v * v) : x = {0}, v = 1+u", ex);

            Console.WriteLine("Ex 3:");

            var m1 = new int[]
            {
                1, 0, 1, 0,
                1, 1, 0, 0,
                1, 0, 0, 1,
                0, 1, 0, 0
            };

            var m2 = new int[]
            {
                0, 0, 1, 1, 
                1, 0, 0, 1,
                0, 1, 1, 0, 
                1, 0, 1, 0
            };

            var multiplicator = new MatrixMultiplicator(m1, m2);
            var result = multiplicator.GetResult();

            Console.WriteLine("M1:");
            Console.WriteLine(m1.NiceString());
            Console.WriteLine("M2:");
            Console.WriteLine(m2.NiceString());
            Console.WriteLine("Result:");
            Console.WriteLine(result.NiceString());
        }

        public static string NiceString(this int[] matrix)
        {
            var n = (int)Math.Sqrt(matrix.Length);
            var sb = new StringBuilder();
            for (int i = 0; i < n; i++)
            {
                var line = matrix.Skip(i*n).Take(n).ToArray();
                sb.AppendLine(string.Join(" ", line));
            }

            return sb.ToString();
        }

        public static int GetMachinePrecision()
        {
            int p = 1;
            double u = Math.Pow(10, -p);
            while (1.0 + u != 1.0)
            {
                u = Math.Pow(10, -(++p));
            }

            return p-1;
        }

        public static bool IsAssocAdd(double u)
        {
            double left = (1.0 + u/10) + u/10;
            double right = 1.0 + (u/10 + u/10);
            return left == right;
        }

        public static double AssocMulExample(double u)
        {
            double p;
            double v = 1 + u;
            double x = 0xa7b7a7b7;
            if ((x*v)*v == x*(v*v))
                return -1;

            return x;
        }
    }
}
