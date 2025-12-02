using CuentaBancaria;
using System.Text;
using CuentaBancaria.Service;
using Serilog;
using static System.Console;

// 1. INICIALIZACIÓN DE SERILOG Y CULTURA
Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
Log.Information("Serilog iniciado y configurado para consola.");

// 2. INICIO (Top-level Execution)
Title = "Conquistadores De Catan";
OutputEncoding = Encoding.UTF8;
Clear();

// Menú principal
Main(args);
return;

void Main(string[] args) {
    var patan = new ServiceCuenta();
    
}

void menuBanco() {
    Log.Debug("Entrando en la funcion MenuBanco...");
    MenuOpcion opcion = 0;
    do {
        WriteLine("\n --- Opciones de Cuenta Bancaria ---");
        WriteLine("1. Crear cuenta");
        WriteLine("2. Ingresar dinero");
        WriteLine("3. Retirar dinero");
        WriteLine("4. Añadir titular");
        WriteLine("5. Borrar titular");
        WriteLine("6. Salir");

        opcion = ValidateOpcion("--- Que opcion desea elegir ---", opcion);

        switch (opcion) {
            case MenuOpcion.CrearCuenta:
                WriteLine("Has elegido: Crear cuenta");
                // Llama aquí al método correspondiente
                break;

            case MenuOpcion.IngresarDinero:
                WriteLine("Has elegido: Ingresar dinero");
                // Lógica para ingresar dinero
                break;

            case MenuOpcion.RetirarDinero:
                WriteLine("Has elegido: Retirar dinero");
                // Lógica para retirar dinero
                break;

            case MenuOpcion.AñadirTitular:
                WriteLine("Has elegido: Añadir titular");
                // Llamada a lógica de añadir titular
                break;

            case MenuOpcion.BorrarTitular:
                WriteLine("Has elegido: Borrar titular");
                // Lógica para borrar titular
                break;

            case MenuOpcion.Salir:
                WriteLine("Saliendo del menú de cuenta bancaria...");
                break;

            default:
                WriteLine("Opción no válida. Intente de nuevo.");
                break;
        }

    } while (opcion != MenuOpcion.Salir);
}

MenuOpcion ValidateOpcion(string msj, MenuOpcion opcion) {
    var input = ReadLine()?.Trim() ?? "";
    if (!int.TryParse(input, out var inputOpcion) || inputOpcion < 1 || inputOpcion > 6) {
        Log.Error("Opcion del menu invalida {Input}", input);
        opcion = 0;
    }
    else {
        opcion = (MenuOpcion)inputOpcion;
    }

    return opcion;
}