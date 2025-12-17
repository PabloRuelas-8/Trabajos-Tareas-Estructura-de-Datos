using System;

public class NodoDoble
{
    public int data;
    public NodoDoble next;
    public NodoDoble prev;

    public NodoDoble(int data)
    {
        this.data = data;
        next = null;
        prev = null;
    }
}

public class ListaDoblementeEnlazada
{
    private NodoDoble head;
    private NodoDoble tail;

    public ListaDoblementeEnlazada()
    {
        head = null;
        tail = null;
    }

    public void AddInicio(int data)
    {
        NodoDoble nuevo = new NodoDoble(data);
        if (head == null)
        {
            head = tail = nuevo;
        }
        else
        {
            nuevo.next = head;
            head.prev = nuevo;
            head = nuevo;
        }
    }

    public void AddFinal (int data)
    {
        NodoDoble nuevo = new NodoDoble(data);
        if (tail == null)
        {
            head = tail = nuevo;
        }
        else
        {
            tail.next = nuevo;
            nuevo.prev = tail;
            tail = nuevo;
        }
    }

    public void PrintLista()
    {
        NodoDoble actual = head;
        while (actual != null)
        {
            Console.Write(actual.data + " <-> ");
            actual = actual.next;
        }
        Console.WriteLine("None");
    }

    public bool Search(int data)
    {
        NodoDoble actual = head;
        while (actual != null)
        {
            if (actual.data == data) return true;
            actual = actual.next;
        }
        return false;
    }

    public void Eliminar(int data)
    {
        NodoDoble actual = head;

        while (actual != null && actual.data != data)
            actual = actual.next;

        if (actual == null) return;

        if (actual.prev != null)
            actual.prev.next = actual.next;
        else
            head = actual.next;

        if (actual.next != null)
            actual.next.prev = actual.prev;
        else
            tail = actual.prev;
    }

    public static void Main(string[] args)
    {
        ListaDoblementeEnlazada lista = new ListaDoblementeEnlazada();
        lista.AddFinal(10);
        lista.AddFinal(20);
        lista.AddFinal(30);
        lista.PrintLista();

        lista.AddInicio(5);
        lista.PrintLista();

        Console.WriteLine("¿Está el 20? " + lista.Search(20));
        Console.WriteLine("¿Está el 99? " + lista.Search(99));

        lista.Eliminar(20);
        lista.PrintLista();

        lista.Eliminar(5);
        lista.PrintLista();

        lista.Eliminar(30);
        lista.PrintLista();
    }
}