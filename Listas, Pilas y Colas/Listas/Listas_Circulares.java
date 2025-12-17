public class Listas_Circulares {

    static class Nodo {
        int data;
        Nodo next;

        public Nodo(int val) {
            data = val;
            next = null;
        }
    }

    public static class ListaCircular {
        public Nodo head;

        public ListaCircular() {
            head = null;
        }

        public void add_inicio(int data) {
            Nodo nuevo_nodo = new Nodo(data);
            if (head == null) {
                head = nuevo_nodo;
                nuevo_nodo.next = head;
            } else {
                Nodo tail = head;
                while (tail.next != head) {
                    tail = tail.next;
                }
                nuevo_nodo.next = head;
                head = nuevo_nodo;
                tail.next = head;
            }
        }

        public void add_final(int data) {
            Nodo nuevo_nodo = new Nodo(data);
            if (head == null) {
                head = nuevo_nodo;
                nuevo_nodo.next = head;
            } else {
                Nodo tail = head;
                while (tail.next != head) {
                    tail = tail.next;
                }
                tail.next = nuevo_nodo;
                nuevo_nodo.next = head;
            }
        }

        public void print_lista() {
            if (head == null) {
                System.out.println("null");
                return;
            }
            
            Nodo nodo_actual = head;
            do {
                System.out.print(nodo_actual.data + " -> ");
                nodo_actual = nodo_actual.next;
            } while (nodo_actual != head);
            
            System.out.println("(vuelve a " + head.data + ")");
        }

        public boolean search(int data_buscada) {
            if (head == null) {
                return false;
            }
            
            Nodo nodo_actual = head;
            do {
                if (nodo_actual.data == data_buscada) {
                    return true;
                }
                nodo_actual = nodo_actual.next;
            } while (nodo_actual != head);
            
            return false;
        }

        public void eliminar(int data_a_eliminar) {
            if (head == null) {
                return;
            }

            if (head.data == data_a_eliminar) {
                if (head.next == head) {
                    head = null;
                    return;
                }
                
                Nodo tail = head;
                while (tail.next != head) {
                    tail = tail.next;
                }
                
                head = head.next;
                tail.next = head;
                return;
            }

            Nodo nodo_previo = head;
            Nodo nodo_actual = head.next;
            while (nodo_actual != head) {
                if (nodo_actual.data == data_a_eliminar) {
                    nodo_previo.next = nodo_actual.next;
                    return;
                }
                nodo_previo = nodo_actual;
                nodo_actual = nodo_actual.next;
            }
        }
    }

    public static void main(String[] args) {
        ListaCircular mi_lista = new ListaCircular();
        mi_lista.add_final(10);
        mi_lista.add_final(20);
        mi_lista.add_final(30);
        mi_lista.print_lista();

        mi_lista.add_inicio(5);
        mi_lista.print_lista();

        System.out.println("¿Está el 20? " + mi_lista.search(20));
        System.out.println("¿Está el 99? " + mi_lista.search(99));

        mi_lista.eliminar(20);
        mi_lista.print_lista();
        
        mi_lista.eliminar(5);
        mi_lista.print_lista();
        
        mi_lista.eliminar(30);
        mi_lista.print_lista();
    }
}