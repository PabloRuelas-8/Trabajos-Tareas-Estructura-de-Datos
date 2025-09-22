def insertionSort(a):
    for i in range (1, len(a)):
        temp = a[i]

        j = i-1
        while j >= 0 and temp < a[j] : 
            a[j + 1] = a[j]
            j = j-1
        a[j+1] = temp
def printArr(a):
    for i in range (len(a)):
        print (a[i], end = " ")
a = [33,55,77,22,88,11,99,44,66]
print("Antes de ordenar los elementos del arreglo: ")
printArr(a)
insertionSort(a)
print("\nDespue de arregloa los elementos del arreglo: ")
printArr(a)