using System;

class Program {
    static void Main() {
        int[,] matriz = {
            {1, 2, 3},
            {4, 5, 6},
            {7, 8, 9}
        };

        Console.WriteLine("Matriz original:");
        for (int i = 0; i < 3; i++) {
            Console.Write("[ ");
            for (int j = 0; j < 3; j++) {
                Console.Write(matriz[i, j] + " ");
            }
            Console.WriteLine("]");
        }

        Console.WriteLine("\nEn filas:");
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                Console.Write(matriz[i, j] + " ");
            }
        }

        Console.WriteLine("\nEn columnas:");
        for (int j = 0; j < 3; j++) {
            for (int i = 0; i < 3; i++) {
                Console.Write(matriz[i, j] + " ");
            }
        }
    }
}
