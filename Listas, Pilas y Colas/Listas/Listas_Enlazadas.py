class Nodo:
    def __init__(self, data):
        self.data = data
        self.next = None

class ListaEnlazada:
    def __init__(self):
        self.head = None

    def insertar_al_principio(self, data):
        nuevo = Nodo(data)
        nuevo.next = self.head
        self.head = nuevo

    def insertar_al_final(self, data):
        nuevo = Nodo(data)
        if not self.head:
            self.head = nuevo
            return
        ultimo = self.head
        while ultimo.next:
            ultimo = ultimo.next
        ultimo.next = nuevo

    def imprimir_lista(self):
        actual = self.head
        output = ""
        while actual:
            output += str(actual.data) + " -> "
            actual = actual.next
        output += "None"
        print(output)

    def buscar(self, data):
        actual = self.head
        while actual:
            if actual.data == data:
                return True
            actual = actual.next
        return False

    def eliminar(self, data):
        actual = self.head
        previo = None

        if actual and actual.data == data:
            self.head = actual.next
            return

        while actual and actual.data != data:
            previo = actual
            actual = actual.next

        if not actual:
            return

        previo.next = actual.next

if __name__ == "__main__":
    lista = ListaEnlazada()
    lista.insertar_al_final(10)
    lista.insertar_al_final(20)
    lista.insertar_al_final(30)
    print("Lista inicial:", end=" ")
    lista.imprimir_lista()

    lista.insertar_al_principio(5)
    print("Lista después de insertar al principio:", end=" ")
    lista.imprimir_lista()

    print("¿Esta el 20?", lista.buscar(20))
    print("¿Esta el 99?", lista.buscar(99))

    lista.eliminar(20)
    print("Lista después de eliminar 20:", end=" ")
    lista.imprimir_lista()

    lista.eliminar(5)
    print("Lista después de eliminar 5:", end=" ")
    lista.imprimir_lista()