import random
import copy


class LogicaSudoku:
    def __init__(self):
        pass

    def resolver(self, tablero):
       
        casilla_vacia = self.encontrar_casilla_vacia(tablero)
        if not casilla_vacia:
            return True 
        else:
            fila, col = casilla_vacia

        numeros = list(range(1, 10))
        random.shuffle(numeros)

        for num in numeros:
            if self.es_valido(tablero, num, fila, col):
                tablero[fila][col] = num

                if self.resolver(tablero):
                    return True

                tablero[fila][col] = 0  
        
        return False

    def es_valido(self, tablero, num, fila, col):
        
        #checar fila
        for j in range(9):
            if tablero[fila][j] == num and col != j:
                return False

        #checar columnas
        for i in range(9):
            if tablero[i][col] == num and fila != i:
                return False

        #checar las cajas
        caja_fila_inicio = (fila // 3) * 3
        caja_col_inicio = (col // 3) * 3
        for i in range(caja_fila_inicio, caja_fila_inicio + 3):
            for j in range(caja_col_inicio, caja_col_inicio + 3):
                if tablero[i][j] == num and (i, j) != (fila, col):
                    return False
        
        return True

    def encontrar_casilla_vacia(self, tablero):
        for i in range(9):
            for j in range(9):
                if tablero[i][j] == 0:
                    return (i, j) 
        return None

    def generar_tablero_completo(self):

        tablero_nuevo = [[0 for _ in range(9)] for _ in range(9)]
        
        self.resolver(tablero_nuevo)
        
        return tablero_nuevo

    def crear_puzzle(self, tablero_completo, casillas_reveladas):

        puzzle = copy.deepcopy(tablero_completo)
        agujeros = 81 - casillas_reveladas
        
        coordenadas = set()
        while len(coordenadas) < agujeros:
            fila = random.randint(0, 8)
            col = random.randint(0, 8)
            coordenadas.add((fila, col))
            
        for fila, col in coordenadas:
            puzzle[fila][col] = 0
            
        return puzzle

def imprimir_tablero(tablero):
    print("\n    1 2 3   4 5 6   7 8 9 (Columnas)")
    print("  +-------+-------+-------+")
    for i in range(9):
        if i % 3 == 0 and i != 0:
            print("  |-------+-------+-------|")
        
        print(f"{i+1} |", end=" ") 
        
        for j in range(9):
            if j % 3 == 0 and j != 0:
                print("| ", end="")
            
            valor = tablero[i][j] if tablero[i][j] != 0 else "."
            print(f"{valor} ", end="")
        print("|")
    print("  +-------+-------+-------+")


def tablero_esta_lleno(tablero):
    for i in range(9):
        for j in range(9):
            if tablero[i][j] == 0:
                return False
    return True


class JuegoSudoku:
    
    def __init__(self):
        self.generador = LogicaSudoku()
        
        self.niveles = [
            ("Muy Facil", (36, 44)),
            ("Facil", (32, 35)),
            ("Medio", (28, 31)),
            ("Dificil", (24, 27)),
            ("Muy Dificil", (17, 23))
        ]
        
        self.nivel_actual_idx = 0
        self.sub_sudoku_actual = 1
        
    def iniciar_juego(self):
        
        
        while self.nivel_actual_idx < len(self.niveles):
            
            nombre_nivel, (min_rev, max_rev) = self.niveles[self.nivel_actual_idx]
            print(f"\n---==[Nivel: {nombre_nivel} ]==---")
            
            self.sub_sudoku_actual = 1
            
            while self.sub_sudoku_actual <= 5:
                
                print(f"\n[Nivel: {nombre_nivel} - Sudoku {self.sub_sudoku_actual} de 5]")
                
                num_reveladas = random.randint(min_rev, max_rev)
                tablero_solucion = self.generador.generar_tablero_completo()
                tablero_puzzle = self.generador.crear_puzzle(tablero_solucion, num_reveladas)
                
                resuelto = self.jugar_un_tablero(tablero_solucion, tablero_puzzle)
                
                if resuelto:
                    # ¡Éxito!
                    print(f"Sudoku {self.sub_sudoku_actual} completado.")
                    self.sub_sudoku_actual += 1
                else:
                    # ¡Error! (Perdió 5 vidas)
                    print(f"Perdiste todas las vidas, mismo nivel, diferente sudoku...")
                    # No incrementamos 'sub_sudoku_actual', se repite el nivel
            
            print(f"\nHas completado el nivel {nombre_nivel}.")
            self.nivel_actual_idx += 1
            
        print("\nFELICIDADES, NO TIENES NADA QUE HACER")
        print("Completaste todos los niveles del juego")

    def jugar_un_tablero(self, solucion, puzzle):

        tablero_jugador = copy.deepcopy(puzzle)
        vidas_tablero = 5 
        
        casillas_originales = set()
        for i in range(9):
            for j in range(9):
                if puzzle[i][j] != 0:
                    casillas_originales.add((i, j))
        
        while True:
            imprimir_tablero(tablero_jugador) 
            print(f"   [ Vidas para este tablero: {vidas_tablero} ]")
            
            fila, col, num = self.pedir_jugada(casillas_originales)
            
            if solucion[fila][col] == num:
            
                tablero_jugador[fila][col] = num
                print("\nCorrecto!")
                
                if tablero_esta_lleno(tablero_jugador):
                    print("\nTablero completado!")
                    imprimir_tablero(tablero_jugador)
                    return True 
            else:
            
                vidas_tablero -= 1
                print(f"\nMal. El número {num} no va en la casilla ({fila+1}, {col+1}).")

                if vidas_tablero > 0:
                    print(f"Te quedan {vidas_tablero} vidas para este tablero.")
                else:
                    print("Te quedaste sin vidas para este tablero")
                    return False
            

    def pedir_jugada(self, casillas_originales):
        while True:
            try:
                entrada = input("\nIntroduce tu jugada (fila,columna,numero). Ejemplo: 1,6,9 ")
                partes = entrada.split(',')
                
                if len(partes) != 3:
                    print("Formato incorrecto, debe ser fila,columna,numero.")
                    continue
                    
                fila = int(partes[0].strip())
                col = int(partes[1].strip())
                num = int(partes[2].strip())
                
                if not (1 <= fila <= 9 and 1 <= col <= 9 and 1 <= num <= 9):
                    print("Los numeros deben estar entre 1 y 9")
                    continue
                
                fila_idx = fila - 1
                col_idx = col - 1
                
                if (fila_idx, col_idx) in casillas_originales:
                    print("No puedes modificar una casilla original del sudoku")
                    continue
                    
                return fila_idx, col_idx, num
                
            except ValueError:
                print("Entrada invalida, usar solo numeros y comas.")
            except Exception as e:
                print(f"Error inesperado: {e}")


if __name__ == "__main__":
    juego = JuegoSudoku()
    juego.iniciar_juego()