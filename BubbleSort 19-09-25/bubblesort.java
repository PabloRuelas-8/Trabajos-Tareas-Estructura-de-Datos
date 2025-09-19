public class bubblesort {
    public static void main(String[] args) {
        int[] arreglo = {44, 66, 88, 55, 33, 11, 22, 77};

        System.out.println("Arreglo original:");
        for (int num : arreglo) {
            System.out.print(num + " ");
        }
        System.out.println();

        int n = arreglo.length;
        
        for (int i = 0; i < n - 1; i++) {
            for (int j = 0; j < n - i - 1; j++) {
                if (arreglo[j] > arreglo[j + 1]) {
                    int aux = arreglo[j];
                    arreglo[j] = arreglo[j + 1];
                    arreglo[j + 1] = aux;
                }
            }
        }

        System.out.println("Arreglo ordenado de menor a mayor:");
        for (int num : arreglo) {
            System.out.print(num + " ");
        }
    }
}

