class NodoDoble:
    def __init__(self, data):
        self.data = data
        self.next = None
        self.prev = None

class ListaDoblementeEnlazada:
    def __init__(self):
        self.head = None
        self.tail = None

    def add_inicio(self, data):
        nuevo = NodoDoble(data)
        if not self.head:
            self.head = self.tail = nuevo
        else:
            nuevo.next = self.head
            self.head.prev = nuevo
            self.head = nuevo

    def add_final (self, data):
        nuevo = NodoDoble(data)
        if not self.tail:
            self.head = self.tail = nuevo
        else:
            self.tail.next = nuevo
            nuevo.prev = self.tail
            self.tail = nuevo

    def printLista(self):
        actual = self.head
        output = ""
        while actual:
            output += str(actual.data) + " <-> "
            actual = actual.next
        output += "None"
        print(output)

    def search(self, data):
        actual = self.head
        while actual:
            if actual.data == data:
                return True
            actual = actual.next
        return False

    def eliminar(self, data):
        actual = self.head

        while actual and actual.data != data:
            actual = actual.next

        if not actual:
            return

        if actual.prev:
            actual.prev.next = actual.next
        else:
            self.head = actual.next

        if actual.next:
            actual.next.prev = actual.prev
        else:
            self.tail = actual.prev

if __name__ == "__main__":
    lista = ListaDoblementeEnlazada()
    lista.add_final(10)
    lista.add_final(20)
    lista.add_final(30)
    lista.printLista()

    lista.add_inicio(5)
    lista.printLista()

    print("¿Está el 20?", lista.search(20))
    print("¿Está el 99?", lista.search(99))

    lista.eliminar(20)
    lista.printLista()

    lista.eliminar(5)
    lista.printLista()

    lista.eliminar(30)
    lista.printLista()