//revisado
let matriz = []

for (let i = 0; i < 3; i++) {
    let capa = [];
    for (let j = 0; j < 3; j++) {
        let fila = [];
        for (let k = 0; k < 3; k++) {
            fila.push(Math.floor(Math.random() * 100)); 
        }
        capa.push(fila);
    }
    matriz.push(capa);
}


    console.log("Matriz por capas:");
    for (let i = 0; i < matriz.length; i++) {
        console.log("Capa " + (i + 1) + ":");
        for (let j = 0; j < matriz[i].length; j++) {
            console.log(matriz[i][j]);
        }
    }

console.log("-------------------------------------------------");

let promedios_capas = []

for (let i = 0; i < matriz.length; i++) {
    let suma = 0;
    let contador = 0;
    for (let j = 0; j < matriz[i].length; j++) {
        for (let k = 0; k < matriz[i][j].length; k++) {
            suma += matriz[i][j][k];
            contador++;
        }
    }
    promedios_capas.push(suma / contador);
}

console.log("Promedio por capas:");
for (let i = 0; i < promedios_capas.length; i++) {
    console.log("Capa " + (i + 1) + ": " + promedios_capas[i].toFixed(2));
}

