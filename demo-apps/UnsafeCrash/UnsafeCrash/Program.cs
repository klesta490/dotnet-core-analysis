using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace UnsafeCrash
{
    class Program
    {
        static void Main(string[] args)
        {
            var t1 = Task.Run(() => Action());
            var t2 = Task.Run(() => { System.Runtime.InteropServices.Marshal.ReadInt32(IntPtr.Zero); });
            Task.WaitAll(t1, t2);
            Console.WriteLine("Hello World!");
            ConcurrentDictionary<string, ulong> a;
            a.GetOrAdd()
        }

        static void Action()
        {
            //Task.Delay(10000).Wait();
            unsafe
            {
                int* f = (int*) 42;
                //int k = *f;

            }
        }
    }
}
