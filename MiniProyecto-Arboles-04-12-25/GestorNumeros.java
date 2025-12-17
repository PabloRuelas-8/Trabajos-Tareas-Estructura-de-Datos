import java.io.FileWriter;
import java.io.IOException;
import java.io.PrintWriter;
import java.util.Scanner;

public class GestorNumeros {

    static class Nodo {
        int valor;
        Nodo izquierdo;
        Nodo derecho;

        public Nodo(int valor) {
            this.valor = valor;
            this.izquierdo = null;
            this.derecho = null;
        }
    }

    static class ArbolBinarioBusqueda {
        private Nodo raiz;

        public ArbolBinarioBusqueda() {
            this.raiz = null;
        }

        //insertar 
        public void insertar(int valor) {
            raiz = insertarRec(raiz, valor);
        }

        private Nodo insertarRec(Nodo raiz, int valor) {
            if (raiz == null) {
                return new Nodo(valor);
            }
            if (valor < raiz.valor)
                raiz.izquierdo = insertarRec(raiz.izquierdo, valor);
            else if (valor > raiz.valor)
                raiz.derecho = insertarRec(raiz.derecho, valor);
            return raiz;
        }

        //buscar 
        public boolean buscar(int valor) {
            return buscarRec(raiz, valor);
        }

        private boolean buscarRec(Nodo raiz, int valor) {
            if (raiz == null) return false;
            
            System.out.print(raiz.valor + " -> "); //muestra ruta visual
            
            if (valor == raiz.valor) return true;
            return valor < raiz.valor 
                ? buscarRec(raiz.izquierdo, valor) 
                : buscarRec(raiz.derecho, valor);
        }

        //eliminar 
        public void eliminar(int valor) {
            raiz = eliminarRec(raiz, valor);
        }

        private Nodo eliminarRec(Nodo raiz, int valor) {
            if (raiz == null) return raiz;

            if (valor < raiz.valor)
                raiz.izquierdo = eliminarRec(raiz.izquierdo, valor);
            else if (valor > raiz.valor)
                raiz.derecho = eliminarRec(raiz.derecho, valor);
            else {
                //caso 1 - Hoja o un hijo 
                if (raiz.izquierdo == null) return raiz.derecho;
                else if (raiz.derecho == null) return raiz.izquierdo;

                //caso 2 - Dos hijos 
                raiz.valor = valorMinimo(raiz.derecho);
                raiz.derecho = eliminarRec(raiz.derecho, raiz.valor);
            }
            return raiz;
        }

        private int valorMinimo(Nodo raiz) {
            int minv = raiz.valor;
            while (raiz.izquierdo != null) {
                minv = raiz.izquierdo.valor;
                raiz = raiz.izquierdo;
            }
            return minv;
        }

        //recorridos 
        public void inorden() { inordenRec(raiz); System.out.println(); }
        private void inordenRec(Nodo raiz) {
            if (raiz != null) {
                inordenRec(raiz.izquierdo);
                System.out.print(raiz.valor + " ");
                inordenRec(raiz.derecho);
            }
        }

        public void preorden() { preordenRec(raiz); System.out.println(); }
        private void preordenRec(Nodo raiz) {
            if (raiz != null) {
                System.out.print(raiz.valor + " ");
                preordenRec(raiz.izquierdo);
                preordenRec(raiz.derecho);
            }
        }

        public void postorden() { postordenRec(raiz); System.out.println(); }
        private void postordenRec(Nodo raiz) {
            if (raiz != null) {
                postordenRec(raiz.izquierdo);
                postordenRec(raiz.derecho);
                System.out.print(raiz.valor + " ");
            }
        }

        //altura 
        public int obtenerAltura() { return obtenerAlturaRec(raiz); }
        private int obtenerAlturaRec(Nodo nodo) {
            if (nodo == null) return 0;
            return Math.max(obtenerAlturaRec(nodo.izquierdo), obtenerAlturaRec(nodo.derecho)) + 1;
        }

        //tamaño
        public int obtenerTamano() { return obtenerTamanoRec(raiz); }
        private int obtenerTamanoRec(Nodo nodo) {
            if (nodo == null) return 0;
            return 1 + obtenerTamanoRec(nodo.izquierdo) + obtenerTamanoRec(nodo.derecho);
        }

        //exportar a archivo 
        public void exportarInorden(String nombreArchivo) {
            try (PrintWriter escritor = new PrintWriter(new FileWriter(nombreArchivo))) {
                exportarInordenRec(raiz, escritor);
                System.out.println("Guardado en: " + nombreArchivo);
            } catch (IOException e) {
                System.out.println("Error al exportar: " + e.getMessage());
            }
        }
        private void exportarInordenRec(Nodo nodo, PrintWriter escritor) {
            if (nodo != null) {
                exportarInordenRec(nodo.izquierdo, escritor);
                escritor.print(nodo.valor + " ");
                exportarInordenRec(nodo.derecho, escritor);
            }
        }
    }

    public static void main(String[] args) {
        ArbolBinarioBusqueda arbol = new ArbolBinarioBusqueda();
        Scanner scanner = new Scanner(System.in);
        String comando = "";

        System.out.println("=== Gestor de Numeros (Arbol Binario) ===");
        System.out.println("Escribe 'help' pan  ra ver comandos.");

        while (!comando.equals("exit")) {
            System.out.print("\n> ");
            String entrada = scanner.nextLine().trim();
            if (entrada.isEmpty()) continue;
            
            String[] partes = entrada.split("\\s+");
            comando = partes[0].toLowerCase();

            try {
                switch (comando) {
                    case "insert": 
                        if (partes.length < 2) System.out.println("Falta el numero.");
                        else arbol.insertar(Integer.parseInt(partes[1]));
                        break;
                    case "search":
                        if (partes.length < 2) System.out.println("Falta el numero.");
                        else {
                            System.out.print("Ruta: ");
                            System.out.println(arbol.buscar(Integer.parseInt(partes[1])) ? " [ENCONTRADO]" : " [NO EXISTE]");
                        }
                        break;
                    case "delete": 
                        if (partes.length < 2) System.out.println("Error: Falta el número.");
                        else arbol.eliminar(Integer.parseInt(partes[1]));
                        break;
                    case "inorder":    arbol.inorden(); break;   
                    case "preorder":   arbol.preorden(); break; 
                    case "postorder":  arbol.postorden(); break; 
                    case "height":     System.out.println("Altura: " + arbol.obtenerAltura()); break;
                    case "size":       System.out.println("Nodos: " + arbol.obtenerTamano()); break;  
                    case "export": 
                        String archivo = (partes.length > 1) ? partes[1] : "arbol.txt";
                        arbol.exportarInorden(archivo);
                        break;
                    case "help": 
                        System.out.println("Comandos: insert <n>, search <n>, delete <n>, inorder, preorder, postorder, height, size, export <nombre>, exit");
                        break;
                    case "exit": 
                        System.out.println("Adios");
                        break;
                    default:
                        System.out.println("Comando desconocido");
                }
            } catch (NumberFormatException e) {
                System.out.println("Debes ingresar un numero validoa");
            } catch (Exception e) {
                System.out.println("Error: " + e.getMessage());
            }
        }
        scanner.close();
    }
}