//revisado
let arr = [1,2,3,4,5,1,2,3,4,5]
let arr_sin_duplicados = []

for (let i = 0; i < arr.length; i++) {
    let duplicado = false
    for (let j = 0; j < arr_sin_duplicados.length; j++) {
        if (arr[i] == arr_sin_duplicados[j]) {
            duplicado = true
            break
        }
    }
    if (!duplicado) {
        arr_sin_duplicados.push(arr[i])
    }
}

console.log("Arreglo con duplicados: " + arr);
console.log("Arreglo sin duplicados: " + arr_sin_duplicados);

