numeros = []

pares = 0 
impares = 0 

for i in range (10):
    n = int(input(f"Ingrese el numero {i+1}: "))
    numeros.append(n)

    if n % 2 == 0: 
        pares += 1
    else:
        impares +=1


promedio_nums = sum(numeros) / len(numeros)

print("\nResultados: ")
print("Total de numeros pares: ", pares)
print("Total de numeros impares: ", impares)
print("Promedio de los numeros: ", promedio_nums)

input("Presiones cualquier tecla para salir...")