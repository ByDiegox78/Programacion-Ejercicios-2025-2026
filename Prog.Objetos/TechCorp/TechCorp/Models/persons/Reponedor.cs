using TechCorp.Models.Interface;

namespace TechCorp.Models;

public record Reponedor : Trabajador, ILocalizar, IActualizarStock {
    public char Sector { get; init; }

    public void Localizar() {
        Console.WriteLine($"Localizando paquete en sector {Sector}");
    }

    public void ActualizarStock() {
        Console.WriteLine("Actualizando stock");
    }
}