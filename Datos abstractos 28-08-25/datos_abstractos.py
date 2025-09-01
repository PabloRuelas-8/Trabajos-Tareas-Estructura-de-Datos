class Persona:
    def __init__(self, nombre, apellido1, apellido2):
        self.nombre = nombre
        self.apellido1 = apellido1
        self.apellido2 = apellido2

# Uso
p = Persona("Pablo", "Ruelas", "López")
print(p.nombre, p.apellido1, p.apellido2)