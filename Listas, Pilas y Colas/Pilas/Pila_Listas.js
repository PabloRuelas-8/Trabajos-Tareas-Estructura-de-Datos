class Nodo {
    constructor(d) {
        this.data = d;
        this.next = null;
    }
}

class Pila_Listas {
    constructor() {
        this.top = null;
    }

    isEmpty() {
        return this.top === null;
    }

    push(data) {
        const nuevo = new Nodo(data);
        nuevo.next = this.top;
        this.top = nuevo;
    }

    pop() {
        if (this.isEmpty()) {
            console.log("Error: Underflow de Pila");
            return -1;
        }
        const dato = this.top.data;
        this.top = this.top.next;
        return dato;
    }

    peek() {
        if (this.isEmpty()) {
            console.log("La Pila esta vacia");
            return -1;
        }
        return this.top.data;
    }
}

// --- Ejecución (Simulación de main) ---

const pila = new Pila_Listas();
pila.push(10);
pila.push(20);
pila.push(30);

console.log("Elemento superior: " + pila.peek());
console.log("Extrae elemento: " + pila.pop());
console.log("Nuevo elemento superior: " + pila.peek());