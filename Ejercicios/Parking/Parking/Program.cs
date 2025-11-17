using System.Text;
using Parking.Enums;
using Parking.Structs;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug() // Permitir mensajes Debug y superiores
    .WriteTo.Console() // Salida a consola
    .CreateLogger(); // Utilizamos Serilog para el logging

Console.OutputEncoding = Encoding.UTF8;
var random = new Random();

Main(args);

void Main(string[] args) {
    Posicion pos;
   
    Vehiculo?[,] parking = new Vehiculo?[2, 5]; 
   
    pos.Fila = parking.GetLength(0);
   pos.Columna = parking.GetLength(1);
   
   int numVehiculos = 0;
   
   InitParking(parking,ref  numVehiculos);
   
   PrintMatrix(parking, pos);
   
   int opcionElegida;
   do {
       opcionElegida = VerificarOpcion("Selecciones una opcion: ");
       switch (opcionElegida) {
           case (int)MenuOpcion.EntradaParkin:
               EntradaParking(parking, pos);
               break;
           case (int)MenuOpcion.AñadirVehiculo:
               break;
           case (int)MenuOpcion.VerParking:
               PrintMatrix(parking, pos);
               break;
           case (int)MenuOpcion.InfPlaza:
               break;
           case (int)MenuOpcion.BusqiedaNip:
               break;
           case (int)MenuOpcion.BusquedaMatricula:
               break;
           case (int)MenuOpcion.ListaMatricula:
               break;
           case (int)MenuOpcion.ListaProfesoresVehiculo:
               break;
           case (int)MenuOpcion.ActualizarDatos:
               break;
           case (int)MenuOpcion.BorrarVehiculo:
               break;
           case (int)MenuOpcion.Salir:
               break;
       }
   } while (opcionElegida != (int)MenuOpcion.Salir);
   
}



void InitParking(Vehiculo?[,] parking, ref int numV) {
    Log.Debug("Creando vehiculos...");
    Vehiculo v1 = new Vehiculo { Matricula = "1111BBB", Marca = "Alfa", Modelo = "Romeo", Profesor = new Profesor { Nip = "P1001", Nombre = "Laura García", Email = "laura.g@mail.com" } };
    Vehiculo v2 = new Vehiculo { Matricula = "2222CCC", Marca = "Seat", Modelo = "Ibiza", Profesor = new Profesor { Nip = "P1002", Nombre = "Javier López", Email = "j.lopez@mail.com" } };
    Vehiculo v3 = new Vehiculo { Matricula = "3333DDD", Marca = "Yamaha", Modelo = "YZF-R3", Profesor = new Profesor { Nip = "P1003", Nombre = "Elena Ruíz", Email = "e.ruiz@mail.com" } };
    Vehiculo v4 = new Vehiculo { Matricula = "4444EEE", Marca = "Tesla", Modelo = "Model 3", Profesor = new Profesor { Nip = "P1004", Nombre = "David Soto", Email = "d.soto@mail.com" } };
    Vehiculo v5 = new Vehiculo { Matricula = "5555FFF", Marca = "Honda", Modelo = "CB500F", Profesor = new Profesor { Nip = "P1005", Nombre = "Sara Vidal", Email = "s.vidal@mail.com" } };
    
    var coche = new Vehiculo[] { v1, v2, v3, v4, v5 };

    while (numV < coche.Length) {
        int filaRandom = random.Next(parking.GetLength(0));
        int columnaRandom = random.Next(parking.GetLength(1));

        if (parking[filaRandom, columnaRandom] is null) {
            parking[filaRandom, columnaRandom] = coche[numV];
            numV++;
        }
    }
}
int VerificarOpcion(string msg) {

    int opcionElegida = 0;
    bool isOpcionOk = false;
    do {
        Console.WriteLine(msg);
        var input = Console.ReadLine()?.Trim() ?? "-1";
        Log.Debug("Validando opción...");
        
        if (int.TryParse(input, out opcionElegida)) {
            if (opcionElegida >= (int)MenuOpcion.EntradaParkin && opcionElegida <= (int)MenuOpcion.Salir) {
                Log.Information($"Opción {opcionElegida} leída correctamente.");
                isOpcionOk = true;
            } else {
                Console.WriteLine($"Opción introducida innexistente. Introduzca una de las {MenuOpcion.Salir} posibles."); 
                Log.Information($"Opción {opcionElegida} no reconocida.");
            }
        } else {
            Console.WriteLine($"Opción introducida no reconocida. Introduzca una de las {MenuOpcion.Salir} posibles.");
            Log.Information($"Dato introducido no válido.");
        }
    } while (!isOpcionOk);
    return opcionElegida;
}

void PrintMatrix(Vehiculo?[,] matrix, Posicion pos) {
    for (int i = 0; i < matrix.GetLength(0); i++) {
        for (int j = 0; j < matrix.GetLength(1); j++) {
            if (j == 0) { // indices filas
                Console.Write((i + 1) + " ");
            }
            if (matrix[i, j] is null)
                Console.Write("[🔵]");
            else 
                Console.Write("[🚗]");
        }
        Console.WriteLine();
    }
}

void EntradaParking(Vehiculo?[,] matrix, Posicion pos) {
    Log.Information("Añadiendo un vehiculo al parking...");
    
}