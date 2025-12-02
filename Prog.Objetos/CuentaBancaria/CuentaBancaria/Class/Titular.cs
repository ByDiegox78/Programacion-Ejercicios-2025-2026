using System.Text.RegularExpressions;

namespace CuentaBancaria.Class;

public class Titular
{
    public string Dni {
        get;
        set => field = !IsDniValido(value) ? throw new ArgumentException("El Dni no es valido.") : value;
    } = string.Empty;

    public string Nombre {
        get; 
        set => field = !IsNombreValido(value) ? throw new ArgumentException("El nombre no es valido.") : value;
    } = string.Empty;

    public string Apellido {
        get; 
        set => field = !IsNombreValido(value) ? throw new ArgumentException("El apellido no es valido.") : value;
        
    } = string.Empty;

    public int Telefono {
        get;
        set => field = !IsNumaroEspañolValido(value) ? throw new ArgumentException("El apellido no es valido.") : value;
    }
    
    private static bool IsDniValido(string dni) {
        var regex = new Regex("^[0-9]{8}[TRWAGMYFPDXBNJZSQVHLCKE]{1}$"); // Por ahora solo se verifica el patron: NNNNNNNNL
        return regex.IsMatch(dni); 
    }
    
    private static bool IsNombreValido(string nombre) {
        var regex = new Regex("^[a-zA-Z]{3,}$"); 
        return regex.IsMatch(nombre); 
    }
    private static bool IsNumaroEspañolValido(int numero) {
        var numeroString = Convert.ToString(numero);
        var regex = new Regex("^[6]{1}[0-9]{8}$"); 
        return regex.IsMatch(numeroString); 
    }

    public override string ToString() {
        return $"Nombre: {Nombre}, Apellido: {Apellido}, DNI: {Dni}, Telefono: {Telefono}";
    }

}


