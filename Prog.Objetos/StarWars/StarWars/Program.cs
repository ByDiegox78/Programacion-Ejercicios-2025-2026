using StarWars.Config;
using StarWars.Models;
using StarWars.Service;
Console.OutputEncoding = System.Text.Encoding.UTF8;

Main();

void Main() {
    Configuration.ParseArguments(args);
    var servicio = new DroidService(
        Configuration.MapSize,
        Configuration.NumberOfEnemies,
        Configuration.Time
    );
    servicio.Simulation();
    MostrarReporte(servicio.Report);
    

}
void MostrarReporte(Reporte r) {
    Console.WriteLine("\n===== REPORTE DE LA SIMULACIÓN =====");
    Console.WriteLine($"🗺️  Tamaño del mapa       : {r.MapSize} x {r.MapSize}");
    Console.WriteLine($"🤖 Droides iniciales      : {r.NumberOfDroids}");
    Console.WriteLine($"⏱️  Tiempo máximo (seg)   : {r.TimeMax}");
    Console.WriteLine($"🔫 Disparos realizados    : {r.DisparosRealizados}");
    Console.WriteLine($"🎯 Aciertos               : {r.Aciertos}");
    Console.WriteLine($"💥 Droides destruidos     : {r.DroidesDestruidos}");
    Console.WriteLine("Enemigos:");
    for (int i = 0; i < r.OrdenPorEnergia.GetLength(0); i++) {
        Console.WriteLine($"Enemigo {i + 1}: {r.OrdenPorEnergia[i]}");
    }
    Console.WriteLine("===================================");
    
}
