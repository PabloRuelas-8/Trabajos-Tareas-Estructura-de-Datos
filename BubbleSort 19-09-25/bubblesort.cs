using System;

class Program {
    static void Main() {
        int[] arreglo = {44, 66, 88, 55, 33, 11, 22, 77};

        Console.WriteLine("Arreglo original:");
        Console.WriteLine(string.Join(" ", arreglo));

        int n = arreglo.Length;

        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (arreglo[j] > arreglo[j + 1])
                {
                    int aux = arreglo[j];
                    arreglo[j] = arreglo[j + 1];
                    arreglo[j + 1] = aux;
                }
            }
        }

        Console.WriteLine("Arreglo ordenado de menor a mayor:");
        Console.WriteLine(string.Join(" ", arreglo));
    }
}
