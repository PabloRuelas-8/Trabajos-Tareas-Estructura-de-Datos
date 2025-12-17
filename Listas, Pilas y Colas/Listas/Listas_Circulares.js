class Nodo {
    constructor(val) {
        this.data = val;
        this.next = null;
    }
}

class ListaCircularEnlazada {
    constructor() {
        this.head = null;
    }

    add_inicio(data) {
        const nuevo_nodo = new Nodo(data);
        if (this.head === null) {
            this.head = nuevo_nodo;
            nuevo_nodo.next = this.head;
        } else {
            let tail = this.head;
            while (tail.next !== this.head) {
                tail = tail.next;
            }
            nuevo_nodo.next = this.head;
            this.head = nuevo_nodo;
            tail.next = this.head;
        }
    }

    add_final(data) {
        const nuevo_nodo = new Nodo(data);
        if (this.head === null) {
            this.head = nuevo_nodo;
            nuevo_nodo.next = this.head;
        } else {
            let tail = this.head;
            while (tail.next !== this.head) {
                tail = tail.next;
            }
            tail.next = nuevo_nodo;
            nuevo_nodo.next = this.head;
        }
    }

    print_lista() {
        if (this.head === null) {
            console.log("null");
            return;
        }
        
        let output = "";
        let nodo_actual = this.head;
        do {
            output += nodo_actual.data + " -> ";
            nodo_actual = nodo_actual.next;
        } while (nodo_actual !== this.head);
        
        output += "(vuelve a " + this.head.data + ")";
        console.log(output);
    }

    search(data_buscada) {
        if (this.head === null) {
            return false;
        }
        
        let nodo_actual = this.head;
        do {
            if (nodo_actual.data === data_buscada) {
                return true;
            }
            nodo_actual = nodo_actual.next;
        } while (nodo_actual !== this.head);
        
        return false;
    }

    eliminar(data_a_eliminar) {
        if (this.head === null) {
            return;
        }

        if (this.head.data === data_a_eliminar) {
            if (this.head.next === this.head) {
                this.head = null;
                return;
            }
            
            let tail = this.head;
            while (tail.next !== this.head) {
                tail = tail.next;
            }
            
            this.head = this.head.next;
            tail.next = this.head;
            return;
        }

        let nodo_previo = this.head;
        let nodo_actual = this.head.next;
        while (nodo_actual !== this.head) {
            if (nodo_actual.data === data_a_eliminar) {
                nodo_previo.next = nodo_actual.next;
                return;
            }
            nodo_previo = nodo_actual;
            nodo_actual = nodo_actual.next;
        }
    }
}


const mi_lista = new ListaCircularEnlazada();
mi_lista.add_final(10);
mi_lista.add_final(20);
mi_lista.add_final(30);
mi_lista.print_lista();

mi_lista.add_inicio(5);
mi_lista.print_lista();

console.log("¿Está el 20? " + mi_lista.search(20));
console.log("¿Está el 99? " + mi_lista.search(99));

mi_lista.eliminar(20);
mi_lista.print_lista();

mi_lista.eliminar(5);
mi_lista.print_lista();

mi_lista.eliminar(30);
mi_lista.print_lista();