using System;
using BenchmarkDotNet.Attributes;
using Task4Cs;

namespace BenchmarkTest
{
    [MemoryDiagnoser]
    public class SortsBenchmark
    {
        [Params(100, 10000)] 
        public int N;
        public int[] array;

        [GlobalSetup]
        public void Setup()
        {
            array = new int[N];
            Random rand = new Random();
            for (int x = 0; x < array.Length; x++)
            {
                array[x] = rand.Next();
            }
        }

        [Benchmark]
        public void QuickSortTest()
        {
            QuickSort.Run(array);
        }
        
        [Benchmark]
        public void MergeSortTest()
        {
            MergeSort.Run(array);
        }
        
        [Benchmark]
        public void StandardSortTest()
        {
            Array.Sort(array);
        }
        
        [Benchmark]
        public void BubbleSortTest()
        {
            BubbleSort.Run(array);
        }
    }
}