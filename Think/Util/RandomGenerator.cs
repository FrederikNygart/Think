using System;
using System.Collections.Generic;
using System.Text;

namespace Think.Util
{
    public class RandomGenerator
    {
        public static double Double(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
    }
}
