using StarWars.Factory;
using StarWars.Models;

namespace StarWars.Service;

    

public class DroidService {
    private readonly Random _rand = new Random();
    private readonly Droide[] _enemies;
    private readonly Droide?[,] _droidMap;
    private readonly int _mapSize; 
    private readonly int _totalEnemigos; 
    private readonly int _timeMax;
    private int _disparosRealizados;
    private int _aciertos;
    private int _enemigosDestruidos;
    private Droide[] _enemigosOrdenados;
   
    
    public DroidService(int mapSize, int totalEnemigos, int timeMax) {
        _mapSize = mapSize; //Configuration.MapSize (7)
        _totalEnemigos = totalEnemigos; //Configuration.totalEnemigos (10)
        _timeMax = timeMax; // Configuration.time
        _droidMap = new Droide?[mapSize, mapSize]; // Mapa 7x7
        _enemies = new Droide[totalEnemigos]; //Vector de 10 ennegimos
        for (var i = 0; i < totalEnemigos; i++)
            _enemies[i] = DroidFactory.RandDroid();//Llenamos con los tipos, ahora ${_enemies} tiene droides
        _enemigosOrdenados = _enemies;

    }

    public Reporte Report =>
        new(
        _mapSize,
        _totalEnemigos,
        _timeMax,
        _disparosRealizados,
        _aciertos,
        _enemigosDestruidos,
        _enemigosOrdenados
        );

    public void Simulation() {
        var time = 0;
        InitMap();

        while (time < _timeMax * 1000 && IsAllDead()) {
            PrintMatrix();
            if (time % 300 == 0) {
                MoverDroides();
            }
            Thread.Sleep(100);
            time += 100;
            var f = _rand.Next(0, _mapSize);
            var c = _rand.Next(0, _mapSize);
            var danio = Disparar();
            if (_droidMap[f, c] is { } nave) {
                _aciertos++;
                switch (nave.Tipo) {
                    case Droide.TipoDroide.Sw348:
                        nave.Defenderse(danio);
                        Console.WriteLine($"La Energia actual de Sw348 es de {nave.EnergiaMaxima}");
                        break;
                    case Droide.TipoDroide.Sw447:
                        nave.UsarEscudo(danio);
                        Console.WriteLine($"La Energia actual de Sw447 es de {nave.EnergiaMaxima}");
                        break;
                    case Droide.TipoDroide.Sw421:
                        if (!nave.Move()) {
                            nave.EnergiaMaxima -= danio;
                            Console.WriteLine($"La Energia actual de Sw421 es de {nave.EnergiaMaxima}");
                        }
                        break;
                }
                
            }
        }
        _enemigosOrdenados = ShellSort(_enemies);
        _enemigosDestruidos = EnemigosDestruidos();



    }

    
    private void InitMap() {
        for (int i = 0; i < _droidMap.GetLength(0); i++) {
            for (int j = 0; j < _droidMap.GetLength(1); j++) {
                _droidMap[i, j] = null;
            }
        }
        var droidesColocados = 0;
        while (droidesColocados < _totalEnemigos) {
            var f = _rand.Next(0, _mapSize);
            var c = _rand.Next(0, _mapSize);

            if (_droidMap[f, c] != null) continue;
            _droidMap[f,c] = _enemies[droidesColocados];
            
            droidesColocados += 1;

        }
    }

    private void PrintMatrix() {
        for (int i = 0; i < _droidMap.GetLength(0); i++) {
            for (int j = 0; j < _droidMap.GetLength(1); j++) {
                if (_droidMap[i, j] is {} nave) {
                    switch (nave.Tipo) {
                        case Droide.TipoDroide.Sw348:
                            Console.Write("[🔴]");
                            break;
                        case Droide.TipoDroide.Sw447:
                            Console.Write("[🔵]");
                            break;
                        case Droide.TipoDroide.Sw421:
                            Console.Write("[🟢]");
                            break;
                        
                    }
                } else if (_droidMap[i, j] is null) {
                    Console.Write("[  ]");
                }
            }
            Console.WriteLine();
        }
    }

    private void MoverDroides() {
        for (int i = 0; i < _droidMap.GetLength(0); i++) {
            for (int j = 0; j < _droidMap.GetLength(1); j++) {
                _droidMap[i, j] = null;
            }
        }

        for (var i = 0; i < _enemies.GetLength(0); i++) {
            var enemie = _enemies[i];
            if (!enemie.IsAlive) continue;
            var colocado = false;
            do {
                var f = _rand.Next(0, _mapSize);
                var c = _rand.Next(0, _mapSize);
                if (_droidMap[f, c] != null) continue;
                _droidMap[f, c] = enemie;
                colocado = true;
            } while (!colocado);
        }
    }
    private int Disparar() {
        _disparosRealizados++;
        if (new Random().Next(0, 101) <= 15) {
            Console.WriteLine("¡Has conseguido un disparo crítico!");
            return 50;
        }

        Console.WriteLine("Disparo normal.");
        return 25;
    }

    private bool IsAllDead() {
        for (int i = 0; i < _enemies.GetLength(0); i++) {
            if (_enemies[i].IsAlive) {
                return true;
            }
        }
        return false;
    }

    private int EnemigosDestruidos() {
        var count = 0;
        for (int i = 0; i < _enemies.GetLength(0); i++) {
            if (!_enemies[i].IsAlive) {
                count++;
            }
        }
        return count;
    }
    private Droide[] ShellSort(Droide[] arr) {
        
        int n = arr.Length;
        int gap = n / 2;
        while (gap > 0) {
            for (int i = gap; i < n; i++) {
                Droide temp = arr[i];
                int j = i;
                while (j >= gap && arr[j - gap].EnergiaMaxima > temp.EnergiaMaxima) {
                    arr[j] = arr[j - gap];
                    j = j - gap;
                }
                arr[j] = temp;
            }
            gap = gap / 2;
        }
        return arr;
    }
   



}