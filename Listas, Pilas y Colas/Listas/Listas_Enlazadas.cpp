#include <iostream>
using namespace std;

class Nodo {
public:
    int data;
    Nodo* next;

    Nodo(int data) {
        this->data = data;
        this->next = nullptr;
    }
};

class ListaEnlazada {
private:
    Nodo* head;

public:
    ListaEnlazada() {
        head = nullptr;
    }

    void insertar_al_principio(int data) {
        Nodo* nuevo = new Nodo(data);
        nuevo->next = head;
        head = nuevo;
    }

    void insertar_al_final(int data) {
        Nodo* nuevo = new Nodo(data);
        if (!head) {
            head = nuevo;
            return;
        }
        Nodo* ultimo = head;
        while (ultimo->next) {
            ultimo = ultimo->next;
        }
        ultimo->next = nuevo;
    }

    void imprimir_lista() {
        Nodo* actual = head;
        while (actual) {
            cout << actual->data << " -> ";
            actual = actual->next;
        }
        cout << "None\n";
    }

    bool buscar(int data) {
        Nodo* actual = head;
        while (actual) {
            if (actual->data == data) return true;
            actual = actual->next;
        }
        return false;
    }

    void eliminar(int data) {
        Nodo* actual = head;
        Nodo* previo = nullptr;

        if (actual && actual->data == data) {
            head = actual->next;
            delete actual;
            return;
        }

        while (actual && actual->data != data) {
            previo = actual;
            actual = actual->next;
        }

        if (!actual) return;

        previo->next = actual->next;
        delete actual;
    }
};

int main() {
    ListaEnlazada lista;
    lista.insertar_al_final(10);
    lista.insertar_al_final(20);
    lista.insertar_al_final(30);
    lista.imprimir_lista();

    lista.insertar_al_principio(5);
    lista.imprimir_lista();

    cout << "¿Esta el 20? " << lista.buscar(20) << endl;
    cout << "¿Esta el 99? " << lista.buscar(99) << endl;

    lista.eliminar(20);
    lista.imprimir_lista();

    lista.eliminar(5);
    lista.imprimir_lista();

    return 0;
}