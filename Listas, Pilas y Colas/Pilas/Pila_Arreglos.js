class Pila_Arreglos {
    constructor(tam) {
        this.max_size = tam;
        this.stack = new Array(this.max_size);
        this.top = -1;
    }

    isEmpty() {
        return this.top === -1;
    }

    isFull() {
        return this.top === this.max_size - 1;
    }

    push(data) {
        if (this.isFull()) {
            console.log("Error: Overflow de Pila");
            return;
        }
        this.stack[++this.top] = data;
    }

    pop() {
        if (this.isEmpty()) {
            console.log("Error: Underflow de Pila");
            return -1;
        }
        return this.stack[this.top--];
    }

    peek() {
        if (this.isEmpty()) {
            console.log("La pila está vacía");
            return -1;
        }
        return this.stack[this.top];
    }
}


const pila = new Pila_Arreglos(100);
pila.push(10);
pila.push(20);
pila.push(30);

console.log("Elemento superior(TOP): " + pila.peek());
console.log("Extrae elemento: " + pila.pop());
console.log("Nuevo elemento superior: " + pila.peek());