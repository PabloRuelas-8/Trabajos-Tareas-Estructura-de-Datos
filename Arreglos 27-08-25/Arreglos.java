import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        int[] numeros = {10, 20, 30, 40};

        numeros[2] = 45;

        for (int n : numeros)
            System.out.println(n);

        Scanner sc = new Scanner(System.in);
        System.out.print("cuantos números?: ");
        int tamaño = sc.nextInt();
        int[] arr = new int[tamaño];
        for (int i = 0; i < tamaño; i++) {
            System.out.print("ingrese valor " + (i+1) + ": ");
            arr[i] = sc.nextInt();
        }
        sc.close();
    }
}