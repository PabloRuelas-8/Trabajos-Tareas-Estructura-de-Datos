#include <iostream>
#include <iomanip>
#include <string>
#include <cctype>
using namespace std;

const int size_casillas = 10;

void inicializar(char tablero[size_casillas][size_casillas]) {
    for (int i = 0; i < size_casillas; i++)
        for (int j = 0; j < size_casillas; j++)
            tablero[i][j] = '.';
}

void imprimir(char tablero[size_casillas][size_casillas], bool ocultar) {
    cout << "   ";
    for (int j = 0; j < size_casillas; j++) cout << char('A' + j) << " ";
    cout << "\n";
    for (int i = 0; i < size_casillas; i++) {
        cout << setw(2) << i + 1 << " ";
        for (int j = 0; j < size_casillas; j++) {
            char c = tablero[i][j];
            if (ocultar && c != '.' && c != 'X' && c != '-') cout << ". ";
            else cout << c << " ";
        }
        cout << "\n";
    }
}

bool convertir(string s, int &fila, int &col) { 
    if (s.size() < 2) return false;
    col = toupper(s[0]) - 'A';
    fila = stoi(s.substr(1)) - 1;
    if (fila < 0 || fila >= size_casillas || col < 0 || col >= size_casillas) return false;
    return true;
}

void colocarBarco(char tablero[size_casillas][size_casillas], int tamBarco, char simbolo) {
    bool puesto = false;
    while (!puesto) {
        imprimir(tablero, false);
        cout << "Coloca barco de " << tamBarco << " casillas " << "(escribir asi, A1 H): ";
        string coord; char ori;
        cin >> coord >> ori;
        int fila, col;
        if (!convertir(coord, fila, col)) { cout << "Coordenada invalida\n"; continue; }
        ori = toupper(ori);
        bool valido = true;
        if (ori == 'H') {
            if (col + tamBarco > size_casillas) { valido = false; }
            else {
                for (int j = 0; j < tamBarco; j++)
                    if (tablero[fila][col + j] != '.') valido = false;
                if (valido) for (int j = 0; j < tamBarco; j++) tablero[fila][col + j] = simbolo;
            }
        } else if (ori == 'V') {
            if (fila + tamBarco > size_casillas) { valido = false; }
            else {
                for (int i = 0; i < tamBarco; i++)
                    if (tablero[fila + i][col] != '.') valido = false;
                if (valido) for (int i = 0; i < tamBarco; i++) tablero[fila + i][col] = simbolo;
            }
        } else valido = false;

        if (!valido) cout << "No se puede colocar ahi\n";
        else puesto = true;
    }
}


int main() {
    char t1[size_casillas][size_casillas], t2[size_casillas][size_casillas];
    inicializar(t1); inicializar(t2);

    cout << "Jugador 1 coloca sus barcos\n";
    colocarBarco(t1, 5, 'P'); // portaaviones
    colocarBarco(t1, 4, 'A'); // acorazado
    colocarBarco(t1, 3, 'D'); // destructor
    colocarBarco(t1, 3, 'S'); // submarino
    colocarBarco(t1, 2, 'T'); // patrullero

    system("cls"); 
    cout << "Jugador 2 coloca sus barcos\n";
    colocarBarco(t2, 5, 'P');
    colocarBarco(t2, 4, 'A');
    colocarBarco(t2, 3, 'D');
    colocarBarco(t2, 3, 'S');
    colocarBarco(t2, 2, 'T');

}
