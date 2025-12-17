using System;
using System.Collections.Generic;

public class Pila_Arreglos
{
    private int[] stack;
    private int maxSize;
    private int top;

    public Pila_Arreglos(int tam)
    {
        maxSize = tam;
        stack = new int[maxSize];
        top = -1;
    }

    public bool IsEmpty()
    {
        return top == -1;
    }

    public bool IsFull()
    {
        return top == maxSize - 1;
    }

    public void Push(int data)
    {
        if (IsFull())
        {
            Console.WriteLine("Error: Overflow de Pila");
            return;
        }
        stack[++top] = data;
    }

    public int Pop()
    {
        if (IsEmpty())
        {
            Console.WriteLine("Error: Underflow de Pila");
            return -1;
        }
        return stack[top--];
    }

    public int Peek()
    {
        if (IsEmpty())
        {
            Console.WriteLine("La pila está vacía");
            return -1;
        }
        return stack[top];
    }

    public static void Main(string[] args)
    {
        PilaArreglo pila = new PilaArreglo(100);
        pila.Push(10);
        pila.Push(20);
        pila.Push(30);

        Console.WriteLine("Elemento superior(TOP): " + pila.Peek());
        Console.WriteLine("Extrae elemento: " + pila.Pop());
        Console.WriteLine("Nuevo elemento superior: " + pila.Peek());
    }
}