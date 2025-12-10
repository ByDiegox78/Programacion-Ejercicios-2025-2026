using System.Text.RegularExpressions;
using FunkoPop.Config;
using FunkoPop.Enums;
using FunkoPop.Models;
using FunkoPop.Validator;
using Serilog;

namespace FunkoPop.Utils;

public static class Utilities {
    public static string ValidarMenu(string msg, string rgx) {
        string input;
        var regex = new Regex(rgx);
        do {
            Console.Write($"{msg} ");
            input = Console.ReadLine()?.Trim() ?? "-1";
        } while (!regex.IsMatch(input));
        Console.WriteLine();
        return input;
    }
    public static int ValidarId(string msg, string rgx) {
        string input;
        var regex = new Regex(rgx);
        do {
            Console.Write($"{msg} ");
            input = Console.ReadLine()?.Trim() ?? "-1";
        } while (!regex.IsMatch(input));
        Console.WriteLine();
        return int.Parse(input);
    }
    
    public static Funko.CategoriaFunko ValidarTipo(string msg) {
        Funko.CategoriaFunko tipo;
        string input;
        bool valido = false;

        do {
            Console.Write($"{msg} ");
            
            input = Console.ReadLine()?.Trim() ?? "-1";
            if (Enum.TryParse(input, out tipo)) {
                valido = true;
            } else {
               
                Console.WriteLine($"Tipo inválida");
            }
        } while (!valido);

        return tipo;
    }
    public static decimal PedirPrecio(string mensaje) {
        decimal precio;
        string input;
        bool valido = false;

        do {
            Console.Write($"{mensaje} ");
            input = Console.ReadLine()?.Trim() ?? "-1";
            
            if (decimal.TryParse(input, out precio)) {

                if (precio >= Configuracion.PrecioMinimo) {
                    valido = true;
                } else {
                    Console.WriteLine($"El precio debe ser igual o superior a {Configuracion.PrecioMinimo}€.");
                }
            } else {
                Console.WriteLine("❌ Formato de precio inválido. Por favor, introduce un número decimal.");
            }
        
        } while (!valido);

        return precio;
    }

    
    public static string ValidarNombre(string msg, string rgx) {
        string input;
        var regex = new Regex(rgx);
        do {
            Console.Write($"{msg} ");
            input = Console.ReadLine()?.Trim() ?? "-1";
        } while (!regex.IsMatch(input));
        Console.WriteLine();
        return input;
    }
    
    public static void ImprimirListado(Funko[] f) {
        Console.WriteLine("---------------------------------------------------------");
        Console.WriteLine($"{"ID",-4} {"Nombre",-25} {"Precio",-10} {"Tipo",-15}");
        Console.WriteLine("---------------------------------------------------------");

        for (var i = 0; i < f.Length; i++) {
            var funko = f[i];
            Console.WriteLine(
                $"{funko.Id,-4} {funko.Nombre,-25} {funko.Precio,-10}{funko.Categoria,-15}");
        }

        Console.WriteLine("---------------------------------------------------------");
    }
    public static void ImprimirMenu() {
        Console.WriteLine();
        Console.WriteLine("===== MENÚ FUNKO POP =====");
        Console.WriteLine("0 - Salir");
        Console.WriteLine("1 - Mostrar todos");
        Console.WriteLine("2 - Buscar por ID");
        Console.WriteLine("3 - Añadir nuevo");
        Console.WriteLine("4 - Actualizar existente");
        Console.WriteLine("5 - Eliminar funko");
        Console.WriteLine("6 - Ordenar por nombre ASC");
        Console.WriteLine("7 - Ordenar por nombre DESC");
        Console.WriteLine("8 - Ordenar por precio ASC");
        Console.WriteLine("9 - Ordenar por precio DESC");
        Console.WriteLine("===========================");
        Console.WriteLine();
    }
    public static void ImprimirMenuCambio() {
        Console.WriteLine();
        Console.WriteLine("===== MENÚ FUNKO POP =====");
        Console.WriteLine("0 - Salir");
        Console.WriteLine("1 - Cambiar Nombre");
        Console.WriteLine("2 - Cambiar Precio");
        Console.WriteLine("3 - Cambiar Tipo");
        Console.WriteLine("===========================");
        Console.WriteLine();
    }
    
    public static void ImprimirInfoFunko(Funko funko) {
        Console.WriteLine("-----------------------------------");
        Console.WriteLine($"👤 ID: {funko.Id}");
        Console.WriteLine($"💳 DNI: {funko.Nombre}");
        Console.WriteLine($"📝 Nombre: {funko.Precio}");
        Console.WriteLine($"💯 Nota: {funko.Categoria}");
        Console.WriteLine("-----------------------------------");
    }
    
    public static bool PedirConfirmacion(string mensaje) {
        Console.WriteLine("\n-----------------------------------");
        Console.WriteLine($"⚠️ {mensaje} Se requiere confirmación.");
        Console.Write("Presione 'S' para confirmar, o cualquier otra tecla para cancelar: ");
        
        var key = Console.ReadKey(true).KeyChar;
        
        if (char.ToUpper(key) == 'S') {
            Console.WriteLine("✅ Operación CONFIRMADA.");
            return true;
        }

        Console.WriteLine("🚫 Operación CANCELADA por el usuario.");
        return false;
    }

    public static Funko UpdatedFunkoCreation(Funko oldFunko) {
        var nombre = oldFunko.Nombre;
        Funko.CategoriaFunko tipo = oldFunko.Categoria;
        var precio = oldFunko.Precio;
        Cambio cambio;

        if (Utilities.PedirConfirmacion("¿Quiere cambiar el nombre del Funko? (Presiona s)")) {
            do {
                Utilities.ImprimirMenuCambio();
                cambio = (Cambio)int.Parse(Utilities.ValidarMenu("--- Elije una opcion ---", FunkoValidator.RegexMenu));
                switch (cambio) {
                    case Cambio.Nombre:
                        nombre = Utilities.ValidarNombre("Introduce el nombre del Funko", FunkoValidator.RegexNombreApellido);
                        break;
                    case Cambio.Precio:
                        precio = Utilities.PedirPrecio($"Introduce un precio mayor a: {Configuracion.PrecioMinimo}€");
                        break;
                    case Cambio.Tipo:
                        tipo = Utilities.ValidarTipo("Untroduce un tipo valido: Anime|Superheroe|Disney");
                        break;
                    case Cambio.Salir:
                        break;
                
                
                } 
            } while (cambio != Cambio.Salir);
        }
        var updatedFunko = new Funko {
            Id = oldFunko.Id,
            Nombre = nombre,
            Categoria = tipo,
            Precio = precio
        };
        return updatedFunko;
    } 


    
}