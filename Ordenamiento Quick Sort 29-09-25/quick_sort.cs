using System;

class Program
{
    static int Partition(int[] a, int l, int h) {
        int pvt = a[h];
        int j = l - 1;
        for (int k = l; k < h; k++) {
            if (a[k] < pvt) {
                j++;
                Swap(a, j, k);
            }
        }
        Swap(a, j + 1, h);
        return j + 1;
    }

    static void Swap(int[] a, int i, int j) {
        int tmp = a[i];
        a[i] = a[j];
        a[j] = tmp;
    }

    static void QuickSort(int[] a, int l, int h) {
        if (l < h) {
            int pi = Partition(a, l, h);
            QuickSort(a, l, pi - 1);
            QuickSort(a, pi + 1, h);
        }
    }

    static void Main() {
        int[] a = {10, 7, 8, 9, 1, 5};
        Console.WriteLine("El arreglo antes de ordenarlo:");
        Console.WriteLine(string.Join(" ", a));

        QuickSort(a, 0, a.Length - 1);

        Console.WriteLine("El arreglo despues de ordenarlo:");
        Console.WriteLine(string.Join(" ", a));
    }
}
