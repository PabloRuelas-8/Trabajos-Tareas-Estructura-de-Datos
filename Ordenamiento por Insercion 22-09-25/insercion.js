function insertionSort(a) {
    for (let i = 1; i < a.length; i++) {
        let temp = a[i];
        let j = i - 1;
        while (j >= 0 && temp < a[j]) {
            a[j + 1] = a[j];
            j--;
        }
        a[j + 1] = temp;
    }
}

function printArr(a) {
    console.log(a.join(" "));
}

let a = [33, 55, 77, 22, 88, 11, 99, 44, 66];

console.log("Antes de ordenar los elementos del arreglo:");
printArr(a);

insertionSort(a);

console.log("Despues de ordenar los elementos del arreglo:");
printArr(a);
