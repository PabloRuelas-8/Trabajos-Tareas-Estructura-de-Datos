using System;

public class Colas_Arreglos
{
    const int MAXSIZE = 5;
    static int[] queue = new int[MAXSIZE];
    static int front = -1;
    static int rear = -1;

    public static void Insertar()
    {
        Console.Write("\nIngrese el elemento que se va a insertar ");
        string input = Console.ReadLine();
        if (!int.TryParse(input, out int elemento))
        {
            Console.WriteLine("\nEntrada inválida.");
            return;
        }

        if (rear == MAXSIZE - 1)
        {
            Console.WriteLine("\nDESBORDAMIENTO (OVERFLOW)\n");
            return;
        }
        if (front == -1 && rear == -1)
        {
            front = rear = 0;
        }
        else
        {
            rear++;
        }

        queue[rear] = elemento;
        Console.WriteLine("\nElemento insertado correctamente.\n");
    }

    public static void Eliminar()
    {
        if (front == -1 || front > rear)
        {
            Console.WriteLine("\nSUBDESBORDAMIENTO (UNDERFLOW)\n");
            return;
        }

        int elemento = queue[front];
        if (front == rear)
        {
            front = rear = -1;
        }
        else
        {
            front++;
        }

        Console.WriteLine("\nElemento correctamente eliminado: " + elemento + "\n");
    }

    public static void Mostrar()
    {
        if (rear == -1 || front == -1 || front > rear)
        {
            Console.WriteLine("\nLa cola está vacía.\n");
        }
        else
        {
            Console.WriteLine("\nElementos en la cola:");
            for (int i = front; i <= rear; i++)
            {
                Console.WriteLine(queue[i]);
            }
        }
    }

    public static void Main(string[] args)
    {
        int opcion = 0;

        while (opcion != 4)
        {
            Console.WriteLine("\n========== MENÚ PRINCIPAL ==========");
            Console.WriteLine("1. Insertar un elemento");
            Console.WriteLine("2. Eliminar un elemento");
            Console.WriteLine("3. Mostrar la cola");
            Console.WriteLine("4. Salir");
            Console.Write("Ingrese su opción: ");
            
            string input = Console.ReadLine();
            if (int.TryParse(input, out opcion))
            {
                switch (opcion)
                {
                    case 1: Insertar(); break;
                    case 2: Eliminar(); break;
                    case 3: Mostrar(); break;
                    case 4: Console.WriteLine("\nCerrando...\n"); break;
                    default: Console.WriteLine("\nOpción inválida.\n"); break;
                }
            }
            else if (input != null && input.Length > 0)
            {
                Console.WriteLine("\nOpción inválida.\n");
            }
        }
    }
}