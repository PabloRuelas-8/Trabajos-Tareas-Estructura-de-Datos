#include <iostream>
#include <list>
#include <vector>
#include <string>
#include <conio.h>
#include <windows.h>
#include <fstream>
#include <algorithm>
#include <ctime>
#include <cmath>

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

int nivelActual;
int frutasComidasNivelActual;
int frutasParaSiguienteNivel;
int velocidadJuego; 
bool partidaGuardada; 

enum Direccion { DETENIDO = 0, IZQUIERDA, DERECHA, ARRIBA, ABAJO };
Direccion direccionActual;

list<Coordenada> cuerpoSerpiente;
list<Coordenada> trampas; 
Coordenada posicionComida;
HANDLE hConsola = GetStdHandle(STD_OUTPUT_HANDLE);

void SiguienteNivel();
void GenerarTrampa();
void GuardarPartida();
bool CargarPartida();
void MostrarRanking();
void IniciarBucleJuego();


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
    for (auto const& trampa : trampas) if (trampa == pos) return true;
    return false;
}

void GenerarTrampa() {
    Coordenada nuevaTrampa;
    do {
        nuevaTrampa.x = rand() % ANCHO_TABLERO;
        nuevaTrampa.y = rand() % ALTO_TABLERO;
    } while (EstaOcupado(nuevaTrampa.x, nuevaTrampa.y) || (nuevaTrampa == posicionComida));
    trampas.push_back(nuevaTrampa);
}

