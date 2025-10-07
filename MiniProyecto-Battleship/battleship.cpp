#include <iostream>
#include <iomanip>
#include <string>
#include <cctype>
using namespace std;

const int TAM = 10;

void inicializar(char tablero[TAM][TAM]) {
    for (int i = 0; i < TAM; i++)
        for (int j = 0; j < TAM; j++)
            tablero[i][j] = '.';
}

void imprimir(char tablero[TAM][TAM], bool ocultar) {
    cout << "   ";
    for (int j = 0; j < TAM; j++) cout << char('A' + j) << " ";
    cout << "\n";
    for (int i = 0; i < TAM; i++) {
        cout << setw(2) << i + 1 << " ";
        for (int j = 0; j < TAM; j++) {
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
    if (fila < 0 || fila >= TAM || col < 0 || col >= TAM) return false;
    return true;
}

void colocarBarco(char tablero[TAM][TAM], int tamBarco, char simbolo) {
    bool puesto = false;
    while (!puesto) {
        imprimir(tablero, false);
        cout << "Coloca barco de " << tamBarco << " casillas (ej A1 H): ";
        string coord; char ori;
        cin >> coord >> ori;
        int fila, col;
        if (!convertir(coord, fila, col)) { cout << "Coordenada invalida\n"; continue; }
        ori = toupper(ori);
        bool valido = true;
        if (ori == 'H') {
            if (col + tamBarco > TAM) { valido = false; }
            else {
                for (int j = 0; j < tamBarco; j++)
                    if (tablero[fila][col + j] != '.') valido = false;
                if (valido) for (int j = 0; j < tamBarco; j++) tablero[fila][col + j] = simbolo;
            }
        } else if (ori == 'V') {
            if (fila + tamBarco > TAM) { valido = false; }
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

bool disparar(char tablero[TAM][TAM]) {
    cout << "Dispara (ej B7): ";
    string coord; cin >> coord;
    int fila, col;
    if (!convertir(coord, fila, col)) { cout << "Coordenada invalida\n"; return false; }
    if (tablero[fila][col] == '.') { tablero[fila][col] = '-'; cout << "Agua!\n"; }
    else if (tablero[fila][col] == '-' || tablero[fila][col] == 'X') { cout << "Ya disparaste ahi\n"; }
    else { tablero[fila][col] = 'X'; cout << "Tocado!\n"; }
    return true;
}

bool quedanBarcos(char tablero[TAM][TAM]) {
    for (int i = 0; i < TAM; i++)
        for (int j = 0; j < TAM; j++)
            if (tablero[i][j] != '.' && tablero[i][j] != '-' && tablero[i][j] != 'X')
                return true;
    return false;
}

int main() {
    char t1[TAM][TAM], t2[TAM][TAM];
    inicializar(t1); inicializar(t2);

    // Colocacion de barcos
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

    int turno = 1;
    while (quedanBarcos(t1) && quedanBarcos(t2)) {
        cout << "\n=== Turno Jugador " << turno << " ===\n";
        if (turno == 1) { imprimir(t2, true); disparar(t2); turno = 2; }
        else { imprimir(t1, true); disparar(t1); turno = 1; }
    }
    if (quedanBarcos(t1)){
        cout << "Jugador 1 gana!\n";
        system("pause");
    }
    else{
        cout << "Jugador 2 gana!\n";
        system("pause");
    } 
}
