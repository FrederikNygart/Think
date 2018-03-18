﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pensum
{
    public class TestMaterial
    {
        public int Label { get; set; }
        public double[] Values = new double[2];

        public TestMaterial()
        {
            Values[0] = new Random().Next(-50, -1);
            Values[1] = new Random().Next(1, 500);
            setLabel(Values);
        }

        private void setLabel(double[] values)
        {
            Label = values[0] + values[1] < 100 ? -1 : 1;
        }
    }
}
