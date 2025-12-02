using System.Text.RegularExpressions;

namespace CuentaBancaria.Class;

public class CuentaBancaria {
    public long NumeroDeCuentaBancaria {
        get;
        set => field = !IsCuentaValida(value)
            ? throw new ArgumentException("El Numero de Cuenta no es valido.")
            : value;
    }
    public int Saldo {
        get; 
        set => field = value < 0 ? throw new ArgumentException("El Saldo de Cuenta no es valido.") : value ;
    }

    private Titular?[] _titular = new Titular?[3];
    
    private static bool IsCuentaValida(long cuenta) {
        var cuentaString = cuenta.ToString();
        var regex = new Regex("^[0-9]{10}$"); // Solo verifica que tiene 24 numeros
        return regex.IsMatch(cuentaString); 
    }
    public override string ToString() {
        return $"NºCuentaBancaria: {NumeroDeCuentaBancaria}, Saldo: {Saldo}";
    }
}
