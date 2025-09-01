using System;

class Program {
    static void Main() {
        int[] numeros = {10, 20, 30, 40};

        numeros[2] = 45;

        foreach (int n in numeros)
            Console.WriteLine(n);

        Console.Write("cuantos números?: ");
        int tamaño = int.Parse(Console.ReadLine());
        int[] arr = new int[tamaño];
        for(int i = 0; i < tamaño; i++) {
            Console.Write($"ingrese valor {i+1}: ");
            arr[i] = int.Parse(Console.ReadLine());
        }
    }
}
