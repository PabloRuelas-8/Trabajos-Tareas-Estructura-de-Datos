#include <iostream>
#include <list>
#include <vector>
#include <string>
#include <conio.h>
#include <windows.h>
#include <fstream>
#include <algorithm>
#include <ctime>

using namespace std;

#define COLOR_VERDE_BRILLANTE 10
#define COLOR_ROJO_BRILLANTE 12
#define COLOR_BLANCO 15
#define COLOR_GRIS 8
#define COLOR_AMARILLO 14
#define COLOR_AZUL_CLARO 11

struct Coordenada {
    int x, y;
    bool operator==(const Coordenada& otra) const { return x == otra.x && y == otra.y; }
};
struct RegistroJugador {
    string nombre; int puntaje;
    bool operator>(const RegistroJugador& otro) const { return puntaje > otro.puntaje; }
};

bool juegoEnCurso;
const int ANCHO_TABLERO = 40;
const int ALTO_TABLERO = 20;
int puntajeActual;
int frutasComidas = 0;

enum Direccion { DETENIDO = 0, IZQUIERDA, DERECHA, ARRIBA, ABAJO };
Direccion direccionActual;

list<Coordenada> cuerpoSerpiente;
list<Coordenada> obstaculos;
Coordenada posicionComida;
HANDLE hConsola = GetStdHandle(STD_OUTPUT_HANDLE);

void MoverCursor(int x, int y) {
    COORD coord = { (SHORT)x, (SHORT)y }; SetConsoleCursorPosition(hConsola, coord);
}
void CambiarColor(int colorID) { SetConsoleTextAttribute(hConsola, colorID); }
void OcultarCursorConsola() {
    CONSOLE_CURSOR_INFO cursorInfo; GetConsoleCursorInfo(hConsola, &cursorInfo);
    cursorInfo.bVisible = false; SetConsoleCursorInfo(hConsola, &cursorInfo);
}

bool EstaOcupado(int x, int y) {
    Coordenada pos = { x, y };
   
    for (auto const& segmento : cuerpoSerpiente) if (segmento == pos) return true;
    for (auto const& obs : obstaculos) if (obs == pos) return true;
    return false;
}

void GenerarObstaculo() {
    Coordenada nuevoObs;
    do {
        nuevoObs.x = rand() % ANCHO_TABLERO;
        nuevoObs.y = rand() % ALTO_TABLERO;
    } while (EstaOcupado(nuevoObs.x, nuevoObs.y) || (nuevoObs == posicionComida));
    obstaculos.push_back(nuevoObs);
}

void InicializarJuego() {
    juegoEnCurso = true; direccionActual = DETENIDO;
    puntajeActual = 0; frutasComidas = 0;
    cuerpoSerpiente.clear(); obstaculos.clear();
    cuerpoSerpiente.push_front({ ANCHO_TABLERO / 2, ALTO_TABLERO / 2 });
    srand(time(0));
    posicionComida.x = rand() % (ANCHO_TABLERO - 2) + 1;
    posicionComida.y = rand() % (ALTO_TABLERO - 2) + 1;
    OcultarCursorConsola();
}

void RenderizarJuego() {
    MoverCursor(0, 0); CambiarColor(COLOR_GRIS);
    for (int i = 0; i < ANCHO_TABLERO + 2; i++) cout << (char)219; cout << endl;

    for (int i = 0; i < ALTO_TABLERO; i++) {
        for (int j = 0; j < ANCHO_TABLERO; j++) {
            if (j == 0) { CambiarColor(COLOR_GRIS); cout << (char)219; }

            Coordenada posActual = { j, i };
            bool dibujado = false;

            if (posActual == cuerpoSerpiente.front()) {
                CambiarColor(COLOR_VERDE_BRILLANTE); cout << "O"; dibujado = true;
            }
            else if (posActual == posicionComida) {
                CambiarColor(COLOR_ROJO_BRILLANTE); cout << "@"; dibujado = true;
            }
            else {
                for (auto const& segmento : cuerpoSerpiente) {
                    if (segmento == posActual) {
                        CambiarColor(COLOR_VERDE_BRILLANTE); cout << "o"; dibujado = true; break;
                    }
                }
                if (!dibujado) {
                    for (auto const& obs : obstaculos) {
                        if (obs == posActual) {
                            CambiarColor(COLOR_AZUL_CLARO); cout << (char)178;
                            dibujado = true; break;
                        }
                    }
                }
            }
            if (!dibujado) cout << " ";
            if (j == ANCHO_TABLERO - 1) { CambiarColor(COLOR_GRIS); cout << (char)219; }
        }
        cout << endl;
    }
    CambiarColor(COLOR_GRIS);
    for (int i = 0; i < ANCHO_TABLERO + 2; i++) cout << (char)219; cout << endl;
    CambiarColor(COLOR_BLANCO);
    cout << "Puntaje: " << puntajeActual << " | Obstaculos: " << obstaculos.size() << endl;
}

