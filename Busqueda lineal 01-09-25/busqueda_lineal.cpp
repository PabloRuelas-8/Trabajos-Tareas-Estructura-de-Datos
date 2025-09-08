#include <iostream>
using namespace std;

void imprimir(int arre[], int n) {
    cout << "El arreglo es: ";
    for (int i = 0; i < n; i++) {
        cout << arre[i] << " ";
    }
    cout << endl;
}

void insertar_elemento(int arre[], int n, int val, int indice) {
    for (int i = n - 1; i > indice; i--) {
        arre[i] = arre[i - 1];
    }
    arre[indice] = val;
}

int busqueda_lineal(int arre[], int n, int obj) {
    for (int i = 0; i < n; i++) {
        if (arre[i] == obj) {
            return i;
        }
    }
    return -1;
}

int main() {
    const int n = 5;
    int arre[n] = {1, 2, 3, 4, 5};
    imprimir(arre, n);

    int val, indice;
    cout << "Valor para insertar: ";
    cin >> val;
    cout << "Indice donde se va a insertar (0.." << n-1 << "): ";
    cin >> indice;

    insertar_elemento(arre, n, val, indice);
    imprimir(arre, n);

    int obj = val;
    int pos = busqueda_lineal(arre, n, obj);
    cout << "busqueda lineal de " << obj << ": indice " << pos << endl;

    cout << "\nPresiona ENTER para salir";
    cin.ignore();
    cin.get();
    return 0;
}
