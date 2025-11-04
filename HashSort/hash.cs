using System;
public class OrdenamientoShell
{
    public static void MostrarArreglo(int[] arreglo)
    {
        Console.WriteLine(string.Join(" ", arreglo));
    }

    public void Ordenar(int[] arreglo)
    {
        int tamano = arreglo.Length;
        int tamanoBrecha = tamano / 2;

        while (tamanoBrecha > 0)
        {
            for (int j = tamanoBrecha; j < tamano; j++)
            {
                int valor = arreglo[j];
                int k = j;

                while (k >= tamanoBrecha && arreglo[k - tamanoBrecha] > valor)
                {
                    arreglo[k] = arreglo[k - tamanoBrecha];
                    k = k - tamanoBrecha;
                }
                arreglo[k] = valor;
            }
            tamanoBrecha = tamanoBrecha / 2;
        }
    }

    public static void Main(string[] args)
    {
        int[] arreglo = { 36, 34, 43, 11, 15, 20, 28, 45 };

        Console.WriteLine("Arreglo antes de ser ordenado:");
        OrdenamientoShell.MostrarArreglo(arreglo);

        OrdenamientoShell obj = new OrdenamientoShell();
        obj.Ordenar(arreglo);

        Console.WriteLine("Arreglo despues de ser ordenado:");
        OrdenamientoShell.MostrarArreglo(arreglo);
    }
}