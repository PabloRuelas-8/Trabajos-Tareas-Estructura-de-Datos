#include <iostream>
using namespace std;

class NodoDoble {
public:
    int data;
    NodoDoble* next;
    NodoDoble* prev;

    NodoDoble(int data) {
        this->data = data;
        next = nullptr;
        prev = nullptr;
    }
};

class ListaDoblementeEnlazada {
private:
    NodoDoble* head;
    NodoDoble* tail;

public:
    ListaDoblementeEnlazada() {
        head = nullptr;
        tail = nullptr;
    }

    void add_inicio(int data) {
        NodoDoble* nuevo = new NodoDoble(data);
        if (!head) {
            head = tail = nuevo;
        } else {
            nuevo->next = head;
            head->prev = nuevo;
            head = nuevo;
        }
    }

    void add_final (int data) {
        NodoDoble* nuevo = new NodoDoble(data);
        if (!tail) {
            head = tail = nuevo;
        } else {
            tail->next = nuevo;
            nuevo->prev = tail;
            tail = nuevo;
        }
    }

    void printLista() {
        NodoDoble* actual = head;
        while (actual) {
            cout << actual->data << " <-> ";
            actual = actual->next;
        }
        cout << "None\n";
    }

    bool search(int data) {
        NodoDoble* actual = head;
        while (actual) {
            if (actual->data == data) return true;
            actual = actual->next;
        }
        return false;
    }

    void eliminar(int data) {
        NodoDoble* actual = head;

        while (actual && actual->data != data)
            actual = actual->next;

        if (!actual) return;

        if (actual->prev)
            actual->prev->next = actual->next;
        else
            head = actual->next;

        if (actual->next)
            actual->next->prev = actual->prev;
        else
            tail = actual->prev;

        delete actual;
    }
};

int main() {
    ListaDoblementeEnlazada lista;
    lista.add_final(10);
    lista.add_final(20);
    lista.add_final(30);
    lista.printLista();

    lista.add_inicio(5);
    lista.printLista();

    cout << "¿Está el 20? " << lista.search(20) << endl;
    cout << "¿Está el 99? " << lista.search(99) << endl;

    lista.eliminar(20);
    lista.printLista();

    lista.eliminar(5);
    lista.printLista();

    lista.eliminar(30);
    lista.printLista();
}