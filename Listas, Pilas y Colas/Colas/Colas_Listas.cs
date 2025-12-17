using System;
using System.Collections.Generic;

public class Nodo
{
    public int data;
    public Nodo next;

    public Nodo(int data)
    {
        this.data = data;
        this.next = null;
    }
}

public class Colas_Listas
{
    public class ColaLista
    {
        private Nodo front, rear;

        public ColaLista()
        {
            this.front = null;
            this.rear = null;
        }

        public bool IsEmpty()
        {
            return front == null;
        }

        public void Enqueue(int data)
        {
            Nodo nuevoNodo = new Nodo(data);

            if (rear == null)
            {
                front = nuevoNodo;
                rear = nuevoNodo;
                return;
            }

            rear.next = nuevoNodo;
            rear = nuevoNodo;
        }

        public int Dequeue()
        {
            if (IsEmpty())
            {
                Console.WriteLine("Error: Cola subdesbordada (Underflow)");
                return -1;
            }

            int data = front.data;
            front = front.next;

            if (front == null)
            {
                rear = null;
            }

            return data;
        }

        public int Peek()
        {
            if (IsEmpty())
            {
                Console.WriteLine("Cola vacía");
                return -1;
            }
            return front.data;
        }
    }

    public static void Main(string[] args)
    {
        ColaLista cola = new ColaLista();

        cola.Enqueue(10);
        cola.Enqueue(20);
        cola.Enqueue(30);

        Console.WriteLine("Elemento frontal: " + cola.Peek());
        Console.WriteLine("Elimina elemento: " + cola.Dequeue());
        Console.WriteLine("Nuevo elemento frontal: " + cola.Peek());
    }
}