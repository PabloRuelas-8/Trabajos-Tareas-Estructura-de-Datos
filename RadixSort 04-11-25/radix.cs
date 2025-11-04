using System;
using System.Linq;

public class RadixSort
{
    static int ObtenerMaximo(int[] arreglo)
    {
        return arreglo.Max();
    }

    static void OrdenamientoPorConteo(int[] arreglo, int exponente)
    {
        int tamano = arreglo.Length;
        int[] arregloSalida = new int[tamano];
        int[] arregloConteo = new int[10];

        for (int j = 0; j < tamano; j++)
        {
            int indice = (arreglo[j] / exponente);
            arregloConteo[indice % 10]++;
        }

        for (int j = 1; j < 10; j++)
        {
            arregloConteo[j] += arregloConteo[j - 1];
        }

        for (int j = tamano - 1; j >= 0; j--)
        {
            int indice = (arreglo[j] / exponente);
            arregloSalida[arregloConteo[indice % 10] - 1] = arreglo[j];
            arregloConteo[indice % 10]--;
        }

        for (int j = 0; j < tamano; j++)
        {
            arreglo[j] = arregloSalida[j];
        }
    }

    static public void HacerRadixSort(int[] arreglo)
    {
        int maximo = ObtenerMaximo(arreglo);

        for (int exponente = 1; maximo / exponente > 0; exponente *= 10)
        {
            OrdenamientoPorConteo(arreglo, exponente);
        }
    }

    static void ImprimirArreglo(int[] arreglo)
    {
        Console.WriteLine(string.Join(" ", arreglo));
    }

    public static void Main(string[] args)
    {
        int[] arr = { 171, 46, 76, 91, 803, 25, 3, 67 };
        
        Console.WriteLine("Arreglo antes de ordenar:");
        ImprimirArreglo(arr);

        HacerRadixSort(arr);

        Console.WriteLine("Arreglo despues de ordenar:");
        ImprimirArreglo(arr);
    }
}