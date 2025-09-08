matriz = [
    [1, 2, 3],
    [4, 5, 6],
    [7, 8, 9]
]

print("Matriz original:")
for fila in matriz:
    print(fila)

print("\n En filas:")
for fila in matriz:
    for elem in fila:
        print(elem, end=" ")

print("\n En columnas:")
for j in range(len(matriz[0])):
    for i in range(len(matriz)):
        print(matriz[i][j], end=" ")
