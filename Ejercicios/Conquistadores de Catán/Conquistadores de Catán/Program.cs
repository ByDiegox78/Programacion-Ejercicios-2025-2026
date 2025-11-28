
using System.Text;
using System.Text.RegularExpressions;
using Conquistadores_de_Catán.Class;
using Conquistadores_de_Catán.Enum;
using Conquistadores_de_Catán.Structs;
using Serilog;
using Serilog.Core;
using static System.Console;

var log = new LoggerConfiguration().WriteTo.Console().CreateLogger();
OutputEncoding = Encoding.UTF8;
const int Fila = 3;
const int Columna = 4;
const int CantidadDeCasillas = 12; 
const int Meta = 20;


var rnd = new Random();
Clear();
Main(args);

void Main(string[] args) {
    log.Information("⚔️ Iniciando Conquistadores de Catán ⚔️");
    Casilla[,] tablero = new Casilla[Fila, Columna];
    Posicion pos;
    pos.Filas = 0;
    pos.Columnas = 0;
    InitTablero(tablero);
    PrintMatrix(tablero);
    log.Information("--- FASE DE ASIGNACIÓN DE CASILLAS ---");
    AsignacionDeCasillas(tablero, ref pos);
    Juego(tablero);
}

void InitTablero(Casilla[,] matrix) {
    int random;
    for (int i = 0; i < matrix.GetLength(0); i++) {
        for (int j = 0; j < matrix.GetLength(1); j++) {
            matrix[i, j] = new Casilla();
            random = rnd.Next(1,7);
            matrix[i, j].Valor = random;
            random = rnd.Next(0,3);
            Recurso recursoAleatorio = (Recurso)random;
            matrix[i, j].Recurso = recursoAleatorio;
        }
    }
}

void PrintMatrix(Casilla[,] matrix) {
    Write("   "); 
    for (int j = 0; j < matrix.GetLength(1); j++) {
        Write($"[{j + 1, 2}]"); 
    }
    WriteLine("");
    for (int i = 0; i < matrix.GetLength(0); i++) {
        Write($"[{i+1}]");
        for (int j = 0; j < matrix.GetLength(1); j++) {
            if (matrix[i, j] is not { } call) continue;
            switch (call.Recurso) {
                case Recurso.Carbon:
                    Write("[⛏️]");
                    break;
                case Recurso.Madera:
                    Write("[🪵]");
                    break;
                case Recurso.Trigo:
                    Write("[🌽]");
                    break;
            }
        }
        WriteLine("");
    }
}

void AsignacionDeCasillas(Casilla[,] matrix,ref Posicion posicion) {  
     for (int i = 0; i < CantidadDeCasillas; i++) {
         if (i % 2 == 0) {
             WriteLine("===============================================");
             log.Information($"Turno {i + 1} del humano:");
             if (i > 0) {
                 PrintMatrixAux(matrix);
             }
             AsignarCasillaHumano(matrix, ref posicion);
         }
         else {
             WriteLine("===============================================");
             log.Information($"Turno {i + 1} de Rider:");
             AsignarCasillaRider(matrix);
             
         }
     }
}

void AsignarCasillaRider(Casilla[,] matrix) {
    var seleccionado = false;
    int fila = 0;
    int columna = 0;
    while (!seleccionado) {
        fila = rnd.Next(0, matrix.GetLength(0));
        columna = rnd.Next(0, matrix.GetLength(1));
        if (matrix[fila,columna].Duenio == null) {
            matrix[fila, columna].Duenio = Duenio.Rider;
            seleccionado = true;
        }
    }
    log.Information($"🤖 Rider eligio la casilla: {fila + 1}:{columna + 1}");
}
void AsignarCasillaHumano(Casilla[,] matrix,ref Posicion posicion) {
    var seleccionado = false;
    while (!seleccionado) {
        LeerEntrada("Que casilla quieres", ref posicion);
        if (matrix[posicion.Filas, posicion.Columnas].Duenio == null) {
            matrix[posicion.Filas, posicion.Columnas].Duenio = Duenio.Humano;
            seleccionado = true;
        } else {
            log.Warning("¡Esa casilla ya está ocupada! Intente de nuevo.");
        }
    }
    log.Information($"🙎 Humano eligio la casilla: {posicion.Filas + 1}:{posicion.Columnas +1 }");
}

