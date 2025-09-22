using System;

class Program {
    static void insertionSort(int[] a) {
        for (int i = 1; i < a.Length; i++) {
            int temp = a[i];
            int j = i - 1;
            while (j >= 0 && temp < a[j]) {
                a[j + 1] = a[j];
                j--;
            }
            a[j + 1] = temp;
        }
    }

    static void printArr(int[] a) {
        foreach (int num in a) {
            Console.Write(num + " ");
        }
        Console.WriteLine();
    }

    static void Main() {
        int[] a = {33, 55, 77, 22, 88, 11, 99, 44, 66};

        Console.WriteLine("Antes de ordenar los elementos del arreglo:");
        printArr(a);

        insertionSort(a);

        Console.WriteLine("Despues de ordenar los elementos del arreglo:");
        printArr(a);
    }
}
