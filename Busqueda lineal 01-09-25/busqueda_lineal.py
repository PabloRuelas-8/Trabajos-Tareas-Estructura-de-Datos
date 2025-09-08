def imprimir(arre):

    print("El arreglo inicial es:", " ".join(str(x) for x in arre))


def insertar_elemento(arre, val, indice):

    n = len(arre)
    for i in range(n - 1, indice, -1):
        arre[i] = arre[i - 1]

    arre[indice] = val


def busqueda_lineal(arre, obj):
 
    for i in range(len(arre)):
        if arre[i] == obj:
            return i
    return -1


arre = [1, 2, 3, 4, 5]
imprimir(arre)

val = int(input("Valor para insertar: "))
indice = int(input("Indice donde se va a insertar (0..{}): ".format(len(arre)-1)))

insertar_elemento( arre, val, indice)
imprimir(arre)

obj = val
pos = busqueda_lineal(arre, obj)
print(f"Búsqueda lineal de {obj}: indice {pos}")
input("\nPresiona enter para salir")
