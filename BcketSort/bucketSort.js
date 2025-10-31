
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

function bucketSort(inputArr) {
    const s = inputArr.length;
    
    let bucketArr = Array.from({ length: s }, () => []);

    for (const j of inputArr) {
        let bi = Math.floor(s * j);
        bucketArr[bi].push(j);
    }

    for (const bukt of bucketArr) {
        insertionSort(bukt); 
    }

    let idx = 0;
    for (const bukt of bucketArr) {
        for (const j of bukt) {
            inputArr[idx] = j;
            idx += 1;
        }
    }
}

// Ejemplo de uso
let test_array = [0.77, 0.16, 0.28, 0.25, 0.71, 0.93, 0.22, 0.11, 0.24, 0.67];

console.log("Arreglo antes de ordenar:");
console.log(test_array.join(" "));

bucketSort(test_array);

console.log("Arreglo despues de ordenar:");
console.log(test_array.join(" "));
