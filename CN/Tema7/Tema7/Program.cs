using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Tema7
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Partea 1:");
            Console.WriteLine("---------");
            DemoLaguerre();

            Console.WriteLine();
            Console.WriteLine("Partea 2:");
            Console.WriteLine("---------");
            DemoMin();
        }

        private static void DemoMin()
        {
            var set1 = new double[] {3, -4, 1};
            var set2 = new double[] {4, -12, 13, -6, 1};

            var fn1 = new AlgFunction(set1);
            var fn2 = new AlgFunction(set2);

            Console.WriteLine("Result 1: " + string.Join(", ", fn1.ComputeMinList()));
            Console.WriteLine("Result 2: " + string.Join(", ", fn2.ComputeMinList()));
        }

        private static void DemoLaguerre()
        {
            var pol = new Polynom() {Coefficients = new double[] {-6, 11, -6, 1}.ToList()};
            Console.WriteLine("Polinom: " + pol.NiceString());
            var res = pol.LaguerreSolutions();
            Console.WriteLine("Solutii:");
            using (var writer = new StreamWriter("solutions.txt"))
            {
                res.ToList().ForEach(x =>
                {
                    Console.WriteLine(x);
                    writer.WriteLine(x);
                });
            }
            Console.WriteLine("Scris {0} solutii", res.Length);
        }
    }
}
