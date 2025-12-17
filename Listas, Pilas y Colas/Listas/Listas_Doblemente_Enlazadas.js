class NodoDoble {
    constructor(data) {
        this.data = data;
        this.next = null;
        this.prev = null;
    }
}

class ListaDoblementeEnlazada {
    constructor() {
        this.head = null;
        this.tail = null;
    }

    add_inicio(data) {
        const nuevo = new NodoDoble(data);
        if (!this.head) {
            this.head = this.tail = nuevo;
        } else {
            nuevo.next = this.head;
            this.head.prev = nuevo;
            this.head = nuevo;
        }
    }

    add_final (data) {
        const nuevo = new NodoDoble(data);
        if (!this.tail) {
            this.head = this.tail = nuevo;
        } else {
            this.tail.next = nuevo;
            nuevo.prev = this.tail;
            this.tail = nuevo;
        }
    }

    printLista() {
        let actual = this.head;
        let output = "";
        while (actual) {
            output += actual.data + " <-> ";
            actual = actual.next;
        }
        output += "None";
        console.log(output);
    }

    search(data) {
        let actual = this.head;
        while (actual) {
            if (actual.data === data) return true;
            actual = actual.next;
        }
        return false;
    }

    eliminar(data) {
        let actual = this.head;

        while (actual && actual.data !== data)
            actual = actual.next;

        if (!actual) return;

        if (actual.prev)
            actual.prev.next = actual.next;
        else
            this.head = actual.next;

        if (actual.next)
            actual.next.prev = actual.prev;
        else
            this.tail = actual.prev;
    }
}


const lista = new ListaDoblementeEnlazada();
lista.add_final(10);
lista.add_final(20);
lista.add_final(30);
lista.printLista();

lista.add_inicio(5);
lista.printLista();

console.log("¿Está el 20? " + lista.search(20));
console.log("¿Está el 99? " + lista.search(99));

lista.eliminar(20);
lista.printLista();

lista.eliminar(5);
lista.printLista();

lista.eliminar(30);
lista.printLista();