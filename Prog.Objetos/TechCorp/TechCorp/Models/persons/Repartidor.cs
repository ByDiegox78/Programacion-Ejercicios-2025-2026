using TechCorp.Models.Interface;

namespace TechCorp.Models;

public record Repartidor: Trabajador, ITrazarRuta, IConfirmarEntrega {
    public required string Barrio { get; init; }
    public void TrazarRuta() {
        Console.WriteLine($"Trazando ruta de {Barrio}");
    }

    public void ConfirmarEntrega() {
        Console.WriteLine($"Confirmando entrega de {Barrio}");
    }
}