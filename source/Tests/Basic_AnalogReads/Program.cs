﻿using System;
using System.Threading;
using Meadow;

namespace Basic_AnalogReads
{
    class MainClass
    {
        static IApp app;

        public static void Main(string[] args)
        {
            app = new AnalogReadApp();
            Thread.Sleep(Timeout.Infinite);
        }
    }
}
