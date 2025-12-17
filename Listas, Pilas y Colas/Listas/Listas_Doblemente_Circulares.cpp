#include <iostream>
using namespace std;

class NodoDoble {
public:
    int data;
    NodoDoble* next;
    NodoDoble* prev;

    NodoDoble(int d) : data(d), next(nullptr), prev(nullptr) {}
};

class ListaDoblementeCircular {
private:
    NodoDoble* head;

public:
    ListaDoblementeCircular() : head(nullptr) {}

    void add_inicio(int data) {
        NodoDoble* nuevo = new NodoDoble(data);

        if (!head) {
            head = nuevo;
            head->next = head;
            head->prev = head;
        } else {
            NodoDoble* tail = head->prev;
            nuevo->next = head;
            head->prev = nuevo;
            head = nuevo;
            head->prev = tail;
            tail->next = head;
        }
    }

    void add_final(int data) {
        NodoDoble* nuevo = new NodoDoble(data);

        if (!head) {
            head = nuevo;
            head->next = head;
            head->prev = head;
        } else {
            NodoDoble* tail = head->prev;
            tail->next = nuevo;
            nuevo->prev = tail;
            nuevo->next = head;
            head->prev = nuevo;
        }
    }

    void print_Lista() {
        if (!head) {
            cout << "None\n";
            return;
        }

        NodoDoble* actual = head;
        do {
            cout << actual->data << " <-> ";
            actual = actual->next;
        } while (actual != head);

        cout << "(vuelve a " << head->data << ")\n";
    }

    bool search(int data) {
        if (!head) return false;

        NodoDoble* actual = head;
        do {
            if (actual->data == data) return true;
            actual = actual->next;
        } while (actual != head);

        return false;
    }

    void eliminar(int data) {
        if (!head) return;

        NodoDoble* actual = head;

        do {
            if (actual->data == data) break;
            actual = actual->next;
        } while (actual != head);

        if (actual->data != data) return;  

        if (actual == head && head->next == head) {
            delete head;
            head = nullptr;
            return;
        }

        if (actual == head)
            head = actual->next;

        actual->prev->next = actual->next;
        actual->next->prev = actual->prev;

        delete actual;
    }
};

int main() {
    ListaDoblementeCircular lista;

    lista.add_final(10);
    lista.add_final(20);
    lista.add_final(30);
    lista.print_Lista();

    lista.add_inicio(5);
    lista.print_Lista();

    cout << "¿Está el 20? " << lista.search(20) << endl;
    cout << "¿Está el 99? " << lista.search(99) << endl;

    lista.eliminar(20);
    lista.print_Lista();

    lista.eliminar(5);
    lista.print_Lista();

    lista.eliminar(30);
    lista.print_Lista();
}