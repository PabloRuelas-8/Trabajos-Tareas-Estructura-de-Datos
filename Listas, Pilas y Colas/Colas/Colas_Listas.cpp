#include <iostream>
#include <cstdlib>

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

class ColaLista {
private:
    Nodo* front;
    Nodo* rear;

public:
    ColaLista() {
        this->front = nullptr;
        this->rear = nullptr;
    }

    bool isEmpty() {
        return front == nullptr;
    }

    void enqueue(int data) {
        Nodo* nuevoNodo = new Nodo(data);

        if (rear == nullptr) {
            front = nuevoNodo;
            rear = nuevoNodo;
            return;
        }

        rear->next = nuevoNodo;
        rear = nuevoNodo;
    }

    int dequeue() {
        if (isEmpty()) {
            cout << "Error: Cola subdesbordada (Underflow)" << endl;
            return -1;
        }

        int data = front->data;
        Nodo* temp = front;
        front = front->next;
        delete temp;

        if (front == nullptr) {
            rear = nullptr;
        }

        return data;
    }

    int peek() {
        if (isEmpty()) {
            cout << "Cola vacía" << endl;
            return -1;
        }
        return front->data;
    }
};

int main() {
    ColaLista cola;

    cola.enqueue(10);
    cola.enqueue(20);
    cola.enqueue(30);

    cout << "Elemento frontal: " << cola.peek() << endl;
    cout << "Elimina elemento: " << cola.dequeue() << endl;
    cout << "Nuevo elemento frontal: " << cola.peek() << endl;

    return 0;
}