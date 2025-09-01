class Persona {
  constructor(nombre, apellido1, apellido2) {
    this.nombre = nombre;
    this.apellido1 = apellido1;
    this.apellido2 = apellido2;
  }
}

// Uso
let p = new Persona("Pablo", "Ruelas", "López");
console.log(p.nombre, p.apellido1, p.apellido2);
