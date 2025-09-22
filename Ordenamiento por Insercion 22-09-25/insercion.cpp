#include <iostream>
using namespace std;

void insertionSort(int a[], int n) {
    for (int i = 1; i < n; i++) {
        int temp = a[i];
        int j = i - 1;
        while (j >= 0 && temp < a[j]) {
            a[j + 1] = a[j];
            j--;
        }
        a[j + 1] = temp;
    }
}

void printArr(int a[], int n) {
    for (int i = 0; i < n; i++) {
        cout << a[i] << " ";
    }
    cout << endl;
}

int main() {
    int a[] = {33, 55, 77, 22, 88, 11, 99, 44, 66};
    int n = 9;

    cout << "Antes de ordenar los elementos del arreglo:\n";
    printArr(a, n);

    insertionSort(a, n);

    cout << "Despues de ordenar los elementos del arreglo:\n";
    printArr(a, n);
    cout << "\nPresiona ENTER para salir...";
    cin.ignore();
    cin.get();
    return 0;
}
