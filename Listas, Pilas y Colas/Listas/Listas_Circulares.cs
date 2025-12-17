using System;

public class Nodo
{
    public int data;
    public Nodo next;

    public Nodo(int val)
    {
        data = val;
        next = null;
    }
}

public class ListaCircularEnlazada
{
    public Nodo head;

    public ListaCircularEnlazada()
    {
        head = null;
    }

    public void AddInicio(int data)
    {
        Nodo nuevoNodo = new Nodo(data);
        if (head == null)
        {
            head = nuevoNodo;
            nuevoNodo.next = head;
        }
        else
        {
            Nodo tail = head;
            while (tail.next != head)
            {
                tail = tail.next;
            }
            nuevoNodo.next = head;
            head = nuevoNodo;
            tail.next = head;
        }
    }

    public void AddFinal(int data)
    {
        Nodo nuevoNodo = new Nodo(data);
        if (head == null)
        {
            head = nuevoNodo;
            nuevoNodo.next = head;
        }
        else
        {
            Nodo tail = head;
            while (tail.next != head)
            {
                tail = tail.next;
            }
            tail.next = nuevoNodo;
            nuevoNodo.next = head;
        }
    }

    public void PrintLista()
    {
        if (head == null)
        {
            Console.WriteLine("null");
            return;
        }
        
        Nodo nodoActual = head;
        do
        {
            Console.Write(nodoActual.data + " -> ");
            nodoActual = nodoActual.next;
        } while (nodoActual != head);
        
        Console.WriteLine("(vuelve a " + head.data + ")");
    }

    public bool Search(int dataBuscada)
    {
        if (head == null)
        {
            return false;
        }
        
        Nodo nodoActual = head;
        do
        {
            if (nodoActual.data == dataBuscada)
            {
                return true;
            }
            nodoActual = nodoActual.next;
        } while (nodoActual != head);
        
        return false;
    }

    public void Eliminar(int dataAEliminar)
    {
        if (head == null)
        {
            return;
        }

        if (head.data == dataAEliminar)
        {
            if (head.next == head)
            {
                head = null;
                return;
            }
            
            Nodo tail = head;
            while (tail.next != head)
            {
                tail = tail.next;
            }
            
            head = head.next;
            tail.next = head;
            return;
        }

        Nodo nodoPrevio = head;
        Nodo nodoActual = head.next;
        while (nodoActual != head)
        {
            if (nodoActual.data == dataAEliminar)
            {
                nodoPrevio.next = nodoActual.next;
                return;
            }
            nodoPrevio = nodoActual;
            nodoActual = nodoActual.next;
        }
    }
}

public class MainClass
{
    public static void Main(string[] args)
    {
        ListaCircularEnlazada miLista = new ListaCircularEnlazada();
        miLista.AddFinal(10);
        miLista.AddFinal(20);
        miLista.AddFinal(30);
        miLista.PrintLista();

        miLista.AddInicio(5);
        miLista.PrintLista();

        Console.WriteLine("¿Está el 20? " + miLista.Search(20));
        Console.WriteLine("¿Está el 99? " + miLista.Search(99));

        miLista.Eliminar(20);
        miLista.PrintLista();
        
        miLista.Eliminar(5);
        miLista.PrintLista();
        
        miLista.Eliminar(30);
        miLista.PrintLista();
    }
}