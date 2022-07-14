namespace Task4Cs
{
    public static class QuickSort
    {
        public static void Run(int[] arr, long first, long last)
        {
            int p = arr[(last - first) / 2 + first];// ищем средний элемент
            
            long i = first, j = last;
            while (i <= j)
            {
                while (arr[i] < p && i <= last) ++i;
                while (arr[j] > p && j >= first) --j;
                if (i <= j)
                {
                    (arr[i], arr[j]) = (arr[j], arr[i]);
                    ++i; --j;
                }
            }
            if (j > first) Run(arr, first, j);
            if (i < last) Run(arr, i, last);
        }

        public static void Run(int[] array)
        {
            Run(array, 0, array.Length - 1);
        }
    }
}