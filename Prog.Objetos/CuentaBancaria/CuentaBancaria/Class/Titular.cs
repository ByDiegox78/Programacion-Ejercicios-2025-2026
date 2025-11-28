using System.Text.RegularExpressions;

namespace CuentaBancaria.Class;

public class Titular {
    public string Dni {
        get; 
        set => field = !IsDniValido(value) ? throw new ArgumentException("El Dni no es valido.") : value;
    }

    public string Nombre {
        get; 
        set => field = !IsNombreValido(value) ? throw new ArgumentException("El nombre no es valido.") : value;
    }
    public string Apellido { get; set; }
    public string Telefono { get; set; }
    
    private static bool IsDniValido(string dni) {
        var regex = new Regex("^[0-9]{8}[TRWAGMYFPDXBNJZSQVHLCKE]{1}"); // Por ahora solo se verifica el patron: NNNNNNNNL
        return regex.IsMatch(dni); 
    }
    
    private static bool IsNombreValido(string nombre) {
        var regex = new Regex("^[a-zA-Z]{3,}"); // Por ahora solo se verifica el patron: NNNNNNNNL
        return regex.IsMatch(nombre); 
    }
}


