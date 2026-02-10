namespace TechCorp.Models;

public record Equipo {
    public required List<Trabajador> ListaDeEquipo { get; set; }
    public required Trabajador Jefe { get; set; }
}