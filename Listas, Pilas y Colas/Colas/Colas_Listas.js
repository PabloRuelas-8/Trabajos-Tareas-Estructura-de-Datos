class Nodo {
    constructor(data) {
        this.data = data;
        this.next = null;
    }
}

class ColaLista {
    constructor() {
        this.front = null;
        this.rear = null;
    }

    isEmpty() {
        return this.front === null;
    }

    enqueue(data) {
        const nuevoNodo = new Nodo(data);

        if (this.rear === null) {
            this.front = nuevoNodo;
            this.rear = nuevoNodo;
            return;
        }

        this.rear.next = nuevoNodo;
        this.rear = nuevoNodo;
    }

    dequeue() {
        if (this.isEmpty()) {
            console.log("Error: Cola subdesbordada (Underflow)");
            return -1;
        }

        const data = this.front.data;
        this.front = this.front.next;

        if (this.front === null) {
            this.rear = null;
        }

        return data;
    }

    peek() {
        if (this.isEmpty()) {
            console.log("Cola vacía");
            return -1;
        }
        return this.front.data;
    }
}

// --- Ejecución (Simulación de main) ---

const cola = new ColaLista();

cola.enqueue(10);
cola.enqueue(20);
cola.enqueue(30);

console.log("Elemento frontal: " + cola.peek());
console.log("Elimina elemento: " + cola.dequeue());
console.log("Nuevo elemento frontal: " + cola.peek());