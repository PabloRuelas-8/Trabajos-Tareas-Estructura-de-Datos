using System;

class Program {
    static void imprimir(int[] arre) {
        Console.Write("El arreglo es: ");
        foreach (int x in arre) {
            Console.Write(x + " ");
        }
        Console.WriteLine();
    }

    static void insertar_elemento(int[] arre, int val, int indice) {
        for (int i = arre.Length - 1; i > indice; i--) {
            arre[i] = arre[i - 1];
        }
        arre[indice] = val;
    }

    static int busqueda_lineal(int[] arre, int obj) {
        for (int i = 0; i < arre.Length; i++) {
            if (arre[i] == obj) {
                return i;
            }
        }
        return -1;
    }

    static void Main() {
        int[] arre = {1, 2, 3, 4, 5};
        imprimir(arre);

        Console.Write("Valor para insertar: ");
        int val = int.Parse(Console.ReadLine());

        Console.Write($"Indice donde se va a insertar (0..{arre.Length - 1}): ");
        int indice = int.Parse(Console.ReadLine());

        insertar_elemento(arre, val, indice);
        imprimir(arre);

        int obj = val;
        int pos = busqueda_lineal(arre, obj);
        Console.WriteLine($"Búsqueda lineal de {obj}: indice {pos}");

        Console.WriteLine("\nPresiona enter para salir");
        Console.ReadLine();
    }
}
