class Nodo {
    int data;
    Nodo next;

    public Nodo(int data) {
        this.data = data;
        this.next = null;
    }
}

public class Colas_Listas {

    static class ColaLista {
        private Nodo front, rear;

        public ColaLista() {
            this.front = null;
            this.rear = null;
        }

        public boolean isEmpty() {
            return front == null;
        }

        public void enqueue(int data) {
            Nodo nuevoNodo = new Nodo(data);

            if (rear == null) {
                front = nuevoNodo;
                rear = nuevoNodo;
                return;
            }

            rear.next = nuevoNodo;
            rear = nuevoNodo;
        }

        public int dequeue() {
            if (isEmpty()) {
                System.out.println("Error: Cola subdesbordada (Underflow)");
                return -1;
            }

            int data = front.data;
            front = front.next;

            if (front == null) {
                rear = null;
            }

            return data;
        }

        public int peek() {
            if (isEmpty()) {
                System.out.println("Cola vacía");
                return -1;
            }
            return front.data;
        }
    }

    public static void main(String[] args) {
        ColaLista cola = new ColaLista();

        cola.enqueue(10);
        cola.enqueue(20);
        cola.enqueue(30);

        System.out.println("Elemento frontal: " + cola.peek());
        System.out.println("Elimina elemento: " + cola.dequeue());
        System.out.println("Nuevo elemento frontal: " + cola.peek());
    }
}