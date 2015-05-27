using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Tema7
{
    public class AlgFunction
    {
        public Polynom Polynom { get; set; }

        public AlgFunction(double[] coefficients)
        {
            Polynom = new Polynom() {Coefficients = coefficients.ToList()};
        }

        public double ComputeDelta(double x1, double x2)
        {
            var up = (x2 - x1)*ComputeGValue(x2, Math.Pow(10, -5));
            var down = ComputeGValue(x2, Math.Pow(10, -5)) - ComputeGValue(x1, Math.Pow(10, -5));

            return up/down;
        }

        public double ComputeGValue(double x, double h)
        {
            var up = 3*Polynom.ComputeValue(x) - 4*Polynom.ComputeValue(x - h) + Polynom.ComputeValue(x - 2*h);
            return up/(2*h);
        }

        public double ComputeMin()
        {
            var k = 0;
            double delta = 0;

            var random = new Random(DateTime.Now.Millisecond);
            var R = (Math.Abs(Polynom.Coefficients.Last()) + Polynom.Coefficients.Max()) / Math.Abs(Polynom.Coefficients.Last());

            var x1 = random.NextDouble() * R * Polynom.Sign(random.Next(-1, 1));
            var x2 = random.NextDouble() * R * Polynom.Sign(random.Next(-1, 1));
            
            do
            {
                delta = ComputeDelta(x1, x2);
                var down = ComputeGValue(x2, Math.Pow(10, -5)) - ComputeGValue(x1, Math.Pow(10, -5));
                if (Math.Abs(down) <= Polynom.Precision)
                    delta = Math.Pow(10, -5);

                x2 = x2 - delta;

            } while (Math.Abs(delta) >= Polynom.Precision && k <= 1000 && Math.Abs(delta) <= Math.Pow(10, 8));

            if (Math.Abs(delta) < Polynom.Precision)
                return x2;

            throw new InvalidDataException("Divergenta");
        }

        public double[] ComputeMinList()
        {
            var list = new List<double>();
            double sol = 0;
            do
            {
                try
                {
                    var sol2 = ComputeMin();
                    if(Math.Abs(sol2 - sol) < Polynom.Precision)
                        break;

                    sol = sol2;

                    if (list.All(x => Math.Abs(x - sol) > Polynom.Precision))
                    {
                        list.Add(sol);
                    }
                }
                catch (InvalidDataException) { }

            } while (true);

            return list.ToArray();
        }
    }
}