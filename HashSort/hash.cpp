#include <iostream>

class OrdenamientoShell {
public:
    static void mostrarArreglo(int arreglo[], int tamano) {
        for (int k = 0; k < tamano; k++) {
            std::cout << arreglo[k] << " ";
        }
        std::cout << std::endl;
    }

    void ordenar(int arreglo[], int tamano) {
        
        int tamanoBrecha = tamano / 2; 

        while (tamanoBrecha > 0) {
            for (int j = tamanoBrecha; j < tamano; j++) {
                int valor = arreglo[j];
                int k = j;

                while (k >= tamanoBrecha && arreglo[k - tamanoBrecha] > valor) {
                    arreglo[k] = arreglo[k - tamanoBrecha];
                    k = k - tamanoBrecha;
                }
                arreglo[k] = valor;
            }
            tamanoBrecha = tamanoBrecha / 2; 
        }
    }
};

int main() {
    int arreglo[] = {36, 34, 43, 11, 15, 20, 28, 45};
    int tamano = sizeof(arreglo) / sizeof(arreglo[0]);

    std::cout << "Arreglo antes de ser ordenado: " << std::endl;
    OrdenamientoShell::mostrarArreglo(arreglo, tamano);

    OrdenamientoShell obj;
    obj.ordenar(arreglo, tamano);

    std::cout << "Arreglo despues de ser ordenado: " << std::endl;
    OrdenamientoShell::mostrarArreglo(arreglo, tamano);

    return 0;
}