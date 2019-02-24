using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;

namespace PerfProblem
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Task.Run(async () => await ComputeSlowly());

            Task.Run(() => Compute(new ComputeSth2()));
            var compute = new ComputeSth();
            await Task.Run(() => compute.ComputeBruteForce());
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void Compute(ComputeSth2 bum)
        {
            bum.Bac();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static async Task ComputeSlowly()
        {
            int i = 0;
            while (true)
            {
                await Task.Delay(10);
                i++;
            }
        }
    }

    class ComputeSth2
    {
        private Random random = new Random();

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public void Bac()
        {
            while (true)
            {
                Pat(Mat());
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        int Mat()
        {
            return random.Next();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        void Pat(int seed)
        {
            seed = seed * 2;
        }
    }

    class ComputeSth
    {
        private double x;

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public void ComputeBruteForce()
        {
            int i = 0;
            while (true)
            {
                i += ComputeHelper(i);
            }

        }

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        private int ComputeHelper(int i)
        {
            return ConputeHelper2(i);
        }

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        private int ConputeHelper2(int i)
        {
            return ComputeHelper3(i);
        }

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        private int ComputeHelper3(int i)
        {
            x = Math.Pow(i,i);
            return ComputeHelper4(i);
        }

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        private int ComputeHelper4(int i)
        {
            return i + i;
        }
    }
}
