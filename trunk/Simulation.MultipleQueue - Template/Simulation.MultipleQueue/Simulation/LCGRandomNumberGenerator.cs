using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultipleQueue.Simulation
{
    static class LCGRandomNumberGenerator
    {
        static Random rand = new Random();
        static int M = 1000;
        static int a = 5;
        static int c = 3;
        static int X = 1;
        public static float GetVariate()
        {
            //X = (a * X + c) % M;

            //return (float)X / M;
            return (float)rand.NextDouble();
        }
    }
}
