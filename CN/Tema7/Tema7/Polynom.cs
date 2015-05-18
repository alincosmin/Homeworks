using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Tema7
{
    public class Polynom
    {
        public double Precision = Math.Pow(10, -10); 

        public List<double> Coefficients { get; set; }

        public Polynom()
        {
            Coefficients = new List<double>();
        }

        public Polynom Derivative1()
        {
            var result = new Polynom();

            for (var i = 1; i < Coefficients.Count; i++)
            {
                result.Coefficients.Add(Coefficients[i]*i);
            }

            return result;
        }

        public Polynom Derivative2()
        {
            var result = new Polynom();
            var derivative = Derivative1().Coefficients;
            for (var i = 1; i < derivative.Count; i++)
            {
                result.Coefficients.Add(derivative[i] * i);
            }

            return result;
        }

        public string NiceString()
        {
            var builder = new StringBuilder();

            var maxPow = Coefficients.Count - 1;
            while (maxPow > 0)
            {
                var coeff = Coefficients[maxPow] > 0 ? "+" + Coefficients[maxPow] : Coefficients[maxPow].ToString();
                builder.Append(string.Format("{0}(x^{1})", coeff, maxPow));
                maxPow--;
            }

            if (Coefficients[maxPow] != 0)
            {
                var coeff = Coefficients[maxPow] > 0 ? "+" + Coefficients[maxPow] : Coefficients[maxPow].ToString();
                builder.Append(coeff);
            }

            return builder.ToString();
        }

        public double ComputeValue(double x)
        {
            var b = Coefficients.Last();

            for (var i = Coefficients.Count - 2; i >= 0; i--)
            {
                b = Coefficients[i] + b*x;
            }

            return b;
        }

        public int Sign(double val)
        {
            return val >= 0 ? 1 : -1;
        }

        public double ComputeH(double x)
        {
            var n = Coefficients.Count - 1;

            var result = Math.Pow(n - 1, 2)*Math.Pow(Derivative1().ComputeValue(x), 2) -
                         n*(n - 1)*ComputeValue(x)*Derivative2().ComputeValue(x);

            return result;
        }

        public double ComputeDelta(double x)
        {
            var n = Coefficients.Count - 1;

            var up = n*ComputeValue(x);
            var val1 = Derivative1().ComputeValue(x);
            var down = val1 + Sign(val1)*Math.Sqrt(ComputeH(x));

            return up/down;
        }

        public double LaguerreComputation()
        {
            double delta = 0;
            int k = 0;
            var random = new Random(DateTime.Now.Millisecond);
            var R = (Math.Abs(Coefficients.Last()) + Coefficients.Max())/Math.Abs(Coefficients.Last());

            var x = random.NextDouble()*R*Sign(random.Next(-1, 1));

            do
            {
                delta = ComputeDelta(x);
                if(ComputeH(x) < 0) break;

                var val1 = Derivative1().ComputeValue(x);
                var down = val1 + Sign(val1) * Math.Sqrt(ComputeH(x));
                if(Math.Abs(down) <= Precision) break;

                x -= delta;
                k++;

            } while (Math.Abs(delta) >= Precision && k <= 1000 && Math.Abs(delta) <= Math.Pow(10, 8));

            if (Math.Abs(delta) < Precision) return x;
            
            throw new InvalidDataException("Divergenta");
        }

        public double[] LaguerreSolutions()
        {
            var solutions = new List<double>();

            double sol = 0;
            do
            {
                try
                {
                    var sol2 = LaguerreComputation();
                    if(Math.Abs(sol2 - sol) < Precision)
                        break;

                    sol = sol2;

                    if (solutions.All(x => Math.Abs(x - sol) > Precision))
                    {
                        solutions.Add(sol);
                    }
                }
                catch (InvalidDataException){}

            } while (true);

            return solutions.ToArray();
        }
    }
}