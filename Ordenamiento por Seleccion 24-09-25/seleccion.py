def selection(a):
    for i in range(len(a)):
        small = i 
        for j in range(i+1, len(a)):
            if a[small] > a[j]:
                small = j 

        a[i], a[small] = a[small], a[i]

def printArr(a):
    for i in range(len(a)):
        print (a[i], end = " ")
a = [66,11,88,44,99,33,55,22]

print("Arreglo antes de ser ordenado: ")
printArr(a)
selection(a)
print("\nArreglo despues de ser ordenado: ")
selection(a)
printArr(a)