MAXSIZE = 5
queue = [0] * MAXSIZE
front = -1
rear = -1

def insertar():
    global front, rear, queue
    try:
        elemento = int(input("\nIngrese el elemento que se va a insertar "))
    except ValueError:
        print("\nEntrada inválida.")
        return

    if rear == MAXSIZE - 1:
        print("\nDESBORDAMIENTO (OVERFLOW)\n")
        return
    if front == -1 and rear == -1:
        front = rear = 0
    else:
        rear += 1

    queue[rear] = elemento
    print("\nElemento insertado correctamente.\n")

def eliminar():
    global front, rear, queue
    if front == -1 or front > rear:
        print("\nSUBDESBORDAMIENTO (UNDERFLOW)\n")
        return

    elemento = queue[front]
    if front == rear:
        front = rear = -1
    else:
        front += 1

    print(f"\nElemento correctamente eliminado: {elemento}\n")

def mostrar():
    global front, rear, queue
    if rear == -1 or front == -1 or front > rear:
        print("\nLa cola está vacía.\n")
    else:
        print("\nElementos en la cola:")
        for i in range(front, rear + 1):
            print(queue[i])

if __name__ == "__main__":
    opcion = 0

    while opcion != 4:
        print("\n========== MENÚ PRINCIPAL ==========")
        print("1. Insertar un elemento")
        print("2. Eliminar un elemento")
        print("3. Mostrar la cola")
        print("4. Salir")
        
        try:
            opcion = int(input("Ingrese su opción: "))
        except ValueError:
            opcion = 0

        if opcion == 1: insertar()
        elif opcion == 2: eliminar()
        elif opcion == 3: mostrar()
        elif opcion == 4: print("\nCerrando...\n")
        else: print("\nOpción inválida.\n")