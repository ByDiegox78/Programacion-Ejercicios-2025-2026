    using System.Text.RegularExpressions;
    using GestionItv.Config;
    using GestionItv.Models;
    using static System.Console;
    namespace GestionItv.Utils;

    public static class Utilities {
        public static string LeerDniValido() {
            while (true) {
                WriteLine("🪪 Introduzca DNI (8 números y letra): ");
                var dni = ReadLine()?.Trim().ToUpper() ?? "";
                if (ValidarDni(dni)) {
                    return dni; 
                }
                WriteLine("ERROR: El DNI no es válido (formato incorrecto o letra no corresponde).");
            }
        }

        public static bool ValidarDni(string dni) {
            if (string.IsNullOrEmpty(dni) || !Configuracion.RegexDni.IsMatch(dni))
                return false;
            int numero = int.Parse(dni.Substring(0, 8));
            char letraCorrecta = Configuracion.LetrasDniPermitidas[numero % 23];
            return dni[8] == letraCorrecta;
        }
        
        public static string LeerMatricula() {
            while (true) {
                Write("🚗 Matrícula (1234ABC): ");
                var m = ReadLine()?.Trim().ToUpper() ?? "";
                // Regex corregido: 4 números y 3 letras válidas
                if (Regex.IsMatch(m, @"^[0-9]{4}[BCDFGHJKLMNPRSTVWXYZ]{3}$")) return m;
                WriteLine("❌ ERROR: Formato de matrícula inválido (Ejemplo: 1234BCD).");
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