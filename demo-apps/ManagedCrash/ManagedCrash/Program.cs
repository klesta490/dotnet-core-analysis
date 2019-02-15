using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ManagedCrash
{
    class Program
    {
        static void Main(string[] args)
        {
            
            int seconds = 10;
            if (args.Length >= 1)
                seconds = int.Parse(args[0]);

            Console.WriteLine("Crash scheduled in {0} sec", seconds);

            Task.Run(() => ForeverWait());
            DoCrashIn(seconds);

        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        static void ForeverWait()
        {
            while (true)
            {
                Task.Delay(1000).Wait();
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        static void DoCrashIn(int seconds)
        {
            Task.Delay(TimeSpan.FromSeconds(seconds)).Wait();
            Crash();
            Console.WriteLine("I should ever get here.");
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void Crash()
        {
            throw new ApplicationException("Whooooups");
        }
    }
}
