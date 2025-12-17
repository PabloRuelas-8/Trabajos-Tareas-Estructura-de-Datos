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

public class ListaEnlazada
{
    private Nodo head;

    public ListaEnlazada()
    {
        head = null;
    }

    public void InsertarAlPrincipio(int data)
    {
        Nodo nuevo = new Nodo(data);
        nuevo.next = head;
        head = nuevo;
    }

    public void InsertarAlFinal(int data)
    {
        Nodo nuevo = new Nodo(data);
        if (head == null)
        {
            head = nuevo;
            return;
        }
        Nodo ultimo = head;
        while (ultimo.next != null)
        {
            ultimo = ultimo.next;
        }
        ultimo.next = nuevo;
    }

    public void ImprimirLista()
    {
        Nodo actual = head;
        while (actual != null)
        {
            Console.Write(actual.data + " -> ");
            actual = actual.next;
        }
        Console.WriteLine("None");
    }

    public bool Buscar(int data)
    {
        Nodo actual = head;
        while (actual != null)
        {
            if (actual.data == data) return true;
            actual = actual.next;
        }
        return false;
    }

    public void Eliminar(int data)
    {
        Nodo actual = head;
        Nodo previo = null;

        if (actual != null && actual.data == data)
        {
            head = actual.next;
            return;
        }

        while (actual != null && actual.data != data)
        {
            previo = actual;
            actual = actual.next;
        }

        if (actual == null) return;

        previo.next = actual.next;
    }

    public static void Main(string[] args)
    {
        ListaEnlazada lista = new ListaEnlazada();
        lista.InsertarAlFinal(10);
        lista.InsertarAlFinal(20);
        lista.InsertarAlFinal(30);
        Console.Write("Lista inicial: ");
        lista.ImprimirLista();

        lista.InsertarAlPrincipio(5);
        Console.Write("Lista después de insertar al principio: ");
        lista.ImprimirLista();

        Console.WriteLine("¿Esta el 20? " + lista.Buscar(20));
        Console.WriteLine("¿Esta el 99? " + lista.Buscar(99));

        lista.Eliminar(20);
        Console.Write("Lista después de eliminar 20: ");
        lista.ImprimirLista();

        lista.Eliminar(5);
        Console.Write("Lista después de eliminar 5: ");
        lista.ImprimirLista();
    }
}