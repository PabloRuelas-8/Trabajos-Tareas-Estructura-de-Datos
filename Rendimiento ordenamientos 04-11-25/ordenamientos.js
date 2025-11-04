function bubbleSort(arr) {
    let n = arr.length;
    for (let i = 0; i < n - 1; i++) {
        for (let j = 0; j < n - 1 - i; j++) {
            if (arr[j] > arr[j + 1]) {
                [arr[j], arr[j + 1]] = [arr[j + 1], arr[j]];
            }
        }
    }
    return arr;
}

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
    return a;
}

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
    return a;
}

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

function quickSort_original(a, l, h) {
    if (l < h) {
        const pi = partition(a, l, h);
        quickSort_original(a, l, pi - 1);
        quickSort_original(a, pi + 1, h);
    }
}

function quickSort(arr) {
    quickSort_original(arr, 0, arr.length - 1);
    return arr;
}

function merge(a, l, m, r) {
    let a1 = m - l + 1;
    let a2 = r - m;

    let L = new Array(a1);
    let R = new Array(a2);

    for (let j = 0; j < a1; j++) {
        L[j] = a[l + j];
    }
    for (let k = 0; k < a2; k++) {
        R[k] = a[m + 1 + k];
    }

    let i = 0;
    let j = 0;
    let k = l;

    while (i < a1 && j < a2) {
        if (L[i] <= R[j]) {
            a[k] = L[i];
            i++;
        } else {
            a[k] = R[j];
            j++;
        }
        k++;
    }

    while (i < a1) {
        a[k] = L[i];
        i++;
        k++;
    }

    while (j < a2) {
        a[k] = R[j];
        j++;
        k++;
    }
}

function mergeSort_original(a, l, r) {
    if (l < r) {
        let m = Math.floor((l + (r - 1)) / 2);
        mergeSort_original(a, l, m);
        mergeSort_original(a, m + 1, r);
        merge(a, l, m, r);
    }
}

function mergeSort(arr) {
    mergeSort_original(arr, 0, arr.length - 1);
    return arr;
}

function heapify(arr, n, i) {
    let largest = i;
    const left = 2 * i + 1;
    const right = 2 * i + 2;

    if (left < n && arr[left] > arr[largest])
        largest = left;

    if (right < n && arr[right] > arr[largest])
        largest = right;

    if (largest !== i) {
        [arr[i], arr[largest]] = [arr[largest], arr[i]];
        heapify(arr, n, largest);
    }
}

function heapsort(arr) {
    const n = arr.length;

    for (let i = Math.floor(n / 2) - 1; i >= 0; i--)
        heapify(arr, n, i);

    for (let i = n - 1; i > 0; i--) {
        [arr[0], arr[i]] = [arr[i], arr[0]];
        heapify(arr, i, 0);
    }
    return arr;
}

class OrdenamientoShell {
    ordenar(arreglo) {
        let tamano = arreglo.length;
        let tamanoBrecha = Math.floor(tamano / 2);

        while (tamanoBrecha > 0) {
            for (let j = tamanoBrecha; j < tamano; j++) {
                let valor = arreglo[j];
                let k = j;

                while (k >= tamanoBrecha && arreglo[k - tamanoBrecha] > valor) {
                    arreglo[k] = arreglo[k - tamanoBrecha];
                    k = k - tamanoBrecha;
                }
                arreglo[k] = valor;
            }
            tamanoBrecha = Math.floor(tamanoBrecha / 2);
        }
    }
}

function hashSort(arr) {
    let obj = new OrdenamientoShell();
    obj.ordenar(arr);
    return arr;
}

function insertionSort_for_bucket(a) {
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

function bucketSort(arr) {
    if (arr.length === 0) return arr;

    let min = arr[0];
    let max = arr[0];
    for (let i = 1; i < arr.length; i++) {
        if (arr[i] < min) min = arr[i];
        if (arr[i] > max) max = arr[i];
    }

    const bucketSize = 5;
    const bucketCount = Math.floor((max - min) / bucketSize) + 1;
    const buckets = Array.from({ length: bucketCount }, () => []);

    for (let i = 0; i < arr.length; i++) {
        const bucketIndex = Math.floor((arr[i] - min) / bucketSize);
        buckets[bucketIndex].push(arr[i]);
    }

    const sortedArr = [];
    for (let i = 0; i < buckets.length; i++) {
        insertionSort_for_bucket(buckets[i]);
        sortedArr.push(...buckets[i]);
    }

    for (let i = 0; i < arr.length; i++) {
        arr[i] = sortedArr[i];
    }
    return arr;
}

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
    return arreglo;
}