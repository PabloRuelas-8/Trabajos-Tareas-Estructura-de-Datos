arr = [44, 66, 88, 55, 33, 11, 22, 77]

print("Arreglo original:")
print(arr)


n = len(arr)
for i in range(n - 1):
    for j in range(n - 1 - i):
        if arr[j] > arr[j + 1]:
            arr[j], arr[j + 1] = arr[j + 1], arr[j]

print("Arreglo ordenado de menor a mayor:")
print(arr)
