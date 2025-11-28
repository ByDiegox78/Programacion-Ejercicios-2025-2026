using System.Text.RegularExpressions;

namespace CuentaBancaria.Class;

public class CuentaBancaria {
    public int NumeroDeCuentaBancaria {
        get;
        set => field = !IsCuentaValida(value)
            ? throw new ArgumentException("El Numero de Cuenta no es valido.")
            : value;
    }

    public int Saldo {
        get; 
        set => field = value < 0 ? throw new ArgumentException("El Saldo de Cuenta no es valido.") : value ;
    } 
    
    
 //   public Titular titular = new Titular();
    
    private static bool IsCuentaValida(int cuenta) {
        var cuentaString = cuenta.ToString();
        var regex = new Regex("^[0-9]{10}"); // Solo verifica que tiene 24 numeros
        return regex.IsMatch(cuentaString); 
    }
    private  static bool IsSaldoValida(int saldo) {
        return saldo > 0;
    }

    public override string ToString() {
        return $"NºCuentaBancaria: {NumeroDeCuentaBancaria}, Saldo: {Saldo}";
    }
}
