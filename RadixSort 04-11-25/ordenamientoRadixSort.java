import java.util.Arrays;

public class ordenamientoRadixSort {

    static int obtenerMaximo(int[] arreglo) {
        return Arrays.stream(arreglo).max().getAsInt();
    }

    static void ordenamientoPorConteo(int[] arreglo, int exponente) {
        int tamano = arreglo.length;
        int[] arregloSalida = new int[tamano];
        int[] arregloConteo = new int[10]; 

        for (int j = 0; j < tamano; j++) {
            int indice = (arreglo[j] / exponente);
            arregloConteo[indice % 10]++;
        }

        for (int j = 1; j < 10; j++) {
            arregloConteo[j] += arregloConteo[j - 1];
        }

        for (int j = tamano - 1; j >= 0; j--) {
            int indice = (arreglo[j] / exponente);
            arregloSalida[arregloConteo[indice % 10] - 1] = arreglo[j];
            arregloConteo[indice % 10]--;
        }

       
        for (int j = 0; j < tamano; j++) {
            arreglo[j] = arregloSalida[j];
        }
    }

    static void hacerRadixSort(int[] arreglo) {
        int maximo = obtenerMaximo(arreglo);

        for (int exponente = 1; maximo / exponente > 0; exponente *= 10) {
            ordenamientoPorConteo(arreglo, exponente);
        }
    }

    public static void main(String[] args) {
        int[] arr = { 171, 46, 76, 91, 803, 25, 3, 67 };
        
        System.out.println("Arreglo antes de ordenar:");
        System.out.println(Arrays.toString(arr));

        hacerRadixSort(arr);

        System.out.println("Arreglo despues de ordenar:");
        System.out.println(Arrays.toString(arr));
    }
}