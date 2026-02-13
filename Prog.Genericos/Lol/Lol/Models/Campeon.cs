namespace Lol.Models;

public abstract record Campeon {
    public int Id { get; init; }
    public string Nombre { get; init; }
    public int Nivel  { get; set; }
    public decimal PrecioEsencias  { get; init; }
    public HashSet<Habilidad> HabilidadCampeon { get; init; }
}