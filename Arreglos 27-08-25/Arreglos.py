numeros = [10, 20, 30, 40]


numeros[2] = 69

for n in numeros:
    print(n)

tamaño = int(input("de que tamaño sera el arreglo?: "))
arr = []
for i in range(tamaño):
    valor = int(input(f"ingrese el valor {i+1}: "))
    arr.append(valor)
print(arr)