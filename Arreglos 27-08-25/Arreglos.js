const readline = require('readline');

const rl = readline.createInterface({
  input: process.stdin,
  output: process.stdout
});

let numeros = [10, 20, 30, 40];

console.log("primer elemento:", numeros[0]);

numeros[2] = 45;

console.log("arreglo inicial:");
for (let n of numeros) {
  console.log(n);
}

rl.question("cuantos números?: ", (answer) => {
  let tamaño = parseInt(answer);
  let arr = [];
  let i = 0;

  function pedirNumero() {
    if (i < tamaño) {
      rl.question(`ingrese valor ${i + 1}: `, (valor) => {
        arr.push(parseInt(valor));
        i++;
        pedirNumero();
      });
    } else {
      console.log("valores ingresados:", arr);
      rl.close();
    }
  }

  pedirNumero();
});