void LeerEntrada(string msg ,ref Posicion posicion) {
    WriteLine(msg);
    var regex = new Regex("^[1-3]{1}:[1-4]{1}$");
    var input = (ReadLine() ?? "").Trim();
    while (!regex.IsMatch(input)) {
        Console.WriteLine("Error: Entrada inválida. Inténtalo de nuevo. Formato correcto: 1-3:1-4");
        input = ReadLine()?.Trim().ToUpper() ?? "";
    }
    var partes = input.Split(':');
    posicion.Filas = int.Parse(partes[0]) - 1;
    posicion.Columnas = int.Parse(partes[1]) - 1;
}
int LanzarDados() {
   var random = rnd.Next(1, 7);
   return random;
}
void Juego(Casilla[,] matrix) {
    var j1 = new Jugador();
    var j2 = new Jugador();
    int random;
    Clear();
    log.Information("--- 🎲 COMIENZA EL JUEGO DE RECOLECCIÓN ---");
    do {
        Thread.Sleep(1000);
        log.Information("--- TURNO DEL HUMANO ---");
        random = LanzarDados(); //6
        AsignarRecurso(matrix, random, j1, j2);
        
        Thread.Sleep(1000);
        log.Information("--- TURNO DEL RIDER ---");
        
        random = LanzarDados();
        AsignarRecurso(matrix, random, j1, j2);
        MostrarAlmacenes(j1,j2);
        
    } while (!((j1.AlmacenMadera >= Meta && j1.AlmacenTrigo >= Meta && j1.AlmacenCarbon >= Meta) || (j2.AlmacenMadera >= Meta && j2.AlmacenTrigo >= Meta && j2.AlmacenCarbon >= Meta)));
}
void AsignarRecurso(Casilla[,] matrix, int rndm, Jugador j1, Jugador j2) {
    for (int i = 0; i < matrix.GetLength(0); i++) {
        for (int j = 0; j < matrix.GetLength(1); j++) {
            if (matrix[i,j].Valor == rndm) {
                if (matrix[i,j].Duenio == Duenio.Humano) {
                    switch (matrix[i,j].Recurso) {
                        case Recurso.Carbon:
                            j1.AlmacenCarbon += matrix[i, j].Valor;
                            break;
                        case Recurso.Madera:
                            j1.AlmacenMadera += matrix[i, j].Valor;
                            break;
                        case Recurso.Trigo:
                            j1.AlmacenTrigo += matrix[i, j].Valor;
                            break;
                    }
                }
                else if (matrix[i,j].Duenio == Duenio.Rider) {
                    switch (matrix[i,j].Recurso) {
                        case Recurso.Carbon:
                            j2.AlmacenCarbon += matrix[i, j].Valor;
                            break;
                        case Recurso.Madera:
                            j2.AlmacenMadera += matrix[i, j].Valor;
                            break;
                        case Recurso.Trigo:
                            j2.AlmacenTrigo += matrix[i, j].Valor;
                            break;
                    }
                }
            }
        }
    }
}
void MostrarAlmacenes(Jugador j1, Jugador j2) {
    log.Information("--- ESTADO ACTUAL DE RECURSOS ---");
    WriteLine($"🙎 Humano | Carbón: {j1.AlmacenCarbon}, Madera: {j1.AlmacenMadera}, Trigo: {j1.AlmacenTrigo}");
    WriteLine($"🤖 Rider | Carbón: {j2.AlmacenCarbon}, Madera: {j2.AlmacenMadera}, Trigo: {j2.AlmacenTrigo}");
}
void PrintMatrixAux(Casilla[,] matrix) {
    for (int i = 0; i < matrix.GetLength(0); i++) {
        for (int j = 0; j < matrix.GetLength(1); j++) {
            if (matrix[i, j].Duenio == Duenio.Humano) {
                Write("[🤵]");
            }
            else if (matrix[i, j].Duenio == Duenio.Rider) {
                Write("[🤖]");
            }
            else {
                Write("[  ]");
            }
        }
        WriteLine();
    }
}

