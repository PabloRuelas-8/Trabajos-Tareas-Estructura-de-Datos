function selection(a) {
    for (let i = 0; i < a.length; i++) {
        let small = i;
        for (let j = i + 1; j < a.length; j++) {
            if (a[small] > a[j]) {
                small = j;
            }
        }
        let temp = a[i];
        a[i] = a[small];
        a[small] = temp;
    }
}

function printArr(a) {
    console.log(a.join(" "));
}

let a = [66, 11, 88, 44, 99, 33, 55, 22];

console.log("Arreglo antes de ser ordenado:");
printArr(a);

selection(a);

console.log("Arreglo despues de ser ordenado:");
printArr(a);
