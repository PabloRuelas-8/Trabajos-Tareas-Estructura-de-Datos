public class quick_sort {
    static int partition(int[] a, int l, int h) {
        int pvt = a[h];
        int j = l - 1;
        for (int k = l; k < h; k++) {
            if (a[k] < pvt) {
                j++;
                swap(a, j, k);
            }
        }
        swap(a, j + 1, h);
        return j + 1;
    }

    static void swap(int[] a, int i, int j) {
        int tmp = a[i];
        a[i] = a[j];
        a[j] = tmp;
    }

    static void quickSort(int[] a, int l, int h) {
        if (l < h) {
            int pi = partition(a, l, h);
            quickSort(a, l, pi - 1);
            quickSort(a, pi + 1, h);
        }
    }

    public static void main(String[] args) {
        int[] a = {10, 7, 8, 9, 1, 5};
        System.out.println("El arreglo antes de ordenarlo:");
        for (int v : a) System.out.print(v + " ");
        System.out.println();

        quickSort(a, 0, a.length - 1);

        System.out.println("El arreglo despues de ordenarlo:");
        for (int v : a) System.out.print(v + " ");
        System.out.println();
    }
}
