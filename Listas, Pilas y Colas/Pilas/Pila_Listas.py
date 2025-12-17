class Nodo:
    def __init__(self, d):
        self.data = d
        self.next = None

class Pila_Listas:
    def __init__(self):
        self.top = None

    def is_empty(self):
        return self.top is None

    def push(self, data):
        nuevo = Nodo(data)
        nuevo.next = self.top
        self.top = nuevo

    def pop(self):
        if self.is_empty():
            print("Error: Underflow de Pila")
            return -1
        dato = self.top.data
        self.top = self.top.next
        return dato

    def peek(self):
        if self.is_empty():
            print("La Pila esta vacia")
            return -1
        return self.top.data

if __name__ == "__main__":
    pila = Pila_Listas()
    pila.push(10)
    pila.push(20)
    pila.push(30)

    print("Elemento superior:", pila.peek())
    print("Extrae elemento:", pila.pop())
    print("Nuevo elemento superior:", pila.peek())