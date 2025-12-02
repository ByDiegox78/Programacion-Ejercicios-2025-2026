using System.Text.RegularExpressions;
using Conquistadores_de_Catán.Class;
using Conquistadores_de_Catán.Enum;
using Conquistadores_de_Catán.Structs;
using Serilog;
using static System.Console;

namespace Conquistadores_de_Catán.Service;

public class ServicioPatan {
    private const int Fila = 3;
    private const int Columna = 4;
    private const int CantidadDeCasillas = 12;
    private const int Meta = 20;
    private readonly Casilla[,] _tablero = new Casilla[Fila, Columna];
    private readonly Random _rnd = new Random();

    
    public void Simulacion() {
        Log.Information("⚔️ Iniciando Conquistadores de Catán ⚔️");
        Posicion pos;
        pos.Filas = 0;
        pos.Columnas = 0;
        InitTablero();
        PrintMatrix();
        Log.Information("--- FASE DE ASIGNACIÓN DE CASILLAS ---");
        AsignacionDeCasillas(ref pos);
        Juego();
    }
    
    private void PrintMatrix() {
        Write("   "); 
        for (var j = 0; j < _tablero.GetLength(1); j++) {
            Write($"[{j + 1, 2}]"); 
        }
        WriteLine("");
        for (var i = 0; i < _tablero.GetLength(0); i++) {
            Write($"[{i+1}]");
            for (var j = 0; j < _tablero.GetLength(1); j++) {
                if (_tablero[i, j] is not { } call) continue;
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

    private void InitTablero() {
        for (var i = 0; i < _tablero.GetLength(0); i++) {
            for (var j = 0; j < _tablero.GetLength(1); j++) {
                _tablero[i, j] = new Casilla();
                var random = _rnd.Next(1,7);
                _tablero[i, j].Valor = random;
                random = _rnd.Next(0,3);
                var recursoAleatorio = (Recurso)random;
                _tablero[i, j].Recurso = recursoAleatorio;
            }
        }
    }

    private void AsignacionDeCasillas(ref Posicion posicion) {  
        for (var i = 0; i < CantidadDeCasillas; i++) {
            WriteLine("===============================================");
            if (i % 2 == 0) {
                Log.Information($"Turno {i + 1} del humano:");
                if (i > 0) {
                    PrintMatrixAux();
                }
                AsignarCasillaHumano(ref posicion);
            }
            else {
                Log.Information($"Turno {i + 1} de Rider:");
                AsignarCasillaRider();
             
            }
        }
    }

    private void AsignarCasillaRider() {
        var seleccionado = false;
        var fila = 0;
        var columna = 0;
        while (!seleccionado) {
            fila = _rnd.Next(0, _tablero.GetLength(0));
            columna = _rnd.Next(0, _tablero.GetLength(1));
            if (_tablero[fila,columna].Duenio == null) {
                _tablero[fila, columna].Duenio = Duenio.Rider;
                seleccionado = true;
            }
        }
        Log.Information($"🤖 Rider eligio la casilla: {fila + 1}:{columna + 1}");
    }

    private void AsignarCasillaHumano(ref Posicion posicion) {
        var seleccionado = false;
        while (!seleccionado) {
            LeerEntrada("Que casilla quieres", ref posicion);
            if (_tablero[posicion.Filas, posicion.Columnas].Duenio == null) {
                _tablero[posicion.Filas, posicion.Columnas].Duenio = Duenio.Humano;
                seleccionado = true;
            } else {
                Log.Warning("¡Esa casilla ya está ocupada! Intente de nuevo.");
            }
        }
        Log.Information($"🙎 Humano eligio la casilla: {posicion.Filas + 1}:{posicion.Columnas +1 }");
    }
    private void LeerEntrada(string msg ,ref Posicion posicion) {
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
    private int LanzarDados() {
        var random = _rnd.Next(1, 7);
        return random;
    }
    private void Juego() {
        var j1 = new Jugador();
        var j2 = new Jugador();
        int random;
        Clear();
        Log.Information("--- 🎲 COMIENZA EL JUEGO DE RECOLECCIÓN ---");
        do {
            Thread.Sleep(1000);
            Log.Information("--- TURNO DEL HUMANO ---");
            random = LanzarDados(); //6
            AsignarRecurso(random, j1, j2);
        
            Thread.Sleep(1000);
            Log.Information("--- TURNO DEL RIDER ---");
        
            random = LanzarDados();
            AsignarRecurso(random, j1, j2);
            MostrarAlmacenes(j1,j2);
        
        } while (!((j1.AlmacenMadera >= Meta && j1.AlmacenTrigo >= Meta && j1.AlmacenCarbon >= Meta) || (j2.AlmacenMadera >= Meta && j2.AlmacenTrigo >= Meta && j2.AlmacenCarbon >= Meta)));
    }
    private void AsignarRecurso(int rndm, Jugador j1, Jugador j2) {
        for (var i = 0; i < _tablero.GetLength(0); i++) {
            for (var j = 0; j < _tablero.GetLength(1); j++) {
                if (_tablero[i,j].Valor == rndm) {
                    switch (_tablero[i,j].Duenio) {
                        case Duenio.Humano:
                            switch (_tablero[i,j].Recurso) {
                                case Recurso.Carbon:
                                    j1.AlmacenCarbon += _tablero[i, j].Valor;
                                    break;
                                case Recurso.Madera:
                                    j1.AlmacenMadera += _tablero[i, j].Valor;
                                    break;
                                case Recurso.Trigo:
                                    j1.AlmacenTrigo += _tablero[i, j].Valor;
                                    break;
                            }
                            break;
                        case Duenio.Rider:
                            switch (_tablero[i,j].Recurso) {
                                case Recurso.Carbon:
                                    j2.AlmacenCarbon += _tablero[i, j].Valor;
                                    break;
                                case Recurso.Madera:
                                    j2.AlmacenMadera += _tablero[i, j].Valor;
                                    break;
                                case Recurso.Trigo:
                                    j2.AlmacenTrigo += _tablero[i, j].Valor;
                                    break;
                            }
                            break;
                    }
                }
            }
        }
    }
    private void MostrarAlmacenes(Jugador j1, Jugador j2) {
        Log.Information("--- ESTADO ACTUAL DE RECURSOS ---");
        WriteLine($"🙎 Humano | Carbón: {j1.AlmacenCarbon}, Madera: {j1.AlmacenMadera}, Trigo: {j1.AlmacenTrigo}");
        WriteLine($"🤖 Rider | Carbón: {j2.AlmacenCarbon}, Madera: {j2.AlmacenMadera}, Trigo: {j2.AlmacenTrigo}");
    }
    private void PrintMatrixAux() {
        for (var i = 0; i < _tablero.GetLength(0); i++) {
            for (var j = 0; j < _tablero.GetLength(1); j++) {
                switch (_tablero[i, j].Duenio) {
                    case Duenio.Humano:
                        Write("[🤵]");
                        break;
                    case Duenio.Rider:
                        Write("[🤖]");
                        break;
                    default:
                        Write("[  ]");
                        break;
                }
            }
            WriteLine();
        }
    }
    
}
