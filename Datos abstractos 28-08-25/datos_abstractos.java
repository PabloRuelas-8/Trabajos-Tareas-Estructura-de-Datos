public class Persona {
    String nombre;
    String apellido1;
    String apellido2;

    public static void main(String[] args) {
        Persona p = new Persona();
        p.nombre = "Pablo";
        p.apellido1 = "Ruelas";
        p.apellido2 = "Lopez";

        System.out.println(p.nombre + " " + p.apellido1 + " " + p.apellido2);
    }
}
