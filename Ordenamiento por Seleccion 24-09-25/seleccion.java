public class seleccion {

    static void selection(int[] a) {
        for (int i = 0; i < a.length; i++) {
            int small = i;
            for (int j = i + 1; j < a.length; j++) {
                if (a[small] > a[j]) {
                    small = j;
                }
            }
            int temp = a[i];
            a[i] = a[small];
            a[small] = temp;
        }
    }

    static void printArr(int[] a) {
        for (int i = 0; i < a.length; i++) {
            System.out.print(a[i] + " ");
        }
    }

    public static void main(String[] args) {
        int[] a = {66, 11, 88, 44, 99, 33, 55, 22};

        System.out.println("Arreglo antes de ser ordenado:");
        printArr(a);

        selection(a);

        System.out.println("\nArreglo despues de ser ordenado:");
        printArr(a);
    }
}
