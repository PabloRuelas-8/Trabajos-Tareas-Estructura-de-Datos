import java.util.Scanner;

public class Main {

    static void imprimir(int[] arre) {
        System.out.print("El arreglo es: ");
        for (int x : arre) {
            System.out.print(x + " ");
        }
        System.out.println();
    }

    static void insertar_elemento(int[] arre, int val, int indice) {
        for (int i = arre.length - 1; i > indice; i--) {
            arre[i] = arre[i - 1];
        }
        arre[indice] = val;
    }

    static int busqueda_lineal(int[] arre, int obj) {
        for (int i = 0; i < arre.length; i++) {
            if (arre[i] == obj) {
                return i;
            }
        }
        return -1;
    }

    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);

        int[] arre = {1, 2, 3, 4, 5};
        imprimir(arre);

        System.out.print("Valor para insertar: ");
        int val = sc.nextInt();

        System.out.print("Indice donde se va a insertar (0.." + (arre.length - 1) + "): ");
        int indice = sc.nextInt();

        insertar_elemento(arre, val, indice);
        imprimir(arre);

        int obj = val;
        int pos = busqueda_lineal(arre, obj);
        System.out.println("Busqueda lineal de " + obj + ": indice " + pos);

        System.out.println("\nPresiona enter para salir");
        try {
            System.in.read();
        } catch (Exception e) {}
    }
}
