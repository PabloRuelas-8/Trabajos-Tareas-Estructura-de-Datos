import java.util.Arrays;

public class hash {

    public static void mostrarArreglo(int[] arreglo) {
        System.out.println(Arrays.toString(arreglo));
    }

    public void ordenar(int[] arreglo) {
        int tamano = arreglo.length;
        int tamanoBrecha = tamano / 2;

        while (tamanoBrecha > 0) {
            for (int j = tamanoBrecha; j < tamano; j++) {
                int valor = arreglo[j];
                int k = j;

                while (k >= tamanoBrecha && arreglo[k - tamanoBrecha] > valor) {
                    arreglo[k] = arreglo[k - tamanoBrecha];
                    k = k - tamanoBrecha;
                }
                arreglo[k] = valor;
            }
            tamanoBrecha = tamanoBrecha / 2;
        }
    }

    public static void main(String[] args) {
        int[] arreglo = { 36, 34, 43, 11, 15, 20, 28, 45 };

        System.out.println("Arreglo antes de ser ordenado:");
        hash.mostrarArreglo(arreglo);

        hash obj = new hash();
        obj.ordenar(arreglo);

        System.out.println("Arreglo despues de ser ordenado:");
        hash.mostrarArreglo(arreglo);
    }
}
