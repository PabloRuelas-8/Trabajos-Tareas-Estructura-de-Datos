import java.util.Scanner;

public class Colas_Arreglos {

    private static final int MAXSIZE = 5;
    private static int[] queue = new int[MAXSIZE];
    private static int front = -1;
    private static int rear = -1;
    private static Scanner scanner = new Scanner(System.in);

    public static void insertar() {
        int elemento;
        System.out.print("\nIngrese el elemento que se va a insertar ");
        if (scanner.hasNextInt()) {
            elemento = scanner.nextInt();
        } else {
            System.out.println("\nEntrada inválida.");
            scanner.next();
            return;
        }

        if (rear == MAXSIZE - 1) {
            System.out.println("\nDESBORDAMIENTO (OVERFLOW)\n");
            return;
        }
        if (front == -1 && rear == -1) {
            front = rear = 0;
        } else {
            rear++;
        }

        queue[rear] = elemento;
        System.out.println("\nElemento insertado correctamente.\n");
    }

    public static void eliminar() {
        if (front == -1 || front > rear) {
            System.out.println("\nSUBDESBORDAMIENTO (UNDERFLOW)\n");
            return;
        }

        int elemento = queue[front];
        if (front == rear) {
            front = rear = -1;
        } else {
            front++;
        }

        System.out.println("\nElemento correctamente eliminado: " + elemento + "\n");
    }

    public static void mostrar() {
        if (rear == -1 || front == -1 || front > rear) {
            System.out.println("\nLa cola está vacía.\n");
        } else {
            System.out.println("\nElementos en la cola:");
            for (int i = front; i <= rear; i++) {
                System.out.println(queue[i]);
            }
        }
    }

    public static void main(String[] args) {
        int opcion = 0;

        while (opcion != 4) {
            System.out.println("\n========== MENÚ PRINCIPAL ==========");
            System.out.println("1. Insertar un elemento");
            System.out.println("2. Eliminar un elemento");
            System.out.println("3. Mostrar la cola");
            System.out.println("4. Salir");
            System.out.print("Ingrese su opción: ");
            
            if (scanner.hasNextInt()) {
                opcion = scanner.nextInt();
                switch (opcion) {
                    case 1: insertar(); break;
                    case 2: eliminar(); break;
                    case 3: mostrar(); break;
                    case 4: System.out.println("\nCerrando...\n"); break;
                    default: System.out.println("\nOpción inválida.\n"); break;
                }
            } else {
                System.out.println("\nOpción inválida.\n");
                scanner.next();
            }
        }
        scanner.close();
    }
}
