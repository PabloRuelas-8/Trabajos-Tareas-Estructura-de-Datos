let matriz = [
    [1, 2, 3],
    [4, 5, 6],
    [7, 8, 9]
];

console.log("Matriz original:");
for (let i = 0; i < matriz.length; i++) {
    console.log("[ " + matriz[i].join(" ") + " ]");
}

console.log("\nElementos en filas:");
for (let i = 0; i < matriz.length; i++) {
    for (let j = 0; j < matriz[i].length; j++) {
        process.stdout.write(matriz[i][j] + " ");
    }
}

console.log("\nElementos en columnas:");
for (let j = 0; j < matriz[0].length; j++) {
    for (let i = 0; i < matriz.length; i++) {
        process.stdout.write(matriz[i][j] + " ");
    }
}
