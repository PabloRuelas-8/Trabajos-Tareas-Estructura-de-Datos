function partition(a, l, h) {
    const pvt = a[h];
    let j = l - 1;
    for (let k = l; k < h; k++) {
        if (a[k] < pvt) {
            j++;
            swap(a, j, k);
        }
    }
    swap(a, j + 1, h);
    return j + 1;
}

function swap(a, i, j) {
    [a[i], a[j]] = [a[j], a[i]];
}

function quickSort(a, l, h) {
    if (l < h) {
        const pi = partition(a, l, h);
        quickSort(a, l, pi - 1);
        quickSort(a, pi + 1, h);
    }
}

// Test
const a = [10, 7, 8, 9, 1, 5];
console.log("El arreglo antes de ordenarlo:");
console.log(a.join(" "));

quickSort(a, 0, a.length - 1);

console.log("El arreglo despues de ordenarlo:");
console.log(a.join(" "));
