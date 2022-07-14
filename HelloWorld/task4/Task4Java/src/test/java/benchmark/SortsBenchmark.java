package benchmark;

import org.openjdk.jmh.annotations.*;

import java.util.Arrays;
import java.util.Random;
import java.util.concurrent.TimeUnit;
import Sorts.*;

import org.openjdk.jmh.annotations.*;
import org.openjdk.jmh.infra.Blackhole;

@State(Scope.Thread)
public class SortsBenchmark {

    @Param({"100", "10000"})
    private int N;
    private int[] array;

    @Setup(Level.Invocation)
    public void setUp() {
        array = new int[N];
        Random rand = new Random();
        for (int i = 0; i < array.length; i++) {
            array[i] = rand.nextInt();
        }
    }

    @Benchmark
    @BenchmarkMode(Mode.AverageTime)
    @OutputTimeUnit(TimeUnit.NANOSECONDS)
    @Fork(value = 1)
    @Measurement(iterations = 10)
    @Warmup(iterations = 1)
    public void QuickSortTest(){
        QuickSort.quickSort(array, 0, N-1);
    }

    @Benchmark
    @BenchmarkMode(Mode.AverageTime)
    @OutputTimeUnit(TimeUnit.NANOSECONDS)
    @Fork(value = 1)
    @Measurement(iterations = 10)
    @Warmup(iterations = 1)
    public void MergeSortTest(){
        MergeSort.mergeSort(array);
    }

    @Benchmark
    @BenchmarkMode(Mode.AverageTime)
    @OutputTimeUnit(TimeUnit.NANOSECONDS)
    @Fork(value = 1)
    @Measurement(iterations = 10)
    @Warmup(iterations = 1)
    public void BubbleSortTest(){
        BubbleSort.sort(array);
    }

    @Benchmark
    @BenchmarkMode(Mode.AverageTime)
    @OutputTimeUnit(TimeUnit.NANOSECONDS)
    @Fork(value = 1)
    @Measurement(iterations = 10)
    @Warmup(iterations = 1)
    public void StandartSortTest(Blackhole bh){ Arrays.sort(array); bh.consume(array); }
}
