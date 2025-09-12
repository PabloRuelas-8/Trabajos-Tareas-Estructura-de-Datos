const readline = require("readline");

const rl = readline.createInterface({
  input: process.stdin,
  output: process.stdout
});

let tablero = ["1", "2", "3", "4", "5", "6", "7", "8", "9"];
let jugadorActual = "X";

function imprimir3x3() {
  console.log(`
   ${tablero[0]} | ${tablero[1]} | ${tablero[2]}
  ---+---+---
   ${tablero[3]} | ${tablero[4]} | ${tablero[5]}
  ---+---+---
   ${tablero[6]} | ${tablero[7]} | ${tablero[8]}
  `);
}

function verificarGanador() {
  const winConditions = [
    [0, 1, 2], [3, 4, 5], [6, 7, 8], //filas             // 0 1 2
    [0, 3, 6], [1, 4, 7], [2, 5, 8], //columnas          // 3 4 5
    [0, 4, 8], [2, 4, 6]             //diagonales        // 6 7 8  
  ];

  return winConditions.some(([a, b, c]) =>
    tablero[a] === jugadorActual &&
    tablero[b] === jugadorActual &&
    tablero[c] === jugadorActual
  );
}

function verificarEmpate() {
  return tablero.every(cell => cell === "X" || cell === "O");
}

function turnoJugador() {
  imprimir3x3();
  rl.question(`Jugador ${jugadorActual}, elige una casilla (1-9): `, (answ) => {
    const movimiento = parseInt(answ) - 1;

    if (movimiento < 0 || movimiento > 8 || tablero[movimiento] === "X" || tablero[movimiento] === "O") {
      console.log("Error, da un movimiento valido.");
      return turnoJugador();
    }

    tablero[movimiento] = jugadorActual;

    if (verificarGanador()) {
      imprimir3x3();
      console.log(`Jugador ${jugadorActual} gana!`);
      return rl.close();
    } else if (verificarEmpate()) {
      imprimir3x3();
      console.log("Es un empate!");
      return rl.close();
    }

    jugadorActual = jugadorActual === "X" ? "O" : "X";
    turnoJugador();
  });
}

console.log("Gatito Miniproyecto");
turnoJugador();