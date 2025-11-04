class OrdenamientoShell {
    
    static mostrarArreglo(arreglo) {
        console.log(arreglo.join(" "));
    }

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


let arreglo = [36, 34, 43, 11, 15, 20, 28, 45];

console.log("Arreglo antes de ser ordenado:");
OrdenamientoShell.mostrarArreglo(arreglo);

let obj = new OrdenamientoShell();
obj.ordenar(arreglo);

console.log("Arreglo despues de ser ordenado:");
OrdenamientoShell.mostrarArreglo(arreglo);