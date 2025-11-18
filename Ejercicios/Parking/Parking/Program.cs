using System.Text;
using System.Text.RegularExpressions;
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
       ImprimirMenu();
       opcionElegida = VerificarOpcion("Selecciones una opcion: ");
       switch (opcionElegida) {
           case (int)MenuOpcion.EntradaParkin:
               
               break;
           case (int)MenuOpcion.AñadirVehiculo:
               AñadirVehiulo(parking, ref pos);
               break;
           case (int)MenuOpcion.VerParking:
               PrintMatrix(parking, pos);
               break;
           case (int)MenuOpcion.InfPlaza:
               break;
           case (int)MenuOpcion.BusqiedaNip:
               BuscarPorId(parking, ref pos);
               break;
           case (int)MenuOpcion.BusquedaMatricula:
               BuscarPorMatricula(parking,ref pos);
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
    Vehiculo v1 = new Vehiculo { Matricula = "1111BBB", Marca = "Alfa", Modelo = "Romeo", Profesor = new Profesor { Id = "P5N6M8", Nombre = "Laura García", Email = "laura.g@mail.com" } };
    Vehiculo v2 = new Vehiculo { Matricula = "2222CCC", Marca = "Seat", Modelo = "Ibiza", Profesor = new Profesor { Id = "L6P7F4", Nombre = "Javier López", Email = "j.lopez@mail.com" } };
    Vehiculo v3 = new Vehiculo { Matricula = "3333DDD", Marca = "Yamaha", Modelo = "YZF-R3", Profesor = new Profesor { Id = "L6F4S2", Nombre = "Elena Ruíz", Email = "e.ruiz@mail.com" } };
    Vehiculo v4 = new Vehiculo { Matricula = "4444EEE", Marca = "Tesla", Modelo = "Model 3", Profesor = new Profesor { Id = "P9P6H2", Nombre = "David Soto", Email = "d.soto@mail.com" } };
    Vehiculo v5 = new Vehiculo { Matricula = "5555FFF", Marca = "Honda", Modelo = "CB500F", Profesor = new Profesor { Id = "A3D5G7", Nombre = "Sara Vidal", Email = "s.vidal@mail.com" } };
    
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

void AñadirVehiulo(Vehiculo?[,] matrix,ref Posicion pos) {
    Log.Information("Añadiendo un vehiculo al parking...");
    ValidarPosicion("Posicion elegida: ",ref pos);
    if (matrix[pos.Fila, pos.Columna] != null) {
        Console.WriteLine($"Ya existe un vehiculo al parking.");
    }
    else {
        string matriculaCorrecta = ValidarMatricula("¿Cual es tu matricula?");
        bool encontrado = false;
        for (int i = 0; i < matrix.GetLength(0); i++) {
            for (int j = 0; j < matrix.GetLength(1); j++) {
                if (matrix[i, j]?.Matricula == matriculaCorrecta) {
                    encontrado = true;
                }
            }
        }

        if (encontrado) {
            Console.WriteLine("Se ha encontrado la matricula en el parking");
        }
        else {
            string nombreTemp;
            Console.WriteLine("Dime la marca: ");
            var marcaTemp = Console.ReadLine()?.Trim() ?? "No introducida";
            Console.WriteLine("Dime el modelo: ");
            var modeloTemp = Console.ReadLine()?.Trim() ?? "No introducida";
            Console.WriteLine("Dime el profesor: ");
            var id = ValidarId("Cual es el id del profesor");
            do {
                Console.WriteLine("Nombre del profesor: ");
                nombreTemp = Console.ReadLine()?.Trim() ?? "No introducida";
                if (nombreTemp.Length < 2) { 
                    Console.WriteLine($"El nombre del profesor debe tener al menos 2 letras.");
                }
            } while (nombreTemp.Length < 2);
            var correo = ValidarCorreo("Introduce el correo del profesor: ");
            var vehiculoAñadido = new Vehiculo {
                Matricula = matriculaCorrecta,
                Marca = marcaTemp,
                Modelo = modeloTemp,
                Profesor = new Profesor{
                    Email = correo,
                    Nombre = correo,
                    Id = id
                }
            };
            matrix[pos.Fila, pos.Columna] = vehiculoAñadido;

        }
    }
    
    
    
}

void ValidarPosicion(string mensaje,ref Posicion pos) {
    pos.Fila = 0;
    pos.Columna = 0;

    bool isPosicionOk = false;
    var regex = new Regex(@"^[1-2]:[1-5]$");

    do {
        Console.WriteLine(mensaje);
        var input = Console.ReadLine()?.Trim() ?? "-1";

        if (regex.IsMatch(input)) {
            var posicion = input.Split(':');
            pos.Fila = int.Parse(posicion[0]) - 1;
            pos.Columna = int.Parse(posicion[1])- 1;
            isPosicionOk = true;
        }
        else {
            Console.Write("Posicion intriducida erronea");
        }
    } while (!isPosicionOk);
}

string ValidarMatricula(string mensaje) {
    string matricula = "";
    var regex = new Regex(@"^\d{4}[B-DF-HJ-NP-TV-Z]{3}$");
    bool isMatriculaOk = false;
    do {
        Console.WriteLine(mensaje);
        var input = Console.ReadLine()?.Trim() ?? "";
        if (regex.IsMatch(input)) {
            matricula = input;
            isMatriculaOk = true;
            
        }
        else {
            Console.Write("Matricula intriducida erronea");
        }
    } while (!isMatriculaOk);
    return matricula;
}

string ValidarId(string mensaje) {
    string id = "";
    var regex = new Regex(@"^[A-Z]\d[A-Z]\d[A-Z]\d$");
    bool isIdOk = false;
    do {
        Console.WriteLine(mensaje);
        var input = Console.ReadLine()?.Trim() ?? "";
        if (regex.IsMatch(input)) {
            id = input;
            isIdOk = true;
        }
    } while (!isIdOk);
    return id;
}

string ValidarCorreo(string msj) {
    var correo = "";
    var regexEmail = new Regex(@"^\w{1,20}@\w{1,15}\.\w{1,4}$");
    bool isGmailOk = false;
    do {
        Console.WriteLine(msj);
        var input = Console.ReadLine()?.Trim() ?? "";
        if (regexEmail.IsMatch(input)) {
            correo = input;
            isGmailOk = true;
        }
    } while (!isGmailOk);
    return correo;
}
void ImprimirMenu() {
    Console.WriteLine("----------- Menú Parking -----------");
    Console.WriteLine($"{(int)MenuOpcion.EntradaParkin}.- Entrar al Parking.");
    Console.WriteLine($"{(int)MenuOpcion.AñadirVehiculo}.- Añadir vehiculo manualmente.");
    Console.WriteLine($"{(int)MenuOpcion.VerParking}.- Ver el Parking.");
    Console.WriteLine($"{(int)MenuOpcion.InfPlaza}.- Ver información de una plaza.");
    Console.WriteLine($"{(int)MenuOpcion.BusqiedaNip}.- Buscar por NIP");
    Console.WriteLine($"{(int)MenuOpcion.BusquedaMatricula}.- Buscar por matrícula.");
    Console.WriteLine($"{(int)MenuOpcion.ListaMatricula}.- Listado de coches por matrícula.");
    Console.WriteLine($"{(int)MenuOpcion.ListaProfesoresVehiculo}.- Listado profesores y sus coches.");
    Console.WriteLine($"{(int)MenuOpcion.ActualizarDatos}.- Actualizar datos de un vehículo.");
    Console.WriteLine($"{(int)MenuOpcion.BorrarVehiculo}.- Borrar vehículo.");
    Console.WriteLine($"{(int)MenuOpcion.Salir}.- Salir.");
    Console.WriteLine("------------------------------------");
}

void BuscarPorId(Vehiculo?[,] arr, ref Posicion pos) {
    var idBuscado = ValidarId("Indique el id del profesor que desea buscar");
    Profesor? profesor;
    Vehiculo? vehiculo;
    for (int i = 0; i < pos.Fila; i++) {
        for (int j = 0; j < pos.Columna; j++) {
            if (arr[i, j]?.Profesor.Id == idBuscado) {
                Log.Information("Profesor encontrado correctamente");
                profesor = arr[i, j]?.Profesor;
                vehiculo = arr[i, j];
                Console.WriteLine($"El nombre del profesor es con id {idBuscado} es: {profesor?.Nombre}");
                Console.WriteLine($"El apellido del profesor con id {idBuscado} es: {profesor?.Email}");
                Console.WriteLine($"La marca del vehiculo del profesor {profesor?.Nombre} es: {vehiculo?.Marca}");
                Console.WriteLine($"El modelo del vehiculo del profesor {profesor?.Nombre} es: {vehiculo?.Modelo}");
                Console.WriteLine($"La matricula del vehiculo del profesor {profesor?.Nombre} es: {vehiculo?.Matricula}");
            }
            else {
                Console.WriteLine($"El nombre del profesor no existe");
            }
        }
    }
}
void BuscarPorMatricula(Vehiculo?[,] arr, ref Posicion pos) {
    var matricula = ValidarMatricula("Indique la maricula que desea buscar");
    Profesor? profesor;
    Vehiculo? vehiculo;
    for (int i = 0; i < pos.Fila; i++) {
        for (int j = 0; j < pos.Columna; j++) {
            if (arr[i, j]?.Matricula == matricula) {
                Log.Information("Profesor encontrado correctamente");
                profesor = arr[i, j]?.Profesor;
                vehiculo = arr[i, j];
                Console.WriteLine($"El nombre del profesor es con matricula {matricula} es: {profesor?.Nombre}");
                Console.WriteLine($"El apellido del profesor con matricula {matricula} es: {profesor?.Email}");
                Console.WriteLine($"La marca del vehiculo de la matricula {matricula} es: {vehiculo?.Marca}");
                Console.WriteLine($"El modelo del vehiculo de la matricula {matricula} es: {vehiculo?.Modelo}");
            }
            else {
                Console.WriteLine($"El nombre del profesor no existe");
            }
        }
    }
}
