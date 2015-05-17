using System;
using System.IO;
using System.Linq;

namespace Tema7
{
    class Program
    {
        static void Main(string[] args)
        {
            DemoLaguerre();
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
