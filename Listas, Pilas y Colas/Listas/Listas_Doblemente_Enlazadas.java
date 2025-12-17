public class Listas_Doblemente_Enlazadas {

    static class NodoDoble {
        int data;
        NodoDoble next;
        NodoDoble prev;

        public NodoDoble(int data) {
            this.data = data;
            next = null;
            prev = null;
        }
    }

    static class ListaDoblementeEnlazada {
        private NodoDoble head;
        private NodoDoble tail;

        public ListaDoblementeEnlazada() {
            head = null;
            tail = null;
        }

        public void add_inicio(int data) {
            NodoDoble nuevo = new NodoDoble(data);
            if (head == null) {
                head = tail = nuevo;
            } else {
                nuevo.next = head;
                head.prev = nuevo;
                head = nuevo;
            }
        }

        public void add_final (int data) {
            NodoDoble nuevo = new NodoDoble(data);
            if (tail == null) {
                head = tail = nuevo;
            } else {
                tail.next = nuevo;
                nuevo.prev = tail;
                tail = nuevo;
            }
        }

        public void printLista() {
            NodoDoble actual = head;
            while (actual != null) {
                System.out.print(actual.data + " <-> ");
                actual = actual.next;
            }
            System.out.println("None");
        }

        public boolean search(int data) {
            NodoDoble actual = head;
            while (actual != null) {
                if (actual.data == data) return true;
                actual = actual.next;
            }
            return false;
        }

        public void eliminar(int data) {
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
    }

    public static void main(String[] args) {
        ListaDoblementeEnlazada lista = new ListaDoblementeEnlazada();
        lista.add_final(10);
        lista.add_final(20);
        lista.add_final(30);
        lista.printLista();

        lista.add_inicio(5);
        lista.printLista();

        System.out.println("¿Está el 20? " + lista.search(20));
        System.out.println("¿Está el 99? " + lista.search(99));

        lista.eliminar(20);
        lista.printLista();

        lista.eliminar(5);
        lista.printLista();

        lista.eliminar(30);
        lista.printLista();
    }
}