class Pila_Arreglos:
    def __init__(self, tam):
        self.max_size = tam
        self.stack = [0] * self.max_size
        self.top = -1

    def is_empty(self):
        return self.top == -1

    def is_full(self):
        return self.top == self.max_size - 1

    def push(self, data):
        if self.is_full():
            print("Error: Overflow de Pila")
            return
        self.top += 1
        self.stack[self.top] = data

    def pop(self):
        if self.is_empty():
            print("Error: Underflow de Pila")
            return -1
        data = self.stack[self.top]
        self.top -= 1
        return data

    def peek(self):
        if self.is_empty():
            print("La pila está vacía")
            return -1
        return self.stack[self.top]

if __name__ == "__main__":
    pila = Pila_Arreglos(100)
    pila.push(10)
    pila.push(20)
    pila.push(30)

    print("Elemento superior(TOP):", pila.peek())
    print("Extrae elemento:", pila.pop())
    print("Nuevo elemento superior:", pila.peek())