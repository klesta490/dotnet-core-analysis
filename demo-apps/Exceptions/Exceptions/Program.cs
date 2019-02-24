using System;
using System.Threading;

namespace Exceptions
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    int zero = 1 - 1;
                    int x = 5 / zero;
                }
                catch (Exception e)
                {
                    // ignore
                }
                Thread.Sleep(100);
            }
        }
    }
}
