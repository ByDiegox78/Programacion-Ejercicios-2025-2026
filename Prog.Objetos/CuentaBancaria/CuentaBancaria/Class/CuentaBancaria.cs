using System.Text.RegularExpressions;

namespace CuentaBancaria.Class;

public class CuentaBancaria {
    public long NumeroDeCuentaBancaria {
        get;
        set => field = !IsCuentaValida(value)
            ? throw new ArgumentException("El Numero de Cuenta no es valido.")
            : value;
    }
    public decimal Saldo {
        get; 
        set => field = value < 0.0m ? throw new ArgumentException("El Saldo de Cuenta no es valido.") : value ;
    }

    public Titular[] _titular { get; private set; }

    public CuentaBancaria(Titular[] titulares)
    {
        if (titulares == null)
            throw new ArgumentException("El array no puede ser null.");

        if (titulares.Length is < 1 or > 3)
            throw new ArgumentException("La cuenta debe tener entre 1 y 3 titulares.");

        // Validar que no vengan titulares nulos
        foreach (var t in titulares)
            if (t == null)
                throw new ArgumentException("Los titulares no pueden ser null.");

        _titular = titulares;
    }
    
    private static bool IsCuentaValida(long cuenta) {
        var cuentaString = cuenta.ToString();
        var regex = new Regex("^[0-9]{10}$"); // Solo verifica que tiene 24 numeros
        return regex.IsMatch(cuentaString); 
    }
    public override string ToString() {
        return $"NºCuentaBancaria: {NumeroDeCuentaBancaria}, Saldo: {Saldo}";
    }
}
