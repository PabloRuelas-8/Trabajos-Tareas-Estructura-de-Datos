class Nodo {
    constructor(data) {
        this.data = data;
        this.next = null;
    }
}

class ListaEnlazada {
    constructor() {
        this.head = null;
    }

    insertar_al_principio(data) {
        const nuevo = new Nodo(data);
        nuevo.next = this.head;
        this.head = nuevo;
    }

    insertar_al_final(data) {
        const nuevo = new Nodo(data);
        if (!this.head) {
            this.head = nuevo;
            return;
        }
        let ultimo = this.head;
        while (ultimo.next) {
            ultimo = ultimo.next;
        }
        ultimo.next = nuevo;
    }

    imprimir_lista() {
        let actual = this.head;
        let output = "";
        while (actual) {
            output += actual.data + " -> ";
            actual = actual.next;
        }
        output += "None";
        console.log(output);
    }

    buscar(data) {
        let actual = this.head;
        while (actual) {
            if (actual.data === data) return true;
            actual = actual.next;
        }
        return false;
    }

    eliminar(data) {
        let actual = this.head;
        let previo = null;

        if (actual && actual.data === data) {
            this.head = actual.next;
            return;
        }

        while (actual && actual.data !== data) {
            previo = actual;
            actual = actual.next;
        }

        if (!actual) return;

        previo.next = actual.next;
    }
}

const lista = new ListaEnlazada();
lista.insertar_al_final(10);
lista.insertar_al_final(20);
lista.insertar_al_final(30);
process.stdout.write("Lista inicial: ");
lista.imprimir_lista();

lista.insertar_al_principio(5);
process.stdout.write("Lista después de insertar al principio: ");
lista.imprimir_lista();

console.log("¿Esta el 20? " + lista.buscar(20));
console.log("¿Esta el 99? " + lista.buscar(99));

lista.eliminar(20);
process.stdout.write("Lista después de eliminar 20: ");
lista.imprimir_lista();

lista.eliminar(5);
process.stdout.write("Lista después de eliminar 5: ");
lista.imprimir_lista();