class NodoDoble {
    constructor(d) {
        this.data = d;
        this.next = null;
        this.prev = null;
    }
}

class ListaDoblementeCircular {
    constructor() {
        this.head = null;
    }

    isEmpty() {
        return this.head === null;
    }

    add_inicio(data) {
        const nuevo = new NodoDoble(data);

        if (this.isEmpty()) {
            this.head = nuevo;
            this.head.next = this.head;
            this.head.prev = this.head;
        } else {
            const tail = this.head.prev;
            nuevo.next = this.head;
            this.head.prev = nuevo;
            this.head = nuevo;
            this.head.prev = tail;
            tail.next = this.head;
        }
    }

    add_final(data) {
        const nuevo = new NodoDoble(data);

        if (this.isEmpty()) {
            this.head = nuevo;
            this.head.next = this.head;
            this.head.prev = this.head;
        } else {
            const tail = this.head.prev;
            tail.next = nuevo;
            nuevo.prev = tail;
            nuevo.next = this.head;
            this.head.prev = nuevo;
        }
    }

    print_Lista() {
        if (this.isEmpty()) {
            console.log("None");
            return;
        }

        let actual = this.head;
        let output = "";
        do {
            output += actual.data + " <-> ";
            actual = actual.next;
        } while (actual !== this.head);

        output += "(vuelve a " + this.head.data + ")";
        console.log(output);
    }

    search(data) {
        if (this.isEmpty()) return false;

        let actual = this.head;
        do {
            if (actual.data === data) return true;
            actual = actual.next;
        } while (actual !== this.head);

        return false;
    }

    eliminar(data) {
        if (this.isEmpty()) return;

        let actual = this.head;

        do {
            if (actual.data === data) break;
            actual = actual.next;
        } while (actual !== this.head);

        if (actual.data !== data) return;

        if (actual === this.head && this.head.next === this.head) {
            this.head = null;
            return;
        }

        if (actual === this.head)
            this.head = actual.next;

        actual.prev.next = actual.next;
        actual.next.prev = actual.prev;
    }
}

const lista = new ListaDoblementeCircular();

lista.add_final(10);
lista.add_final(20);
lista.add_final(30);
lista.print_Lista();

lista.add_inicio(5);
lista.print_Lista();

console.log("¿Está el 20? " + lista.search(20));
console.log("¿Está el 99? " + lista.search(99));

lista.eliminar(20);
lista.print_Lista();

lista.eliminar(5);
lista.print_Lista();

lista.eliminar(30);
lista.print_Lista();