using System.Text.RegularExpressions;
using Serilog;
using static System.Console;

namespace CuentaBancaria.Validators;

public class ValidatorCuenta {
    
    public long ValidateNumeroCuenta(string msg) {
        WriteLine(msg);
        var regex = new Regex("^[0-9]{10}$");
        var input = (ReadLine() ?? "").Trim();
        while (!regex.IsMatch(input)) {
            Log.Warning("Error: Entrada inválida. Formato correcto: 10 dígitos numéricos (Ej: 1234567890)");
            input = ReadLine()?.Trim() ?? "";
        }
        return long.Parse(input);
    }
    public decimal ValidateSaldo(string msg) {
        WriteLine(msg);
        bool flag = false;
        decimal input;
        do {
            var line = ReadLine()?.Trim() ?? "";
            if (decimal.TryParse(line, out input) && input is >= 0m and <= 10000000m) {
                flag = true; // Entrada correcta
            } else {
                Log.Warning("Error: Entrada inválida. Introduce un número entre 0 y 10.000.000.");
            }
        } while (!flag);
        return input;
    }
    
    public string ValidateNombre(string msg) {
        WriteLine(msg);
        var regex = new Regex("^[a-zA-Z]{3,}$");
        var input = (ReadLine() ?? "").Trim();
        while (!regex.IsMatch(input)) {
            Log.Warning("Error: Entrada inválida. Debe tener al menos 3 letras y solo letras.");
            input = ReadLine()?.Trim() ?? "";
            
        }
        return input;
    }
    public string ValidateApellido(string msg) {
        WriteLine(msg);
        var regex = new Regex("^[a-zA-Z]{3,}$");
        var input = (ReadLine() ?? "").Trim();
        while (!regex.IsMatch(input)) {
            Log.Warning("Error: Entrada inválida. Debe tener al menos 3 letras y solo letras.");
            input = ReadLine()?.Trim() ?? "";
        }
        return input;
    }
    
    public string ValidateDni(string msg) {
        WriteLine(msg);
        var regex = new Regex("^[0-9]{8}[TRWAGMYFPDXBNJZSQVHLCKE]{1}$");
        var input = (ReadLine() ?? "").Trim();
        while (!regex.IsMatch(input)) {
            Log.Warning("Error: Entrada inválida. Debe tener el formato NNNNNNNNL");
            input = ReadLine()?.Trim() ?? "";
        }
        return input;
    }
    
    public int ValidateTelefono(string msg) {
        WriteLine(msg);
        var regex = new Regex("^[6]{1}[0-9]{8}$");
        var input = (ReadLine() ?? "").Trim();
        while (!regex.IsMatch(input)) {
            Log.Warning("Error: Entrada inválida. Debe tener el formato 6NNNNNNNN");
            input = ReadLine()?.Trim() ?? "";
        }
        return int.Parse(input);
    }
    
    
    
}