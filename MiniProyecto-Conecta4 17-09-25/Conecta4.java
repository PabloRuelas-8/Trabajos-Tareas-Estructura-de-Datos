import java.util.Scanner;

public class Conecta4 {

    static final int FILAS = 6;
    static final int COLUMNAS = 7;
    static String[][] tablero = new String[FILAS][COLUMNAS];
    static String jugadorActual = "X";
    static Scanner sc = new Scanner(System.in);

    public static void main(String[] args) {
        inicializarTablero();
        System.out.println("Conecta 4 - Miniproyecto");
        turnoJugador();
    }

    static void inicializarTablero() {
        for (int i = 0; i < FILAS; i++) {
            for (int j = 0; j < COLUMNAS; j++) {
                tablero[i][j] = " ";
            }
        }
    }

    static void imprimirTablero() {
        System.out.println();
        for (int i = 0; i < FILAS; i++) {
            System.out.print("|");
            for (int j = 0; j < COLUMNAS; j++) {
                System.out.print(" " + tablero[i][j] + " |");
            }
            System.out.println();
            System.out.println("-----------------------------");
        }
        System.out.println("  1   2   3   4   5   6   7 ");
        System.out.println();
    }

    static boolean verificarGanador() {
        for (int i = 0; i < FILAS; i++) {
            for (int j = 0; j <= COLUMNAS - 4; j++) {
                if (tablero[i][j].equals(jugadorActual) &&
                    tablero[i][j+1].equals(jugadorActual) &&
                    tablero[i][j+2].equals(jugadorActual) &&
                    tablero[i][j+3].equals(jugadorActual)) {
                    return true;
                }
            }
        }

        for (int i = 0; i <= FILAS - 4; i++) {
            for (int j = 0; j < COLUMNAS; j++) {
                if (tablero[i][j].equals(jugadorActual) &&
                    tablero[i+1][j].equals(jugadorActual) &&
                    tablero[i+2][j].equals(jugadorActual) &&
                    tablero[i+3][j].equals(jugadorActual)) {
                    return true;
                }
            }
        }

        // d abajo derecha
        for (int i = 0; i <= FILAS - 4; i++) {
            for (int j = 0; j <= COLUMNAS - 4; j++) {
                if (tablero[i][j].equals(jugadorActual) &&
                    tablero[i+1][j+1].equals(jugadorActual) &&
                    tablero[i+2][j+2].equals(jugadorActual) &&
                    tablero[i+3][j+3].equals(jugadorActual)) {
                    return true;
                }
            }
        }

        // d abajo izquierdq
        for (int i = 0; i <= FILAS - 4; i++) {
            for (int j = 3; j < COLUMNAS; j++) {
                if (tablero[i][j].equals(jugadorActual) &&
                    tablero[i+1][j-1].equals(jugadorActual) &&
                    tablero[i+2][j-2].equals(jugadorActual) &&
                    tablero[i+3][j-3].equals(jugadorActual)) {
                    return true;
                }
            }
        }

        return false;
    }

    static boolean verificarEmpate() {
        for (int j = 0; j < COLUMNAS; j++) {
            if (tablero[0][j].equals(" ")) { 
                return false;
            }
        }
        return true;
    }

    static void turnoJugador() {
        imprimirTablero();
        System.out.print("Jugador " + jugadorActual + ", elige una columna (1-7): ");
        int columna;

        try {
            columna = Integer.parseInt(sc.nextLine()) - 1;
        } catch (Exception e) {
            System.out.println("Ingresa un numero valido.");
            turnoJugador();
            return;
        }

        if (columna < 0 || columna >= COLUMNAS) {
            System.out.println("Columna fuera de rango.");
            turnoJugador();
            return;
        }

        for (int fila = FILAS - 1; fila >= 0; fila--) {
            if (tablero[fila][columna].equals(" ")) {
                tablero[fila][columna] = jugadorActual;

                if (verificarGanador()) {
                    imprimirTablero();
                    System.out.println("Jugador " + jugadorActual + " gana!");
                    return;
                } else if (verificarEmpate()) {
                    imprimirTablero();
                    System.out.println("Es un empate!");
                    return;
                }

                jugadorActual = jugadorActual.equals("X") ? "O" : "X";
                turnoJugador();
                return;
            }
        }

        System.out.println("Esa columna esta llena, elige otra.");
        turnoJugador();
    }
}

