using System;

class Persona {
    public string Nombre;
    public string Apellido1;
    public string Apellido2;
}

class Program {
    static void Main() {
        Persona p = new Persona();
        p.Nombre = "Pablo";
        p.Apellido1 = "Ruelas";
        p.Apellido2 = "López";

        Console.WriteLine($"{p.Nombre} {p.Apellido1} {p.Apellido2}");
    }
}
