public class Pila_Arreglos {
    
    private int[] stack;
    private int maxSize;
    private int top;

    public Pila_Arreglos(int tam) {
        maxSize = tam;
        stack = new int[maxSize];
        top = -1;
    }

    public boolean isEmpty() {
        return top == -1;
    }

    public boolean isFull() {
        return top == maxSize - 1;
    }

    public void push(int data) {
        if (isFull()) {
            System.out.println("Error: Overflow de Pila");
            return;
        }
        top++;
        stack[top] = data;
    }

    public int pop() {
        if (isEmpty()) {
            System.out.println("Error: Underflow de Pila");
            return -1;
        }
        int data = stack[top];
        top--;
        return data;
    }

    public int peek() {
        if (isEmpty()) {
            System.out.println("La pila está vacía");
            return -1;
        }
        return stack[top];
    }

    public static void main(String[] args) {
        Pila_Arreglos pila = new Pila_Arreglos(100);
        pila.push(10);
        pila.push(20);
        pila.push(30);

        System.out.println("Elemento superior(TOP): " + pila.peek());
        System.out.println("Extrae elemento: " + pila.pop());
        System.out.println("Nuevo elemento superior: " + pila.peek());
    }
}