
document.addEventListener('DOMContentLoaded', () => {

    const btnIniciar = document.getElementById('btn-iniciar');
    const selectTamaño = document.getElementById('select-tamaño');
    const selectOrden = document.getElementById('select-orden');
    const canvasContext = document.getElementById('miGrafica').getContext('2d');
    const divTablaResultados = document.getElementById('tabla-resultados');
    
    let miGraficaChart;

    function generarArreglo(tamaño, tipoOrden) {
        let arreglo = [];
        for (let i = 0; i < tamaño; i++) {
            arreglo.push(Math.floor(Math.random() * tamaño) + 1);
        }

        if (tipoOrden === 'ordenado') {
            arreglo.sort((a, b) => a - b);
        }
        else if (tipoOrden === 'inverso') {
            arreglo.sort((a, b) => b - a);
        } 
        else if (tipoOrden === 'semidesordenado') {
            let mitad = Math.floor(tamaño / 2);
            let primeraMitad = arreglo.slice(0, mitad).sort((a, b) => a - b);
            let segundaMitad = arreglo.slice(mitad);
            arreglo = primeraMitad.concat(segundaMitad);
        }
        return arreglo;
    }

    function medirTiempo(funcionOrdenamiento, arreglo) {
        const t0 = performance.now();
        funcionOrdenamiento(arreglo); 
        const t1 = performance.now();
        return t1 - t0; 
    }

    btnIniciar.addEventListener('click', () => {
        btnIniciar.disabled = true; 
        btnIniciar.textContent = "Procesando...";

        const tamaño = parseInt(selectTamaño.value);
        const orden = selectOrden.value;

        const algoritmos = [
            { nombre: 'Bubble Sort', funcion: bubbleSort },
            { nombre: 'Insertion Sort', funcion: insertionSort },
            { nombre: 'Selection', funcion: selection }, 
            { nombre: 'Quick Sort', funcion: quickSort },
            { nombre: 'Merge Sort', funcion: mergeSort },
            { nombre: 'Heapsort', funcion: heapsort },
            { nombre: 'Hash Sort (Shell)', funcion: hashSort },
            { nombre: 'Bucket Sort', funcion: bucketSort },
            { nombre: 'Radix Sort', funcion: radixSort }
        ];

        const arregloBase = generarArreglo(tamaño, orden);
        let resultados = [];


        setTimeout(() => {
            for (const algo of algoritmos) {
                let copiaArreglo = arregloBase.slice();
                
                let tiempo = 0;

                if (tamaño > 20000 && (algo.nombre === 'Bubble Sort' || algo.nombre === 'Insertion Sort' || algo.nombre === 'Selection')) {
                    tiempo = -1;
                } else {
                    tiempo = medirTiempo(algo.funcion, copiaArreglo);
                }
                
                resultados.push({ nombre: algo.nombre, tiempo: tiempo });
            }

            resultados.sort((a, b) => a.tiempo - b.tiempo);

            actualizarGrafica(resultados, `Resultados (Tamaño: ${tamaño}, Orden: ${orden})`);
            actualizarTabla(resultados);

            btnIniciar.disabled = false;
            btnIniciar.textContent = "Iniciar Prueba!";

        }, 50); 
    });

    function actualizarGrafica(resultados, titulo) {
        if (miGraficaChart) {
            miGraficaChart.destroy();
        }

        const resultadosFiltrados = resultados.filter(r => r.tiempo >= 0);

        miGraficaChart = new Chart(canvasContext, {
            type: 'bar', 
            data: {
                labels: resultadosFiltrados.map(r => r.nombre),
                datasets: [{
                    label: 'Tiempo de Ejecución (ms)',
                    data: resultadosFiltrados.map(r => r.tiempo),
                    backgroundColor: 'rgba(54, 162, 235, 0.6)'
                }]
            },
            options: {
                responsive: true,
                plugins: {
                    title: { display: true, text: titulo, font: { size: 18 } }
                },
                scales: {
                    y: { beginAtZero: true, title: { display: true, text: 'Tiempo (ms)' } }
                }
            }
        });
    }
    
    function actualizarTabla(resultados) {
        let html = "<h2>Rendimiento</h2>";
        html += "<table>";
        html += "<tr><th>Posición</th><th>Algoritmo</th><th>Tiempo (ms)</th></tr>";
        
        let posicion = 1;
        for (const res of resultados) {
            html += "<tr>";
            if (res.tiempo === -1) {
                html += `<td>-</td>`;
                html += `<td class="omitido">${res.nombre}</td>`;
                html += `<td class="omitido">Omitido (muy grande)</td>`;
            } else {
                html += `<td>${posicion}º</td>`;
                html += `<td>${res.nombre}</td>`;
                html += `<td>${res.tiempo.toFixed(4)}</td>`;
                posicion++;
            }
            html += "</tr>";
        }
        
        html += "</table>";
        divTablaResultados.innerHTML = html;
    }
});