using CuentaBancaria.Validators;
using Serilog;

namespace CuentaBancaria.Service;
using Class;
using static Console;
public class ServiceCuenta {
    private CuentaBancaria?[] _banco = new CuentaBancaria?[5];
    private readonly ValidatorCuenta _validate = new ValidatorCuenta();

    public void CrearCuenta() {
        WriteLine("=== CREAR NUEVA CUENTA BANCARIA ===");
        
        var numeroCuenta = _validate.ValidateNumeroCuenta("Introduce el número de cuenta:");
        
        var saldo = _validate.ValidateSaldo("Introduce el saldo inicial: ");
        
        var titular = CrearTitular();
        var cuenta = new CuentaBancaria {
            NumeroDeCuentaBancaria = numeroCuenta,
            Saldo = saldo,
            Titular = [titular, null, null]
        };
        bool guardada = false;
        for (int i = 0; i < _banco.Length; i++) {
            if (_banco[i] != null) continue;
            _banco[i] = cuenta;
            guardada = true; 
            Log.Information($"Cuenta guardada en la posición {i}.");
            break;
        }
        if (!guardada) {
            Log.Error("Error: El banco está lleno.");
        }
        else {
            WriteLine(cuenta);
        }
    }

    public void IngresarDinero() {
        if (BancoVacio()) {
            Log.Error("El banco está vacío. Necesitas crear una cuenta primero...");
            return;
        }
        var cuentaBuscada = _validate.ValidateNumeroCuenta("Introduce el Número de Cuenta: ");
    
        bool cuentaEncontrada = false;
        
        for (int i = 0; i < _banco.GetLength(0); i++) {
            if (_banco[i] is not { } cuenta) continue;
            
            if (cuenta.NumeroDeCuentaBancaria == cuentaBuscada) {
            
                var cantidad = _validate.ValidateSaldo($"Saldo actual: {cuenta.Saldo}. ¿Cuánto quieres ingresar? ");
                cuenta.Saldo += cantidad;
            
                Log.Information($"Ingreso realizado. Nuevo saldo: {cuenta.Saldo}");
                return;
            }
        }
        if (!cuentaEncontrada) {
            Log.Error("Error: No existe ninguna cuenta con ese número.");
        }
    }

    public void RetirarDinero() {
        if (BancoVacio()) {
            Log.Error("El banco está vacío. Necesitas crear una cuenta primero...");
            return;
        }
        var cuentaBuscada = _validate.ValidateNumeroCuenta("Introduce el Número de Cuenta para retirar: ");
    
        bool cuentaEncontrada = false;
        
        for (int i = 0; i < _banco.GetLength(0); i++) {
            if (_banco[i] is not { } cuenta) continue;

            if (cuenta.NumeroDeCuentaBancaria != cuentaBuscada) continue;
            var cantidad = _validate.ValidateSaldo($"Saldo actual: {cuenta.Saldo}. ¿Cuánto quieres retirar? ");
                
            if (cuenta.Saldo >= cantidad) {
                cuenta.Saldo -= cantidad;
                Log.Information($"Retiro realizado. Nuevo saldo: {cuenta.Saldo}");
            } else {
                Log.Error($"Operación denegada. No tienes dinero suficiente. Saldo: {cuenta.Saldo}");  
            }
            return;
        }

        if (!cuentaEncontrada) {
            Log.Error("Error: No existe ninguna cuenta con ese número.");
        }
    }

    public void AñadirTitular() {
        var cuentaBuscada = _validate.ValidateNumeroCuenta("Introduce el Número de Cuenta para añadir un titular: ");
        for (int i = 0; i < _banco.GetLength(0); i++) {
            if (_banco[i] is not { } cuenta) continue;
            if (cuenta.NumeroDeCuentaBancaria != cuentaBuscada) continue;
            for (int j = 0; j < cuenta.Titular.GetLength(0); j++) {
                if (cuenta.Titular[j] != null) continue;
                var titularNew = CrearTitular();
                cuenta.Titular[j] = titularNew;
                Log.Information("Se ha añadido el nuevo titular correctamente.");
                return;
            }
            Log.Error("Error: La cuenta ya tiene 3 titulares");
            return;
        }
        Log.Error("Error: No se ha encontrado ninguna cuenta con ese número.");
    }
 
    
    public void EliminarTitular() {
    
        var cuentaBuscada = _validate.ValidateNumeroCuenta("Introduce el Número de Cuenta: ");
        for (int i = 0; i < _banco.GetLength(0); i++) {
            if (_banco[i] is not { } cuenta) continue;
            if (cuenta.NumeroDeCuentaBancaria != cuentaBuscada) continue;
            int contador = 0;
            foreach(var t in cuenta.Titular) {
                if (t != null) contador++;
            }
            if (contador <= 1) {
                Log.Error("Error: No puedes borrar al último titular. La cuenta debe tener al menos uno.");
                return;
            }
            var dniBuscado = _validate.ValidateDni("Introduce el DNI del titular a eliminar: ");
            for (int j = 0; j < cuenta.Titular.GetLength(0); j++) {
                if (cuenta.Titular[j] == null || cuenta.Titular[j]?.Dni != dniBuscado) continue;
                cuenta.Titular[j] = null; // Borramos
                Log.Information("Titular eliminado correctamente.");
                return; // Salimos felices
            }
            Log.Error("No se ha encontrado ese DNI en esta cuenta.");
            return;
        }
    
        Log.Error("No se ha encontrado ninguna cuenta con ese número.");
    }
    private Titular CrearTitular() {
        var nombre = _validate.ValidateNombre("Introduce el nombre del titular: ");
        var apellidos = _validate.ValidateApellido("Introduce el apellido del titular: ");
        var dni = _validate.ValidateDni("Introduce el DNI del titular: ");
        int telefono = _validate.ValidateTelefono("Introduce el telefono del titular: ");
        var titularNew = new Titular {
            Nombre = nombre,
            Apellido = apellidos,
            Dni = dni,
            Telefono = telefono
        };
        return titularNew;
    }
    private bool BancoVacio() {
        foreach (var t in _banco) {
            if (t != null) return false;
        }
        return true;
    }
    

    
}














