#include <iostream>

void merge(int a[], int l, int m, int r) {
    int a1 = m - l + 1;
    int a2 = r - m;

    int* L = new int[a1];
    int* R = new int[a2];

    for (int j = 0; j < a1; j++) {
        L[j] = a[l + j];
    }
    for (int k = 0; k < a2; k++) {
        R[k] = a[m + 1 + k];
    }

    int i = 0; 
    int j = 0;
    int k = l;

    while (i < a1 && j < a2) {
        if (L[i] <= R[j]) {
            a[k] = L[i];
            i++;
        } else {
            a[k] = R[j];
            j++;
        }
        k++;
    }

    while (i < a1) {
        a[k] = L[i];
        i++;
        k++;
    }

    while (j < a2) {
        a[k] = R[j];
        j++;
        k++;
    }

    delete[] L;
    delete[] R;
}

void mergeSort(int a[], int l, int r) {
    if (l < r) {
        int m = (l + (r - 1)) / 2;

        mergeSort(a, l, m);
        mergeSort(a, m + 1, r);

        merge(a, l, m, r);
    }
}

void imprimirArreglo(int a[], int tamano) {
    for (int i = 0; i < tamano; i++) {
        std::cout << a[i] << " ";
    }
    std::cout << std::endl;
}

int main() {
    int a[] = {39, 28, 44, 11};
    int s = sizeof(a) / sizeof(a[0]);

    std::cout << "Antes de ordenar el arreglo: " << std::endl;
    imprimirArreglo(a, s);
    
    mergeSort(a, 0, s - 1);
    
    std::cout << "Despues de ordenar el arreglo: " << std::endl;
    imprimirArreglo(a, s);

    return 0;
}