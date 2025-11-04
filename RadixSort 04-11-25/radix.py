def countingSort(arr, exp):
    s = len(arr)
    outputArray = [0] * (s)
    countArray = [0] * (10)

    for j in range(0, s):
        idx = (arr[j] // exp)
        countArray[idx % 10] += 1

    for j in range(1, 10):
        countArray[j] += countArray[j - 1]

    j = s - 1
    while j >= 0:
        idx = (arr[j] // exp)
        outputArray[countArray[idx % 10] - 1] = arr[j]
        countArray[idx % 10] -= 1
        j -= 1

    j = 0
    for j in range(0, len(arr)):
        arr[j] = outputArray[j]

def radixSort(arr):
    max1 = max(arr)
    exp = 1
    while max1 / exp > 1:
        countingSort(arr, exp)
        exp *= 10

#Ejemplo
arr=[171, 46, 76, 91, 803, 25, 3, 67]
print("Arreglo antes de ordenar: ")
for j in range(len(arr)):
    print("%d" % arr[j], end=" ")

radixSort(arr)
print("\nArreglo despues de ordenar: ")
for j in range(len(arr)):
    print("%d" % arr[j], end=" ")

