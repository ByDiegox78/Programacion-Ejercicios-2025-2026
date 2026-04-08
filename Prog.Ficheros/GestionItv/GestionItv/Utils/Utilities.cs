using System.Text.RegularExpressions;
using GestionItv.Config;
using GestionItv.Models;
using static System.Console;
namespace GestionItv.Utils;

public static class Utilities {
    public static bool ValidarDni(string dni) {
        if (!Configuracion.RegexDni.IsMatch(dni))
            return false;
        int numero = int.Parse(dni.Substring(0, 8));
        char letraCorrecta = Configuracion.LetrasDniPermitidas[numero % 23];
        return dni[8] == letraCorrecta;
    }
    
    public static string LeerMatricula() {
        while (true) {
            Write("🚗 Matrícula (1234ABC): ");
            var m = ReadLine()?.Trim().ToUpper() ?? "";
            if (Regex.IsMatch(m, @"^\d{4}[A-Z]{3}$")) return m;
            WriteLine("❌ ERROR: Formato de matrícula inválido.");
        }
    }
    public static string LeerMarca() {
        while (true) {
            Write("🏷️ Marca: ");
            var marca = ReadLine()?.Trim() ?? "";
            if (!string.IsNullOrWhiteSpace(marca)) return marca;
            WriteLine("❌ ERROR: La marca no puede estar vacía.");
        }
    }
    public static int LeerCilindrada() {
        while (true) {
            Write("⚙️ Cilindrada (>0): ");
            var input = ReadLine()?.Trim() ?? "";
            if (int.TryParse(input, out var c) && c > 0) return c;
            WriteLine("❌ ERROR: Cilindrada inválida.");
        }
    }
    public static Motor LeerMotor() {
        WriteLine("🔧 Tipo de motor: 1.Gasolina 2.Diesel 3.Hibrido 4.Electrico");
        var eleccion = LeerCadenaValida("🎯 Elija tipo: ", @"^[1-4]$", "Seleccione entre 1 y 4.");
        return (Motor)(int.Parse(eleccion) - 1);
    }
    public static string LeerCadenaValida(string prompt, string regex, string mensajeError) {
        while (true) {
            Write(prompt);
            var input = ReadLine()?.Trim() ?? "";
            if (Regex.IsMatch(input, regex))
                return input;
            WriteLine($"❌ ERROR: {mensajeError}");
        }
    }
}