using System;
using System.Collections.Generic;
using System.Text;

namespace Think.Util
{
    public static class RandomGenerator
    {
        private static Random rng = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static double Double(double minimum, double maximum)
        {
            return rng.NextDouble() * (maximum - minimum) + minimum;
        }

        public static int Integer(int maximum)
        {
            return rng.Next(maximum);
        }
    }
}
