const readline = require("readline");

function imprimir(arre) {
    console.log("El arreglo es:", arre.join(" "));
}

function insertar_elemento(arre, val, indice) {
    for (let i = arre.length - 1; i > indice; i--) {
        arre[i] = arre[i - 1];
    }
    arre[indice] = val;
}

function busqueda_lineal(arre, obj) {
    for (let i = 0; i < arre.length; i++) {
        if (arre[i] === obj) {
            return i;
        }
    }
    return -1;
}

let arre = [1, 2, 3, 4, 5];
imprimir(arre);

const rl = readline.createInterface({
    input: process.stdin,
    output: process.stdout
});

rl.question("Valor para insertar: ", (val) => {
    val = parseInt(val);
    rl.question(`Indice donde se va a insertar (0..${arre.length - 1}): `, (indice) => {
        indice = parseInt(indice);

        insertar_elemento(arre, val, indice);
        imprimir(arre);

        let obj = val;
        let pos = busqueda_lineal(arre, obj);
        console.log(`Busqueda lineal de ${obj}: indice ${pos}`);

        rl.question("\nPresiona enter para salir", () => rl.close());
    });
});

