class Nodo:
    def __init__(self, val):
        self.data = val
        self.next = None

class ListaCircularEnlazada:
    def __init__(self):
        self.head = None

    def add_inicio(self, data):
        nuevo_nodo = Nodo(data)
        if self.head is None:
            self.head = nuevo_nodo
            nuevo_nodo.next = self.head
        else:
            tail = self.head
            while tail.next != self.head:
                tail = tail.next
            nuevo_nodo.next = self.head
            self.head = nuevo_nodo
            tail.next = self.head

    def add_final(self, data):
        nuevo_nodo = Nodo(data)
        if self.head is None:
            self.head = nuevo_nodo
            nuevo_nodo.next = self.head
        else:
            tail = self.head
            while tail.next != self.head:
                tail = tail.next
            tail.next = nuevo_nodo
            nuevo_nodo.next = self.head

    def print_lista(self):
        if self.head is None:
            print("null")
            return
        
        output = ""
        nodo_actual = self.head
        while True:
            output += str(nodo_actual.data) + " -> "
            nodo_actual = nodo_actual.next
            if nodo_actual == self.head:
                break
        
        output += "(vuelve a " + str(self.head.data) + ")"
        print(output)

    def search(self, data_buscada):
        if self.head is None:
            return False
        
        nodo_actual = self.head
        while True:
            if nodo_actual.data == data_buscada:
                return True
            nodo_actual = nodo_actual.next
            if nodo_actual == self.head:
                break
        
        return False

    def eliminar(self, data_a_eliminar):
        if self.head is None:
            return

        if self.head.data == data_a_eliminar:
            if self.head.next == self.head:
                self.head = None
                return
            
            tail = self.head
            while tail.next != self.head:
                tail = tail.next
            
            self.head = self.head.next
            tail.next = self.head
            return

        nodo_previo = self.head
        nodo_actual = self.head.next
        while nodo_actual != self.head:
            if nodo_actual.data == data_a_eliminar:
                nodo_previo.next = nodo_actual.next
                return
            nodo_previo = nodo_actual
            nodo_actual = nodo_actual.next

if __name__ == "__main__":
    mi_lista = ListaCircularEnlazada()
    mi_lista.add_final(10)
    mi_lista.add_final(20)
    mi_lista.add_final(30)
    mi_lista.print_lista()

    mi_lista.add_inicio(5)
    mi_lista.print_lista()

    print("¿Está el 20?", mi_lista.search(20))
    print("¿Está el 99?", mi_lista.search(99))

    mi_lista.eliminar(20)
    mi_lista.print_lista()
    
    mi_lista.eliminar(5)
    mi_lista.print_lista()
    
    mi_lista.eliminar(30)
    mi_lista.print_lista()