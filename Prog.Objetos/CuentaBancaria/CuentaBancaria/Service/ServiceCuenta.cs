namespace CuentaBancaria.Service;
using CuentaBancaria.Class;
using static Console;
public class ServiceCuenta
{
    private CuentaBancaria?[] banco = new CuentaBancaria?[5];

    void CrearCuenta() {
        var numeroCuenta = ReadLine();
        var saldo = ReadLine();
        var nombre = ReadLine();
        var apellidos = ReadLine();
        var dni = ReadLine();
        int telefono = int.Parse(ReadLine()!);
        
        var cuenta = new CuentaBancaria {
            NumeroDeCuentaBancaria = numeroCuenta,
            Saldo = saldo,
        }

    }
}