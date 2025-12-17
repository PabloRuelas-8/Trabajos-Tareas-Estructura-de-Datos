using System;

public class NodoDoble
{
    public int data;
    public NodoDoble next;
    public NodoDoble prev;

    public NodoDoble(int d)
    {
        data = d;
        next = null;
        prev = null;
    }
}

public class ListaDoblementeCircular
{
    private NodoDoble head;

    public ListaDoblementeCircular()
    {
        head = null;
    }

    public bool IsEmpty()
    {
        return head == null;
    }

    public void AddInicio(int data)
    {
        NodoDoble nuevo = new NodoDoble(data);

        if (IsEmpty())
        {
            head = nuevo;
            head.next = head;
            head.prev = head;
        }
        else
        {
            NodoDoble tail = head.prev;
            nuevo.next = head;
            head.prev = nuevo;
            head = nuevo;
            head.prev = tail;
            tail.next = head;
        }
    }

    public void AddFinal(int data)
    {
        NodoDoble nuevo = new NodoDoble(data);

        if (IsEmpty())
        {
            head = nuevo;
            head.next = head;
            head.prev = head;
        }
        else
        {
            NodoDoble tail = head.prev;
            tail.next = nuevo;
            nuevo.prev = tail;
            nuevo.next = head;
            head.prev = nuevo;
        }
    }

    public void PrintLista()
    {
        if (IsEmpty())
        {
            Console.WriteLine("None");
            return;
        }

        NodoDoble actual = head;
        do
        {
            Console.Write(actual.data + " <-> ");
            actual = actual.next;
        } while (actual != head);

        Console.WriteLine("(vuelve a " + head.data + ")");
    }

    public bool Search(int data)
    {
        if (IsEmpty()) return false;

        NodoDoble actual = head;
        do
        {
            if (actual.data == data) return true;
            actual = actual.next;
        } while (actual != head);

        return false;
    }

    public void Eliminar(int data)
    {
        if (IsEmpty()) return;

        NodoDoble actual = head;

        do
        {
            if (actual.data == data) break;
            actual = actual.next;
        } while (actual != head);

        if (actual.data != data) return; 

        if (actual == head && head.next == head)
        {
            head = null;
            return;
        }

        if (actual == head)
            head = actual.next;

        actual.prev.next = actual.next;
        actual.next.prev = actual.prev;
    }
}

public class MainClass
{
    public static void Main(string[] args)
    {
        ListaDoblementeCircular lista = new ListaDoblementeCircular();
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