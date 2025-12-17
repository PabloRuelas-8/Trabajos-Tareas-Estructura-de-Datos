const MAXSIZE = 5;
const queue = new Array(MAXSIZE).fill(0);
let front = -1;
let rear = -1;

function insertar() {
    const prompt = require('prompt-sync')({sigint: true});
    let elemento;
    
    process.stdout.write("\nIngrese el elemento que se va a insertar ");
    const input = prompt('');

    if (isNaN(parseInt(input))) {
        console.log("\nEntrada inválida.");
        return;
    }
    elemento = parseInt(input);

    if (rear === MAXSIZE - 1) {
        console.log("\nDESBORDAMIENTO (OVERFLOW)\n");
        return;
    }
    if (front === -1 && rear === -1) {
        front = rear = 0;
    } else {
        rear++;
    }

    queue[rear] = elemento;
    console.log("\nElemento insertado correctamente.\n");
}

function eliminar() {
    if (front === -1 || front > rear) {
        console.log("\nSUBDESBORDAMIENTO (UNDERFLOW)\n");
        return;
    }

    const elemento = queue[front];
    if (front === rear) {
        front = rear = -1;
    } else {
        front++;
    }

    console.log(`\nElemento correctamente eliminado: ${elemento}\n`);
}

function mostrar() {
    if (rear === -1 || front === -1 || front > rear) {
        console.log("\nLa cola está vacía.\n");
    } else {
        console.log("\nElementos en la cola:");
        for (let i = front; i <= rear; i++) {
            console.log(queue[i]);
        }
    }
}

function main() {
    const prompt = require('prompt-sync')({sigint: true});
    let opcion = 0;

    while (opcion !== 4) {
        console.log("\n========== MENÚ PRINCIPAL ==========");
        console.log("1. Insertar un elemento");
        console.log("2. Eliminar un elemento");
        console.log("3. Mostrar la cola");
        console.log("4. Salir");
        process.stdout.write("Ingrese su opción: ");
        
        const input = prompt('');
        opcion = parseInt(input);

        if (isNaN(opcion)) {
            console.log("\nOpción inválida.\n");
            opcion = 0;
            continue;
        }

        switch (opcion) {
            case 1: insertar(); break;
            case 2: eliminar(); break;
            case 3: mostrar(); break;
            case 4: console.log("\nCerrando...\n"); break;
            default: console.log("\nOpción inválida.\n"); break;
        }
    }
}

main();