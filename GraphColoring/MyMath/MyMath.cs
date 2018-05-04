using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.MyMath
{
    static class MyMath
    {
        /// <summary>
        /// Spočítá kombinační číslo
        /// </summary>
        /// <returns>kombinační číslo</returns>
        public static int nCr(int n, int r)
        {
            return (int) (nPr(n, r) / Factorial(r));
        }

        /// <summary>
        /// Spočítá nPr
        /// </summary>
        public static long nPr(int n, int r)
        {
            return FactorialDivision(n, n - r);
        }

        private static long FactorialDivision(int topFactorial, int divisorFactorial)
        {
            long result = 1;
            for (int i = topFactorial; i > divisorFactorial; i--)
                result *= i;
            return result;
        }

        /// <summary>
        /// Spočítá faktorial
        /// </summary>
        /// <param name="i">číslo, prokteré chceme spočítat faktorial</param>
        /// <returns>faktorial</returns>
        private static long Factorial(int i)
        {
            if (i <= 1)
                return 1;

            return i * Factorial(i - 1);
        }
    }
}
