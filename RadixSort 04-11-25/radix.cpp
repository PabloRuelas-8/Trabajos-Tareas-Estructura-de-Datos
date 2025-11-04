#include <iostream>

void imprimirArreglo(int arreglo[], int tamano) {
    for (int i = 0; i < tamano; i++) {
        std::cout << arreglo[i] << " ";
    }
    std::cout << std::endl;
}

int obtenerMaximo(int arreglo[], int tamano) {
    int maximo = arreglo[0]; 
    for (int i = 1; i < tamano; i++) {
        if (arreglo[i] > maximo) {
            maximo = arreglo[i];
        }
    }
    return maximo;
}

void ordenamientoPorConteo(int arreglo[], int tamano, int exponente) {
    int* arregloSalida = new int[tamano]; 
    int arregloConteo[10] = {0}; 

    for (int j = 0; j < tamano; j++) {
        int indice = (arreglo[j] / exponente);
        arregloConteo[indice % 10]++;
    }

    for (int j = 1; j < 10; j++) {
        arregloConteo[j] += arregloConteo[j - 1];
    }

    for (int j = tamano - 1; j >= 0; j--) {
        int indice = (arreglo[j] / exponente);
        arregloSalida[arregloConteo[indice % 10] - 1] = arreglo[j];
        arregloConteo[indice % 10]--;
    }

    for (int j = 0; j < tamano; j++) {
        arreglo[j] = arregloSalida[j];
    }
    
    delete[] arregloSalida;
}
void radixSort(int arreglo[], int tamano) {
    int maximo = obtenerMaximo(arreglo, tamano);

    for (int exponente = 1; maximo / exponente > 0; exponente *= 10) {
        ordenamientoPorConteo(arreglo, tamano, exponente);
    }
}

int main() {
    int arr[] = {171, 46, 76, 91, 803, 25, 3, 67};
    
    int tamano = sizeof(arr) / sizeof(arr[0]);
    
    std::cout << "Arreglo antes de ordenar: " << std::endl;
    imprimirArreglo(arr, tamano);

    radixSort(arr, tamano);

    std::cout << "Arreglo despues de ordenar: " << std::endl;
    imprimirArreglo(arr, tamano);

    return 0;
}