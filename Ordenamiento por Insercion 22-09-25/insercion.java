
public class insercion {
    
    public static void insertionSort(int[] a) {
        for (int i = 1; i < a.length; i++) {
            int temp = a[i];
            int j = i - 1;
            while (j >= 0 && temp < a[j]) {
                a[j + 1] = a[j];
                j--;
            }
            a[j + 1] = temp;
        }
    }

    public static void printArr(int[] a) {
        for (int num : a) {
            System.out.print(num + " ");
        }
        System.out.println();
    }

    public static void main(String[] args) {
        int[] a = {33, 55, 77, 22, 88, 11, 99, 44, 66};

        System.out.println("Antes de ordenar los elementos del arreglo:");
        printArr(a);

        insertionSort(a);

        System.out.println("Despues de ordenar los elementos del arreglo:");
        printArr(a);
    }

    
}
