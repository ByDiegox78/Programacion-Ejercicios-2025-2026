using StarWars.Config;
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
}