void InicializarJuego() {
    juegoEnCurso = true;
    partidaGuardada = false; 
    puntajeActual = 0;
    
    nivelActual = 1;
    frutasComidasNivelActual = 0;
    frutasParaSiguienteNivel = 10;
    velocidadJuego = 100; 
    cuerpoSerpiente.clear();
    trampas.clear();
    
    cuerpoSerpiente.push_front({ (ANCHO_TABLERO / 2) + 2, ALTO_TABLERO / 2 });
    cuerpoSerpiente.push_back({ (ANCHO_TABLERO / 2) + 1, ALTO_TABLERO / 2 });
    cuerpoSerpiente.push_back({ (ANCHO_TABLERO / 2), ALTO_TABLERO / 2 });
    cuerpoSerpiente.push_back({ (ANCHO_TABLERO / 2) - 1, ALTO_TABLERO / 2 });
    cuerpoSerpiente.push_back({ (ANCHO_TABLERO / 2) - 2, ALTO_TABLERO / 2 });
    
    direccionActual = DERECHA; 

    posicionComida.x = rand() % (ANCHO_TABLERO - 2) + 1;
    posicionComida.y = rand() % (ALTO_TABLERO - 2) + 1;

    for(int i = 0; i < 2; i++) GenerarTrampa();

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
                    for (auto const& trampa : trampas) {
                        if (trampa == posActual) {
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
    
    cout << "Puntaje: " << puntajeActual << endl;
    cout << "Nivel: " << nivelActual << endl;
    cout << "Frutas: " << frutasComidasNivelActual << "/" << frutasParaSiguienteNivel << endl;
}

void LeerControles() {
    if (_kbhit()) {
        switch (_getch()) {
        case 'a': if (direccionActual != DERECHA) direccionActual = IZQUIERDA; break;
        case 'd': if (direccionActual != IZQUIERDA) direccionActual = DERECHA; break;
        case 'w': if (direccionActual != ABAJO) direccionActual = ARRIBA; break;
        case 's': if (direccionActual != ARRIBA) direccionActual = ABAJO; break;
        case 'x': juegoEnCurso = false; break;
        case 'p': 
            GuardarPartida(); 
            MoverCursor(0, ALTO_TABLERO + 4); 
            CambiarColor(COLOR_AMARILLO);
            cout << "Guardado. Volviendo...";
            Sleep(1500);
            partidaGuardada = true; 
            juegoEnCurso = false; 
            break;
        }
    }
}

void SiguienteNivel() {
    nivelActual++;
    frutasComidasNivelActual = 0;
    frutasParaSiguienteNivel += 10; 

    if (velocidadJuego > 30) velocidadJuego -= 10; 

    cuerpoSerpiente.clear();
    cuerpoSerpiente.push_front({ (ANCHO_TABLERO / 2) + 2, ALTO_TABLERO / 2 });
    cuerpoSerpiente.push_back({ (ANCHO_TABLERO / 2) + 1, ALTO_TABLERO / 2 });
    cuerpoSerpiente.push_back({ (ANCHO_TABLERO / 2), ALTO_TABLERO / 2 });
    cuerpoSerpiente.push_back({ (ANCHO_TABLERO / 2) - 1, ALTO_TABLERO / 2 });
    cuerpoSerpiente.push_back({ (ANCHO_TABLERO / 2) - 2, ALTO_TABLERO / 2 });
    direccionActual = DERECHA; 

    int trampasAAgregar = max(1, (int)(trampas.size() * 0.25));
    for(int i=0; i < trampasAAgregar; i++) GenerarTrampa();

    MoverCursor(ANCHO_TABLERO / 2 - 5, ALTO_TABLERO / 2);
    CambiarColor(COLOR_AMARILLO);
    cout << "NIVEL " << nivelActual;
    Sleep(1000); 
}

void ActualizarLogica() {
    if (direccionActual == DETENIDO) return;
    Coordenada nuevaCabeza = cuerpoSerpiente.front();
    switch (direccionActual) {
    case IZQUIERDA: nuevaCabeza.x--; break; case DERECHA: nuevaCabeza.x++; break;
    case ARRIBA: nuevaCabeza.y--; break; case ABAJO: nuevaCabeza.y++; break;
    }

    if (nuevaCabeza.x >= ANCHO_TABLERO) nuevaCabeza.x = 0;
    else if (nuevaCabeza.x < 0) nuevaCabeza.x = ANCHO_TABLERO - 1;
    if (nuevaCabeza.y >= ALTO_TABLERO) nuevaCabeza.y = 0;
    else if (nuevaCabeza.y < 0) nuevaCabeza.y = ALTO_TABLERO - 1;

    for (auto const& segmento : cuerpoSerpiente) {
        if (segmento == nuevaCabeza) { juegoEnCurso = false; return; }
    }
    
    bool trampaGolpeadaEsteTurno = false;

    for (auto it = trampas.begin(); it != trampas.end(); ++it) {
        if (*it == nuevaCabeza) {
            cuerpoSerpiente.pop_back(); 
            trampaGolpeadaEsteTurno = true; 
            
            if (cuerpoSerpiente.empty()) { 
                juegoEnCurso = false; 
                return; 
            }

            trampas.erase(it); 
            GenerarTrampa(); 
            break; 
        }
    }

    cuerpoSerpiente.push_front(nuevaCabeza);

    if (nuevaCabeza == posicionComida && !trampaGolpeadaEsteTurno) {
        puntajeActual += 10 * nivelActual; 
        frutasComidasNivelActual++;

        if (frutasComidasNivelActual >= frutasParaSiguienteNivel) {
            SiguienteNivel();
        }

        Coordenada nuevaPosComida;
        do {
            nuevaPosComida.x = rand() % ANCHO_TABLERO;
            nuevaPosComida.y = rand() % ALTO_TABLERO;
        } while (EstaOcupado(nuevaPosComida.x, nuevaPosComida.y));
        posicionComida = nuevaPosComida;
    }
    else {
        if (cuerpoSerpiente.size() > 1) 
        {
             cuerpoSerpiente.pop_back();
        }
    }

    if (cuerpoSerpiente.empty()) {
        juegoEnCurso = false;
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
    
    sort(listaRanking.begin(), listaRanking.end(), [](const RegistroJugador& a, const RegistroJugador& b) { return a.puntaje > b.puntaje; });

    bool esTop5 = false;
    if (puntajeFinal > 0) {
        if (listaRanking.size() < 5 || puntajeFinal > listaRanking.back().puntaje) {
            esTop5 = true;
        }

        CambiarColor(COLOR_ROJO_BRILLANTE); cout << "\n--- GAME OVER ---\n";
        CambiarColor(COLOR_BLANCO); cout << "Puntaje final: " << puntajeFinal << endl;
        cout << "Nivel alcanzado: " << nivelActual << endl;

        if (esTop5) {
            cout << "\nEntraste al TOP 5.\n";
            cout << "Ingresa tu nombre: "; string alias; cin >> alias;
            listaRanking.push_back({ alias, puntajeFinal });
            
            sort(listaRanking.begin(), listaRanking.end(), [](const RegistroJugador& a, const RegistroJugador& b) { return a.puntaje > b.puntaje; });
            if (listaRanking.size() > 5) listaRanking.resize(5);

            ofstream archivoEscritura(nombreArchivo);
            for (const auto& reg : listaRanking) {
                archivoEscritura << reg.nombre << " " << reg.puntaje << endl;
            }
            archivoEscritura.close();
        } else {
            cout << "\nNo alcanzaste el TOP 5 esta vez.\n";
        }
    }

    CambiarColor(COLOR_AMARILLO); cout << "\n=== TOP 5 ===\n"; CambiarColor(COLOR_BLANCO);
    if(listaRanking.empty()) {
        cout << "Aun no hay puntajes.\n";
    } else {
        for (const auto& reg : listaRanking) {
            cout << reg.nombre << "\t\t" << reg.puntaje << endl;
        }
    }
}


void GuardarPartida() {
    string archivoGuardado = "partida_snake.dat";
    ofstream archivo(archivoGuardado);
    if (!archivo.is_open()) return;

    archivo << puntajeActual << " "
            << nivelActual << " "
            << frutasComidasNivelActual << " "
            << frutasParaSiguienteNivel << " "
            << velocidadJuego << " "
            << (int)direccionActual << endl;

    archivo << posicionComida.x << " " << posicionComida.y << endl;

    archivo << cuerpoSerpiente.size() << endl;
    for (const auto& segmento : cuerpoSerpiente) {
        archivo << segmento.x << " " << segmento.y << endl;
    }

    archivo << trampas.size() << endl;
    for (const auto& trampa : trampas) {
        archivo << trampa.x << " " << trampa.y << endl;
    }

    archivo.close();
}

bool CargarPartida() {
    string archivoGuardado = "partida_snake.dat";
    ifstream archivo(archivoGuardado);
    if (!archivo.is_open()) return false; 

    cuerpoSerpiente.clear();
    trampas.clear();

    int dirInt;
    archivo >> puntajeActual
            >> nivelActual
            >> frutasComidasNivelActual
            >> frutasParaSiguienteNivel
            >> velocidadJuego
            >> dirInt;
    direccionActual = (Direccion)dirInt;

    archivo >> posicionComida.x >> posicionComida.y;

    int tamanoSerpiente;
    archivo >> tamanoSerpiente;
    for (int i = 0; i < tamanoSerpiente; i++) {
        Coordenada segmento;
        archivo >> segmento.x >> segmento.y;
        cuerpoSerpiente.push_back(segmento); 
    }

    int numTrampas;
    archivo >> numTrampas;
    for (int i = 0; i < numTrampas; i++) {
        Coordenada trampa;
        archivo >> trampa.x >> trampa.y;
        trampas.push_back(trampa);
    }

    archivo.close();
    juegoEnCurso = true; 
    partidaGuardada = false; 
    return true;
}

void IniciarBucleJuego() {
    while (juegoEnCurso) {
        RenderizarJuego(); 
        LeerControles(); 
        ActualizarLogica(); 
        Sleep(velocidadJuego);
    }
    
    if (!partidaGuardada) {
        GuardarYMostrarRanking(puntajeActual);
        CambiarColor(COLOR_BLANCO);
        cout << "\n\nPresiona cualquier tecla para volver al menu...";
        _getch();
    }
}

void MostrarRanking() {
    system("cls");
    GuardarYMostrarRanking(0); 
    
    CambiarColor(COLOR_BLANCO);
    cout << "\n\nPresiona cualquier tecla para volver al menu...";
    _getch();
}

int MostrarMenuPrincipal() {
    system("cls");
    
    CambiarColor(COLOR_BLANCO);
    cout << "\n\n"
         << "       1. Jugar (Nueva Partida)\n"
         << "       2. Cargar Partida\n"
         << "       3. Ver Ranking\n"
         << "       4. Salir\n\n"
         << "       Elige una opcion: ";
    
    int opcion;
    cin >> opcion;
    return opcion;
}


int main() {
    OcultarCursorConsola();
    srand(time(0)); 

    bool salirDelJuego = false;
    while (!salirDelJuego) {
        
        int opcion = MostrarMenuPrincipal();

        switch (opcion) {
            case 1: 
                InicializarJuego();
                IniciarBucleJuego();
                break;
            case 2: 
                system("cls");
                if (CargarPartida()) {
                    CambiarColor(COLOR_AMARILLO);
                    cout << "Partida cargada exitosamente.\nPresiona una tecla para continuar...";
                    _getch();
                    IniciarBucleJuego();
                } else {
                    CambiarColor(COLOR_ROJO_BRILLANTE);
                    cout << "No se encontro un archivo de guardado (partida_snake.dat)\n"
                         << "Presiona una tecla para volver al menu...";
                    _getch();
                }
                break;
            case 3:
                MostrarRanking();
                break;
            case 4:
                salirDelJuego = true;
                break;
            default:
                CambiarColor(COLOR_ROJO_BRILLANTE);
                cout << "Opcion no valida. Presiona una tecla...";
                _getch();
                break;
        }
    }

    CambiarColor(COLOR_BLANCO); 
    return 0;
}