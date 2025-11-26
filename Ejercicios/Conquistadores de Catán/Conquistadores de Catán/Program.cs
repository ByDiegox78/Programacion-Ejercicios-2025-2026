
using System.Text;
using System.Text.RegularExpressions;
using Conquistadores_de_Catán.Class;
using Conquistadores_de_Catán.Enum;
using Conquistadores_de_Catán.Structs;
using Serilog;
using Serilog.Core;

var log = new LoggerConfiguration().WriteTo.Console().CreateLogger();
Console.OutputEncoding = Encoding.UTF8;
const int Fila = 3;
const int Columna = 4;
const int CantidadDeCasillas = 6; 


var rnd = new Random();
Main(args);

void Main(String[] args) {
    Casilla[,] tablero = new Casilla[Fila, Columna];
    Posicion pos;
    pos.Filas = 0;
    pos.Columnas = 0;
    InitTablero(tablero);
    PrintMatrix(tablero);
    AsignacionDeCasillas(tablero, ref pos);
    
    
}

void InitTablero(Casilla[,] matrix) {
    int random;
    for (int i = 0; i < matrix.GetLength(0); i++) {
        for (int j = 0; j < matrix.GetLength(1); j++) {
            matrix[i, j] = new Casilla();
            random = rnd.Next(1,7);
            matrix[i, j].SetValor(random);
            random = rnd.Next(0,3);
            Recurso recursoAleatorio = (Recurso)random;
            matrix[i, j].SetRecurso(recursoAleatorio);
        }
    }
}

void PrintMatrix(Casilla[,] matrix) {
    for (int i = 0; i < matrix.GetLength(0); i++) {
        //Console.Write($"{i} ");
        for (int j = 0; j < matrix.GetLength(1); j++) {
            if (matrix[i, j] is not { } recurso) continue;
            if (recurso.GetRecurso() == Recurso.Cargon) {
                Console.Write("[⛏️]");
            } else if (recurso.GetRecurso() == Recurso.Madera) {
                Console.Write("[🪵]"); 
            } else if (recurso.GetRecurso() == Recurso.Trigo) {
                Console.Write("[🌽]");
            }
        }
        Console.WriteLine("");
    }
}

void AsignacionDeCasillas(Casilla[,] matrix,ref Posicion posicion) {  
     log.Information("Asignacion de casillas");
     for (int i = 0; i < CantidadDeCasillas; i++) {
         if (i % 2 == 0) {
             log.Information($"Turno {i + 1} del humano:");
             AsignarCasillaHumano(matrix, ref posicion);
         }
         else {
             log.Information($"Turno {i + 1} de Rider:");
             AsignarCasillaRider(matrix);
         }
     }
}

void LeerEntrada(string msg ,ref Posicion posicion) {
    Console.WriteLine(msg);
    var regex = new Regex("^[1-3]{1}:[1-4]{1}$");
    var input = (Console.ReadLine() ?? "").Trim();
    while (!regex.IsMatch(input)) {
        Console.WriteLine("Error: Entrada inválida. Inténtalo de nuevo. Formato correcto: 1-3:1-4");
        input = Console.ReadLine()?.Trim().ToUpper() ?? "";
    }
    string[] partes = input.Split(':');

    posicion.Filas = int.Parse(partes[0]) - 1;
    posicion.Columnas = int.Parse(partes[1]) - 1;
}

void AsignarCasillaHumano(Casilla[,] matrix,ref Posicion posicion) {
    bool seleccionado = false;
    while (!seleccionado) {
        LeerEntrada("Que casilla quieres", ref posicion);
        if (matrix[posicion.Filas, posicion.Columnas].GetDueño() == null) {
            matrix[posicion.Filas, posicion.Columnas].SetDueño(Duenio.Humano);
            seleccionado = true;
        } else {
            log.Warning("¡Esa casilla ya está ocupada! Intente de nuevo.");
        }
    }
}

void AsignarCasillaRider(Casilla[,] matrix) {
    bool seleccionado = false;
    int fila = 0;
    int columna = 0;
    while (!seleccionado) {
        fila = rnd.Next(0, matrix.GetLength(0));
        columna = rnd.Next(0, matrix.GetLength(1));
        if (matrix[fila,columna].GetDueño() == null) {
            matrix[fila, columna].SetDueño(Duenio.Rider);
            seleccionado = true;
        }
    }
    log.Information($"Rider eligio la casilla: {fila + 1}:{columna + 1}");
}

