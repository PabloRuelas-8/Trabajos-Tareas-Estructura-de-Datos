class Nodo:
    def __init__(self, data):
        self.data = data
        self.next = None

class ColaLista:
    def __init__(self):
        self.front = None
        self.rear = None

    def is_empty(self):
        return self.front is None

    def enqueue(self, data):
        nuevo_nodo = Nodo(data)

        if self.rear is None:
            self.front = nuevo_nodo
            self.rear = nuevo_nodo
            return

        self.rear.next = nuevo_nodo
        self.rear = nuevo_nodo

    def dequeue(self):
        if self.is_empty():
            print("Error: Cola subdesbordada (Underflow)")
            return -1

        data = self.front.data
        self.front = self.front.next

        if self.front is None:
            self.rear = None

        return data

    def peek(self):
        if self.is_empty():
            print("Cola vacía")
            return -1
        return self.front.data

if __name__ == "__main__":
    cola = ColaLista()

    cola.enqueue(10)
    cola.enqueue(20)
    cola.enqueue(30)

    print("Elemento frontal:", cola.peek())
    print("Elimina elemento:", cola.dequeue())
    print("Nuevo elemento frontal:", cola.peek())