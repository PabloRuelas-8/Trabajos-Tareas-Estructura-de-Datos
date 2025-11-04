function obtenerMaximo(arreglo) {
    return Math.max(...arreglo);
}

function ordenamientoPorConteo(arreglo, exponente) {
    let tamano = arreglo.length;
    let arregloSalida = new Array(tamano).fill(0);
    let arregloConteo = new Array(10).fill(0);

    for (let j = 0; j < tamano; j++) {
        let indice = Math.floor(arreglo[j] / exponente);
        arregloConteo[indice % 10]++;
    }

    for (let j = 1; j < 10; j++) {
        arregloConteo[j] += arregloConteo[j - 1];
    }

    for (let j = tamano - 1; j >= 0; j--) {
        let indice = Math.floor(arreglo[j] / exponente);
        arregloSalida[arregloConteo[indice % 10] - 1] = arreglo[j];
        arregloConteo[indice % 10]--;
    }

    
    for (let j = 0; j < tamano; j++) {
        arreglo[j] = arregloSalida[j];
    }
}

function radixSort(arreglo) {
    let maximo = obtenerMaximo(arreglo);

    for (let exponente = 1; Math.floor(maximo / exponente) > 0; exponente *= 10) {
        ordenamientoPorConteo(arreglo, exponente);
    }
}

let arr = [171, 46, 76, 91, 803, 25, 3, 67];

console.log("Arreglo antes de ordenar:");
console.log(arr.join(" "));

radixSort(arr);

console.log("Arreglo despues de ordenar:");
console.log(arr.join(" "));