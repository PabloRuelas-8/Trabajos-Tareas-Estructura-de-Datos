class NodoDoble:
    def __init__(self, d):
        self.data = d
        self.next = None
        self.prev = None

class ListaDoblementeCircular:
    def __init__(self):
        self.head = None

    def is_empty(self):
        return self.head is None

    def add_inicio(self, data):
        nuevo = NodoDoble(data)

        if self.is_empty():
            self.head = nuevo
            self.head.next = self.head
            self.head.prev = self.head
        else:
            tail = self.head.prev
            nuevo.next = self.head
            self.head.prev = nuevo
            self.head = nuevo
            self.head.prev = tail
            tail.next = self.head

    def add_final(self, data):
        nuevo = NodoDoble(data)

        if self.is_empty():
            self.head = nuevo
            self.head.next = self.head
            self.head.prev = self.head
        else:
            tail = self.head.prev
            tail.next = nuevo
            nuevo.prev = tail
            nuevo.next = self.head
            self.head.prev = nuevo

    def print_Lista(self):
        if self.is_empty():
            print("None")
            return

        actual = self.head
        output = ""
        while True:
            output += str(actual.data) + " <-> "
            actual = actual.next
            if actual == self.head:
                break

        output += "(vuelve a " + str(self.head.data) + ")"
        print(output)

    def search(self, data):
        if self.is_empty():
            return False

        actual = self.head
        while True:
            if actual.data == data:
                return True
            actual = actual.next
            if actual == self.head:
                break

        return False

    def eliminar(self, data):
        if self.is_empty():
            return

        actual = self.head

        while True:
            if actual.data == data:
                break
            actual = actual.next
            if actual == self.head:
                break

        if actual.data != data:
            return

        if actual == self.head and self.head.next == self.head:
            self.head = None
            return

        if actual == self.head:
            self.head = actual.next

        actual.prev.next = actual.next
        actual.next.prev = actual.prev

if __name__ == "__main__":
    lista = ListaDoblementeCircular()

    lista.add_final(10)
    lista.add_final(20)
    lista.add_final(30)
    lista.print_Lista()

    lista.add_inicio(5)
    lista.print_Lista()

    print("¿Está el 20?", lista.search(20))
    print("¿Está el 99?", lista.search(99))

    lista.eliminar(20)
    lista.print_Lista()

    lista.eliminar(5)
    lista.print_Lista()

    lista.eliminar(30)
    lista.print_Lista()