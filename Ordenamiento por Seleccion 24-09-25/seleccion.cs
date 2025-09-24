using System;

class Program {
    static void Selection(int[] a) {
        for (int i = 0; i < a.Length; i++) {
            int small = i;
            for (int j = i + 1; j < a.Length; j++) {
                if (a[small] > a[j]) {
                    small = j;
                }
            }
            int temp = a[i];
            a[i] = a[small];
            a[small] = temp;
        }
    }

    static void PrintArr(int[] a) {
        for (int i = 0; i < a.Length; i++) {
            Console.Write(a[i] + " ");
        }
    }

    static void Main() {
        int[] a = { 66, 11, 88, 44, 99, 33, 55, 22};

        Console.WriteLine("Arreglo antes de ser ordenado:");
        PrintArr(a);

        Selection(a);

        Console.WriteLine("\nArreglo despues de ser ordenado:");
        PrintArr(a);
    }
}
