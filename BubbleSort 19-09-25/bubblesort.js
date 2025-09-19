let arreglo = [44, 66, 88, 55, 33, 11, 22, 77];

console.log("Arreglo original:");
console.log(arreglo.join(" "));

for (let i = 0; i < arreglo.length - 1; i++) {
    for (let j = 0; j < arreglo.length - 1 - i; j++) {
        if (arreglo[j] > arreglo[j + 1]) {
            let aux = arreglo[j];
            arreglo[j] = arreglo[j + 1];
            arreglo[j + 1] = aux;
        }
    }
}

console.log("Arreglo ordenado de menor a mayor:");
console.log(arreglo.join(" "));
