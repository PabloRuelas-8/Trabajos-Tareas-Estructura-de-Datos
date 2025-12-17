public class Listas_Enlazadas {

    static class Nodo {
        int data;
        Nodo next;

        public Nodo(int data) {
            this.data = data;
            this.next = null;
        }
    }

    static class ListaEnlazada {
        private Nodo head;

        public ListaEnlazada() {
            head = null;
        }

        public void insertar_al_principio(int data) {
            Nodo nuevo = new Nodo(data);
            nuevo.next = head;
            head = nuevo;
        }

        public void insertar_al_final(int data) {
            Nodo nuevo = new Nodo(data);
            if (head == null) {
                head = nuevo;
                return;
            }
            Nodo ultimo = head;
            while (ultimo.next != null) {
                ultimo = ultimo.next;
            }
            ultimo.next = nuevo;
        }

        public void imprimir_lista() {
            Nodo actual = head;
            while (actual != null) {
                System.out.print(actual.data + " -> ");
                actual = actual.next;
            }
            System.out.println("None");
        }

        public boolean buscar(int data) {
            Nodo actual = head;
            while (actual != null) {
                if (actual.data == data) return true;
                actual = actual.next;
            }
            return false;
        }

        public void eliminar(int data) {
            Nodo actual = head;
            Nodo previo = null;

            if (actual != null && actual.data == data) {
                head = actual.next;
                return;
            }

            while (actual != null && actual.data != data) {
                previo = actual;
                actual = actual.next;
            }

            if (actual == null) return;

            previo.next = actual.next;
        }
    }

    public static void main(String[] args) {
        ListaEnlazada lista = new ListaEnlazada();
        lista.insertar_al_final(10);
        lista.insertar_al_final(20);
        lista.insertar_al_final(30);
        System.out.print("Lista inicial: ");
        lista.imprimir_lista();

        lista.insertar_al_principio(5);
        System.out.print("Lista después de insertar al principio: ");
        lista.imprimir_lista();

        System.out.println("¿Esta el 20? " + lista.buscar(20));
        System.out.println("¿Esta el 99? " + lista.buscar(99));

        lista.eliminar(20);
        System.out.print("Lista después de eliminar 20: ");
        lista.imprimir_lista();

        lista.eliminar(5);
        System.out.print("Lista después de eliminar 5: ");
        lista.imprimir_lista();
    }
}