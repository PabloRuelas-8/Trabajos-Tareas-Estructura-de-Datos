#include <iostream>

using namespace std;

class Nodo {
public:
    int data;
    Nodo* next;

    Nodo(int val) : data(val), next(nullptr) {}
};

class ListaCircularEnlazada {
public:
    Nodo* head;

    ListaCircularEnlazada() : head(nullptr) {}

    ~ListaCircularEnlazada() {
        if (head == nullptr) return;
        Nodo* actual = head->next;
        while (actual != head) {
            Nodo* temp = actual;
            actual = actual->next;
            delete temp;
        }
        delete head;
    }

    void add_inicio(int data) {
        Nodo* nuevo_nodo = new Nodo(data);
        if (head == nullptr) {
            head = nuevo_nodo;
            nuevo_nodo->next = head;
        } else {
            Nodo* tail = head;
            while (tail->next != head) {
                tail = tail->next;
            }
            nuevo_nodo->next = head;
            head = nuevo_nodo;
            tail->next = head;
        }
    }

    void add_final(int data) {
        Nodo* nuevo_nodo = new Nodo(data);
        if (head == nullptr) {
            head = nuevo_nodo;
            nuevo_nodo->next = head;
        } else {
            Nodo* tail = head;
            while (tail->next != head) {
                tail = tail->next;
            }
            tail->next = nuevo_nodo;
            nuevo_nodo->next = head;
        }
    }

    void print_lista() {
        if (head == nullptr) {
            cout << "null" << endl;
            return;
        }
        
        Nodo* nodo_actual = head;
        do {
            cout << nodo_actual->data << " -> ";
            nodo_actual = nodo_actual->next;
        } while (nodo_actual != head);
        
        cout << "(vuelve a " << head->data << ")" << endl;
    }

    bool search(int data_buscada) {
        if (head == nullptr) {
            return false;
        }
        
        Nodo* nodo_actual = head;
        do {
            if (nodo_actual->data == data_buscada) {
                return true;
            }
            nodo_actual = nodo_actual->next;
        } while (nodo_actual != head);
        
        return false;
    }

    void eliminar(int data_a_eliminar) {
        if (head == nullptr) {
            return;
        }

        if (head->data == data_a_eliminar) {
            if (head->next == head) {
                delete head;
                head = nullptr;
                return;
            }
            
            Nodo* tail = head;
            while (tail->next != head) {
                tail = tail->next;
            }
            
            Nodo* temp = head;
            head = head->next;
            tail->next = head;
            delete temp;
            return;
        }

        Nodo* nodo_previo = head;
        Nodo* nodo_actual = head->next;
        while (nodo_actual != head) {
            if (nodo_actual->data == data_a_eliminar) {
                nodo_previo->next = nodo_actual->next;
                delete nodo_actual;
                return;
            }
            nodo_previo = nodo_actual;
            nodo_actual = nodo_actual->next;
        }
    }
};

int main() {
    ListaCircularEnlazada mi_lista;
    mi_lista.add_final(10);
    mi_lista.add_final(20);
    mi_lista.add_final(30);
    mi_lista.print_lista();

    mi_lista.add_inicio(5);
    mi_lista.print_lista();

    cout << "¿Está el 20? " << (mi_lista.search(20) ? "True" : "False") << endl;
    cout << "¿Está el 99? " << (mi_lista.search(99) ? "True" : "False") << endl;

    mi_lista.eliminar(20);
    mi_lista.print_lista();
    
    mi_lista.eliminar(5);
    mi_lista.print_lista();
    
    mi_lista.eliminar(30);
    mi_lista.print_lista();

    return 0;
}