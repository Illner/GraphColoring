using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.MyMath
{
    static class MyMath
    {
        // Variable
        #region
        private static Random random = new Random();
        #endregion

        // Method
        #region
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

        /// <summary>
        /// Fisher–Yates shuffle
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void FisherYatesShuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        #endregion
    }
}
