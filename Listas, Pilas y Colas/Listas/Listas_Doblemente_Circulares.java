public class Listas_Doblemente_Circulares {

    static class NodoDoble {
        int data;
        NodoDoble next;
        NodoDoble prev;

        public NodoDoble(int d) {
            data = d;
            next = null;
            prev = null;
        }
    }

    public static class ListaDobleCircularImpl {
        private NodoDoble head;

        public ListaDobleCircularImpl() {
            head = null;
        }

        public boolean isEmpty() {
            return head == null;
        }

        public void add_inicio(int data) {
            NodoDoble nuevo = new NodoDoble(data);

            if (isEmpty()) {
                head = nuevo;
                head.next = head;
                head.prev = head;
            } else {
                NodoDoble tail = head.prev;
                nuevo.next = head;
                head.prev = nuevo;
                head = nuevo;
                head.prev = tail;
                tail.next = head;
            }
        }

        public void add_final(int data) {
            NodoDoble nuevo = new NodoDoble(data);

            if (isEmpty()) {
                head = nuevo;
                head.next = head;
                head.prev = head;
            } else {
                NodoDoble tail = head.prev;
                tail.next = nuevo;
                nuevo.prev = tail;
                nuevo.next = head;
                head.prev = nuevo;
            }
        }

        public void print_Lista() {
            if (isEmpty()) {
                System.out.println("None");
                return;
            }

            NodoDoble actual = head;
            do {
                System.out.print(actual.data + " <-> ");
                actual = actual.next;
            } while (actual != head);

            System.out.println("(vuelve a " + head.data + ")");
        }

        public boolean search(int data) {
            if (isEmpty()) return false;

            NodoDoble actual = head;
            do {
                if (actual.data == data) return true;
                actual = actual.next;
            } while (actual != head);

            return false;
        }

        public void eliminar(int data) {
            if (isEmpty()) return;

            NodoDoble actual = head;

            do {
                if (actual.data == data) break;
                actual = actual.next;
            } while (actual != head);

            if (actual.data != data) return;

            if (actual == head && head.next == head) {
                head = null;
                return;
            }

            if (actual == head)
                head = actual.next;

            actual.prev.next = actual.next;
            actual.next.prev = actual.prev;
        }
    }

    public static void main(String[] args) {
        ListaDobleCircularImpl lista = new ListaDobleCircularImpl();
        lista.add_final(10);
        lista.add_final(20);
        lista.add_final(30);
        lista.print_Lista();

        lista.add_inicio(5);
        lista.print_Lista();

        System.out.println("¿Está el 20? " + lista.search(20));
        System.out.println("¿Está el 99? " + lista.search(99));

        lista.eliminar(20);
        lista.print_Lista();

        lista.eliminar(5);
        lista.print_Lista();

        lista.eliminar(30);
        lista.print_Lista();
    }
}