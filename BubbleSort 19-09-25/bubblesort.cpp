#include <iostream>
using namespace std;

int main() {
    int arreglo [8] = {44, 66, 88, 55, 33, 11, 22, 77};
    int n = 8;

    cout << "Arreglo original:\n";
    for (int i = 0; i < n; i++) cout << arreglo[i] << " ";
    cout << endl;

    for (int i = 0; i < n - 1; i++) {
        for (int j = 0; j < n - i - 1; j++) {
            if (arreglo[j] > arreglo[j + 1]) {
                int aux = arreglo[j];
                arreglo[j] = arreglo[j + 1];
                arreglo[j + 1] = aux;
            }
        }
    }

    cout << "Arreglo ordenado de menor a mayor:\n";
    for (int i = 0; i < n; i++) cout << arreglo[i] << " ";
    cout << endl;

    return 0;
}
