using System.Text.RegularExpressions;
using FunkoPop.Enums;
using FunkoPop.Models;

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
    
    public static void ImprimirInfoFunko(Funko funko) {
        Console.WriteLine("-----------------------------------");
        Console.WriteLine($"👤 ID: {funko.Id}");
        Console.WriteLine($"💳 DNI: {funko.Nombre}");
        Console.WriteLine($"📝 Nombre: {funko.Precio}");
        Console.WriteLine($"💯 Nota: {funko.Categoria}");
        Console.WriteLine("-----------------------------------");
    }


    
}