void LeerControles() {
    if (_kbhit()) {
        switch (_getch()) {
        case 'a': if (direccionActual != DERECHA) direccionActual = IZQUIERDA; break;
        case 'd': if (direccionActual != IZQUIERDA) direccionActual = DERECHA; break;
        case 'w': if (direccionActual != ABAJO) direccionActual = ARRIBA; break;
        case 's': if (direccionActual != ARRIBA) direccionActual = ABAJO; break;
        case 'x': juegoEnCurso = false; break;
        }
    }
}

void ActualizarLogica() {
    if (direccionActual == DETENIDO) return;
    Coordenada nuevaCabeza = cuerpoSerpiente.front();
    switch (direccionActual) {
    case IZQUIERDA: nuevaCabeza.x--; break; case DERECHA: nuevaCabeza.x++; break;
    case ARRIBA: nuevaCabeza.y--; break; case ABAJO: nuevaCabeza.y++; break;
    }

    if (nuevaCabeza.x >= ANCHO_TABLERO || nuevaCabeza.x < 0 || nuevaCabeza.y >= ALTO_TABLERO || nuevaCabeza.y < 0) {
        juegoEnCurso = false; return;
    }
    for (auto const& segmento : cuerpoSerpiente) {
        if (segmento == nuevaCabeza) { juegoEnCurso = false; return; }
    }
    for (auto const& obs : obstaculos) {
        if (obs == nuevaCabeza) { juegoEnCurso = false; return; }
    }

    cuerpoSerpiente.push_front(nuevaCabeza);

    if (nuevaCabeza == posicionComida) {
        puntajeActual += 10;
        frutasComidas++;

        if (frutasComidas % 2 == 0) {
            GenerarObstaculo();
        }

        Coordenada nuevaPosComida;
        do {
            nuevaPosComida.x = rand() % ANCHO_TABLERO;
            nuevaPosComida.y = rand() % ALTO_TABLERO;
        } while (EstaOcupado(nuevaPosComida.x, nuevaPosComida.y));
        posicionComida = nuevaPosComida;
    }
    else {
        cuerpoSerpiente.pop_back();
    }
}

void GuardarYMostrarRanking(int puntajeFinal) {
    CambiarColor(COLOR_BLANCO); system("cls");
    string nombreArchivo = "ranking_snake.txt";
    vector<RegistroJugador> listaRanking;
    ifstream archivoLectura(nombreArchivo);
    if (archivoLectura.is_open()) {
        RegistroJugador reg;
        while (archivoLectura >> reg.nombre >> reg.puntaje) listaRanking.push_back(reg);
        archivoLectura.close();
    }
    if (puntajeFinal > 0) {
        CambiarColor(COLOR_ROJO_BRILLANTE); cout << "\n--- GAME OVER ---\n";
        CambiarColor(COLOR_BLANCO); cout << "Puntaje final: " << puntajeFinal << endl;
        cout << "Ingresa tu alias: "; string alias; cin >> alias;
        listaRanking.push_back({ alias, puntajeFinal });
    }
    sort(listaRanking.begin(), listaRanking.end(), [](const RegistroJugador& a, const RegistroJugador& b) { return a.puntaje > b.puntaje; });
    if (listaRanking.size() > 5) listaRanking.resize(5);
    ofstream archivoEscritura(nombreArchivo);
    CambiarColor(COLOR_AMARILLO); cout << "\n=== TOP 5 ===\n"; CambiarColor(COLOR_BLANCO);
    for (const auto& reg : listaRanking) {
        archivoEscritura << reg.nombre << " " << reg.puntaje << endl;
        cout << reg.nombre << "\t\t" << reg.puntaje << endl;
    }
}

int main() {
    InicializarJuego();
    while (juegoEnCurso) {
        RenderizarJuego(); LeerControles(); ActualizarLogica(); Sleep(100);
    }
    GuardarYMostrarRanking(puntajeActual);
    cout << "\nPresiona cualquier tecla..."; _getch(); return 0;
}