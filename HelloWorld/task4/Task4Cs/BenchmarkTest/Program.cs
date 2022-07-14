using BenchmarkDotNet.Running;
using BenchmarkTest;

namespace BenchmarkTest
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<SortsBenchmark>();
        }
    }
}