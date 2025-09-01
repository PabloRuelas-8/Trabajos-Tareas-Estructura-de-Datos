#include <iostream>
#include <vector>
using namespace std;

int main() {
    int numeros[4] = {10, 20, 30, 40};

    cout << "Primer elemento: " << numeros[0] << endl;

    numeros[2] = 45;

    cout << "arreglo: ";
    for(int i = 0; i < 4; i++) {
        cout << numeros[i] << " ";
    }
    cout << endl;

    int n;
    cout << "cuantos numeros?: ";
    cin >> n;

    vector<int> arr(n); 
    for(int i = 0; i < n; i++) {
        cout << "ingrese valor " << (i+1) << ": ";
        cin >> arr[i];
    }

    cout << "valores ingresados: ";
    for(int i = 0; i < n; i++) {
        cout << arr[i] << " ";
    }
    cout << endl;

    return 0;
}
