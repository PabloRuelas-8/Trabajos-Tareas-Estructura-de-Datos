#include <iostream>
#include <string>
using namespace std;

struct Persona {
    string nombre;
    string apellido1;
    string apellido2;
};

int main() {
    Persona p;
    p.nombre = "Pablo";
    p.apellido1 = "Ruelas";
    p.apellido2 = "López";

    cout << p.nombre << " " << p.apellido1 << " " << p.apellido2 << endl;
    return 0;
}
