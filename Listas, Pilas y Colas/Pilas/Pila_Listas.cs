using System;

public class Nodo
{
    public int data;
    public Nodo next;

    public Nodo(int d)
    {
        data = d;
        next = null;
    }
}

public class Pila_Listas
{
    private Nodo top;

    public Pila_Listas()
    {
        top = null;
    }

    public bool IsEmpty()
    {
        return top == null;
    }

    public void Push(int data)
    {
        Nodo nuevo = new Nodo(data);
        nuevo.next = top;
        top = nuevo;
    }

    public int Pop()
    {
        if (IsEmpty())
        {
            Console.WriteLine("Error: Underflow de Pila");
            return -1;
        }
        int dato = top.data;
        top = top.next;
        return dato;
    }

    public int Peek()
    {
        if (IsEmpty())
        {
            Console.WriteLine("La Pila esta vacia");
            return -1;
        }
        return top.data;
    }

    public static void Main(string[] args)
    {
        Pila_Listas pila = new Pila_Listas();
        pila.Push(10);
        pila.Push(20);
        pila.Push(30);

        Console.WriteLine("Elemento superior: " + pila.Peek());
        Console.WriteLine("Extrae elemento: " + pila.Pop());
        Console.WriteLine("Nuevo elemento superior: " + pila.Peek());
    }
}