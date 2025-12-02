using CuentaBancaria.Validators;

namespace CuentaBancaria.Service;
using CuentaBancaria.Class;
using static Console;
public class ServiceCuenta
{
    private CuentaBancaria?[] _banco = new CuentaBancaria?[5];
    private ValidatorCuenta _validate = new ValidatorCuenta();

    public void CrearCuenta() {
        WriteLine("=== CREAR NUEVA CUENTA BANCARIA ===");
        
        var numeroCuenta = _validate.ValidateNumeroCuenta("Introduce el número de cuenta:");
        
        var saldo = _validate.ValidateSaldo("Introduce el saldo inicial: ");

        Write("Introduce el nombre del titular: ");
        var nombre = _validate.ValidateNombre("Introduce el nombre del titular: ");
        

        Write("Introduce los apellidos del titular: ");
        var apellidos = _validate.ValidateApellido("Introduce el apellido del titular: ");

        Write("Introduce el DNI del titular: ");
        var dni = _validate.ValidateDni("Introduce el DNI del titular: ");

        Write("Introduce el teléfono del titular: ");
        int telefono = _validate.ValidateTelefono("Introduce el telefono del titular: ");
        
        var titular = new Titular();
        titular.Nombre = nombre;
        titular.Apellido = apellidos;
        titular.Dni = dni;
        titular.Telefono = telefono;

// Crear cuenta con array
        var cuenta = new CuentaBancaria(titular);
        

        WriteLine();
        WriteLine("Cuenta creada correctamente:");
        WriteLine(cuenta);

    }
}