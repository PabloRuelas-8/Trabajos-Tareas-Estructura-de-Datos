//revisado
let matriz = []
for (let i = 0; i < 3; i++) {
    matriz[i] = []; 
    for (let j = 0; j < 3; j++) {
        matriz[i][j] = Math.floor(Math.random() * 100);
    }
}
console.log("Matriz antes de ordenar por filas: ");

for (let i = 0; i < matriz.length; i++) {
    console.log("Fila " + (i + 1) + ": " + matriz[i]);
}

//ordenar bubblesort

for (let f = 0; f < matriz.length; f++) { 
    for (let i = 0; i < matriz[f].length - 1; i++) {
        for (let j = 0; j < matriz[f].length - 1 - i; j++) {
            if (matriz[f][j] > matriz[f][j + 1]) {
                let aux = matriz[f][j];
                matriz[f][j] = matriz[f][j + 1];
                matriz[f][j + 1] = aux;
            }
        }
    }
}


console.log("\nMatriz despues de ordenar por filas: ");
for (let i = 0; i < matriz.length; i++) {
    console.log("Fila " + (i + 1) + ": " + matriz[i]);

}