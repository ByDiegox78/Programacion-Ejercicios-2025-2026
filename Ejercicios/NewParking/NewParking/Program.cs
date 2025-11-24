
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using NewParking.Enum;
using NewParking.Struct;
using Serilog;
Console.OutputEncoding = Encoding.UTF8;
Console.Clear();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug() // Permitir mensajes Debug y superiores
    .WriteTo.Console() // Salida a consola
    .CreateLogger(); // Utilizamos Serilog para el logging

const string LetrasValidas = "BCDFGHJKLMNPRSTVWXYZ";

var random = new Random();
void Main(string[] args) {
    
};

Main(args);{
    
    var parking = new Vehiculo?[2,5];
    
    Posicion pos = new Posicion();
    pos.Fila = parking.GetLength(0);
    pos.Columna = parking.GetLength(1);
    int numVehiculos = 0;
    InitParking(parking, ref numVehiculos);
    int opcion;
    do {
        MostrarMenuParking();
        opcion = LeerOpcion("Introduce la opcion del parkin:...");
        switch (opcion) {
            case (int)MenuParking.VerParking:
                VerParking(parking);
                break;
            case (int)MenuParking.AparcarCoche:
                AparcarCoche(parking, ref pos);
                break;
            case (int)MenuParking.ActualizarMatricula:
                ActualizarMatricula(parking);
                break;
            case (int)MenuParking.DesaparcarCoche:
                DesaparcarCoche(parking);
                break;
            case (int)MenuParking.BuscarCoche:
                BuscarCoche(parking);
                break;
            case (int)MenuParking.OrdenarPorMatricula:
                MostrarPorMatriculaAsc(parking);
                break;
            case (int)MenuParking.OrdenarPorFechaMatriculacion:
                //OrdenarPorMatriculacion();
                break;
            case (int)MenuParking.Salir:
                break;
        }
    } while (opcion != 8);
}
void MostrarMenuParking()
{
    Console.WriteLine("========== MENÚ PARKING ==========");
    Console.WriteLine("1. Ver parking");
    Console.WriteLine("2. Aparcar coche");
    Console.WriteLine("3. Actualizar matrícula");
    Console.WriteLine("4. Desaparcar coche");
    Console.WriteLine("5. Buscar coche");
    Console.WriteLine("6. Ordenar por matrícula");
    Console.WriteLine("7. Ordenar por fecha de matriculación");
    Console.WriteLine("8. Salir");
    Console.WriteLine("==================================");
}
void InitParking(Vehiculo?[,] parking, ref int numV) {
    Log.Debug("Creando vehiculos...");
    Vehiculo v1 = new Vehiculo { Matricula = "1111CBB", Marca = "Alfa",   FechaMatriculacion = "2020-05-12" };
    Vehiculo v2 = new Vehiculo { Matricula = "2222DCC", Marca = "Seat",  FechaMatriculacion = "2019-11-03" };
    Vehiculo v3 = new Vehiculo { Matricula = "3333BDD", Marca = "Yamaha",FechaMatriculacion = "2021-04-28" };
    Vehiculo v4 = new Vehiculo { Matricula = "4444EEE", Marca = "Tesla", FechaMatriculacion = "2022-09-15" };
    Vehiculo v5 = new Vehiculo { Matricula = "5555FFF", Marca = "Honda", FechaMatriculacion = "2018-02-07" };

    
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
int LeerOpcion(string msj) {
    var regex = new Regex("^[1-8]$");
    var isOk = false;
    string? input;
    var opcion = 0;
    do {
        Console.Write(msj);
        input = (Console.ReadLine() ?? "").Trim();
        isOk  = regex.IsMatch(input);
        if (isOk) {
            opcion = int.Parse(input);
        }
    } while (!isOk);
    return opcion;
}
void VerParking(Vehiculo?[,] parking) {
    for (int i = 0; i < parking.GetLength(0); i++) {
        for (int j = 0; j < parking.GetLength(1); j++) {
            if (j == 0) {
                 Console.Write((i + 1) + " ");
            }
            if (parking[i, j] is null)
                Console.Write("[🔵]");
            else 
                Console.Write("[🚗]");
        }
        Console.WriteLine();
    }
}
void AparcarCoche(Vehiculo?[,] parking,ref Posicion pos) {
    ValidarPosicion("Donde quieres aparcar el coche: ",ref pos);
    if (parking[pos.Columna, pos.Columna] != null) {
        Log.Information($"Ya hay un vehiculo en esa plaza con matricula {parking[pos.Columna, pos.Columna]?.Matricula}");
    }
    else {
        var matriculaNueva = ValidarMatricula("Introduce la matricula del coche:...");
        bool encontrado = false;
        for (int i = 0; i < parking.GetLength(0); i++) {
            for (int j = 0; j < parking.GetLength(1); j++) {
                if (parking[i, j]?.Matricula == matriculaNueva) {
                    encontrado = true;
                }
            }
        }
        if (encontrado) {
            Console.WriteLine("Se ha encontrado la matricula en el parking");

        }else {
            string fechaMatriculacionNueva;
            Console.WriteLine("Dime la fecha de matriculacion: ");
            fechaMatriculacionNueva = ValidarFechaMatriculacion("Introduce la fecha matriculacion:...");
            string marca =  ValidarMarca("Introduce la marca:...");

            var vehiculoNuevo = new Vehiculo {
                Matricula = matriculaNueva,
                Marca = marca,
                FechaMatriculacion = fechaMatriculacionNueva
            };
            parking[pos.Columna, pos.Columna] = vehiculoNuevo;
        }
    }
    
    
    
}
string ValidarMarca(string msj) {
    var regex = new Regex("^[a-zA-Z]{3,}$");
    var isOk = false;
    string? input;
    string marca = "";
    do {
        Console.Write(msj);
        input = (Console.ReadLine() ?? "").Trim();
        if (regex.IsMatch(input)) {
            marca = input;
            isOk = true;
        }
        else {
            Console.WriteLine("Matricula no valida, vuelve a introducirla con formato: NNNNLLL");
        }
    } while (!isOk);

    return marca;
}
string ValidarMatricula(string msj) {
    var regex = new Regex("^[0-9]{4}[" + LetrasValidas + "]{3}$");
    var isOk = false;
    string? input;
    string matricula = "";
    do {
        Console.Write(msj);
        input = (Console.ReadLine() ?? "").Trim();
        isOk = regex.IsMatch(input);
        if (isOk) {
            matricula = input;
        }
        else {
            Console.WriteLine("Matricula no valida, vuelve a introducirla con formato: NNNNLLL");
        }
    } while (!isOk);

    return matricula;
}
string ValidarFechaMatriculacion(string msj) {
    var regex = new Regex(@"^(\d{2})([\/\-])(\d{2})\2(\d{4})$");
    var isValid = false;
    string? input;
    string fecha = ""; //17/01/2004
    do {
        Console.Write(msj);
        input = (Console.ReadLine() ?? "").Trim();
        if (regex.IsMatch(input)) {
            var dia = int.Parse(input.Substring(0, 2));
            var mes = int.Parse(input.Substring(3, 2));
            var anio = int.Parse(input.Substring(6, 4));

            if (ValidarDate(dia, mes,anio)) {
                fecha = input;
                isValid = true;
            }
            else {
                Console.WriteLine("Fecha no válida: el día no corresponde al mes o al año bisiesto.");
            }
           
        }
        else {
            Console.WriteLine("Fecha no valida");
        }
    } while (!isValid);
    return fecha;
}
bool ValidarDate(int dias, int mes, int anio) {
    int diasMax = mes switch {
        1 or 3 or 5 or 7 or 8 or 10 or 12 => 31,
        4 or 6 or 9 or 11 => 30,
        2 => EsBisiesto(anio) ? 29 : 28,
        _ => 0
    };
    return dias >= 1 && dias <= diasMax;
}
bool EsBisiesto(int anio) {
    return (anio % 4 == 0 && anio % 100 != 0) || (anio % 400 == 0);
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
void ActualizarMatricula(Vehiculo?[,] parking){
    var matriculaBuscada  = ValidarMatricula("Que matricula quieres modificar:...");
    for (int i = 0; i < parking.GetLength(0); i++) {
        for (int j = 0; j < parking.GetLength(1); j++) {
            if (parking[i, j] != null) {
                if (parking[i, j]?.Matricula == matriculaBuscada) {
                    var matriculaNueva = ValidarMatricula("Asigna la nueva matricula:...");
                    Vehiculo vehiculo = new Vehiculo {
                        Matricula = matriculaNueva,
                        FechaMatriculacion = parking[i, j]!.Value.FechaMatriculacion,
                        Marca = parking[i, j]!.Value.Marca,
                    };
                    parking[i, j] = vehiculo;
                }
                else {
                    Console.WriteLine($"Matricula no valida: {matriculaBuscada}");
                }
            }
        }
    }
}
void DesaparcarCoche(Vehiculo?[,] parking) {
    var matriculaBuscada  = ValidarMatricula("Que matricula quieres modificar:...");
    bool encontrado = false;
    for (int i = 0; i < parking.GetLength(0); i++) {
        for (int j = 0; j < parking.GetLength(1); j++) {
            if (parking[i, j]?.Matricula == matriculaBuscada) {
                Console.WriteLine($"Matricula matricula encontrada: {matriculaBuscada}");
                parking[i, j] = null;
                encontrado = true;
                break;
            }
        }
    }
    if (!encontrado) {
        Console.WriteLine($"Matricula no encontrada: {matriculaBuscada}");
    }
}
void BuscarCoche(Vehiculo?[,] parking) {
    var matriculaBuscada  = ValidarMatricula("Que matricula quieres modificar:...");
    bool encontrado = false;
    for (int i = 0; i < parking.GetLength(0); i++) {
        for (int j = 0; j < parking.GetLength(1); j++) {
            if (parking[i, j]?.Matricula == matriculaBuscada) {
                Console.WriteLine($"Matricula matricula encontrada: {matriculaBuscada}");
                Console.WriteLine($"Su vehiculo se encuentra en la plaza: {i}:{j}");
                encontrado = true;
                break;
            }
        }
    }
    if (!encontrado) {
        Console.WriteLine($"Matricula no encontrada: {matriculaBuscada}");
    }
}
void OrdenarPorMatricula(Vehiculo[] vehiculosExistentes) {
    
    for (int i = 0; i < vehiculosExistentes.Length - 1; i++) {
        bool swapped = false;
        for (int j = 0; j < vehiculosExistentes.Length - i - 1; j++) {
            
            // comparacion de los numeros de la matricla
            int matriculaActual = int.Parse(vehiculosExistentes[j].Matricula.Substring(0,4));
            
            int matriculaSiguiente = int.Parse(vehiculosExistentes[j + 1].Matricula.Substring(0, 4));

            // si la siguiente matricula es menor se pone en la posicion actual
            if (matriculaActual > matriculaSiguiente) {
                // swap
                Swap(vehiculosExistentes, j, j + 1);
                swapped = true;
            }
        }
        // si no hubo intercambio el array está ordenado asc en base a su matricula
        if (!swapped) break;
    }
}
void Swap(Vehiculo[] arr, int i, int j) {
    (arr[i], arr[j]) = (arr[j], arr[i]);
}
Vehiculo[] ArrayDeVehiculos(Vehiculo?[,] parking) {
    int coches = 0;
    for (int i = 0; i < parking.GetLength(0); i++) {
        for (int j = 0; j < parking.GetLength(1); j++) {
            if (parking[i, j] is not null)
                coches++;
        }
    }
    Vehiculo[] vehiculosAparcados = new Vehiculo[coches];
    if (coches == 0) {
        Console.WriteLine("No hay vehículos en el parking.");
        Log.Warning("No hay vehículos para mostrar.");
        return vehiculosAparcados;
    }

    int indice = 0;
    for (int i = 0; i < parking.GetLength(0); i++) {
        for (int j = 0; j < parking.GetLength(1); j++) {
            if (parking[i, j] is { } vehiculo) {
                vehiculosAparcados[indice++] = vehiculo;
            }
        }
    }
    return vehiculosAparcados;
}

void ImprimirDatos(Vehiculo[] vehiculos) {
    
    Console.WriteLine("-- 🚗 Listado de vehículos por matrícula (ascendente) --");
    foreach (Vehiculo vehiculo in vehiculos) {
        Console.WriteLine("-- Información del vehículo --");
        Console.WriteLine($"- Matrícula: {vehiculo.Matricula}");
        Console.WriteLine($"- Marca: {vehiculo.Marca}");
        Console.WriteLine($"- FechaMatriculacion: {vehiculo.FechaMatriculacion}");
        Console.WriteLine("---------------------------------");
    }
}
void MostrarPorMatriculaAsc(Vehiculo?[,] parking) {
    Log.Debug("🔵 Mostrando vehículos ordenados por matrícula...");
    
    // creamos un array donde se almacenaran los coches que hay actualmente en el parking
    var vehiculosExistentes = ArrayDeVehiculos(parking);

    // ordenamos el array
    Log.Debug($"🔵 Ordenando por matrícula...");
    OrdenarPorMatricula(vehiculosExistentes);
    
    // mostramos los datos por pantalla
    Log.Debug($"🔵 Mostrando los datos...");
    Console.WriteLine();
    ImprimirDatos(vehiculosExistentes);
}


