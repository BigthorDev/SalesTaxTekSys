using System;
using System.Collections.Generic;
using System.Text;

namespace TekSystems.Utilities
{
    public class Logger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"---> log > {message}");
        }
    }
}
