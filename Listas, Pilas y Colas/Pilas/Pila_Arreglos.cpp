
#include <iostream>
using namespace std;

class PilaArreglo {
private:
    int* stack;
    int max_size;
    int top;

public:
    PilaArreglo(int tam) {
        max_size = tam;
        stack = new int[max_size];
        top = -1;
    }

    bool isEmpty() {
        return top == -1;
    }

    bool isFull() {
        return top == max_size - 1;
    }

    void push(int data) {
        if (isFull()) {
            cout << "Error: Overflow de Pila\n";
            return;
        }
        stack[++top] = data;
    }

    int pop() {
        if (isEmpty()) {
            cout << "Error: Underflow de Pila\n";
            return -1;
        }
        return stack[top--];
    }

    int peek() {
        if (isEmpty()) {
            cout << "La pila está vacía\n";
            return -1;
        }
        return stack[top];
    }
};

int main() {
    PilaArreglo pila(100);
    pila.push(10);
    pila.push(20);
    pila.push(30);

    cout << "Elemento superior(TOP): " << pila.peek() << endl;
    cout << "Extrae elemento: " << pila.pop() << endl;
    cout << "Nuevo elemento superior: " << pila.peek() << endl;

    return 0;
}
