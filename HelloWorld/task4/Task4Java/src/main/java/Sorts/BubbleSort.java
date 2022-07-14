package Sorts;

public class BubbleSort {
    public static int[] sort(int[] array) {
        boolean isSorted = false;
        int[] result = array;
        int buf;
        while (!isSorted) {
            isSorted = true;
            for (int i = 0; i < result.length - 1; i++) {
                if (result[i] > result[i + 1]) {
                    isSorted = false;

                    buf = result[i];
                    result[i] = result[i + 1];
                    result[i + 1] = buf;
                }
            }
        }
        return result;
    }
}
