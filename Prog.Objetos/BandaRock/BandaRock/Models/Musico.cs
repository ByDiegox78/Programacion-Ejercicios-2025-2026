namespace BandaRock.Models;

public abstract record Musico {
    public int Id  { get; init; }
    public required string Nombre  { get; set; }
    public required int Tiempo { get; set; }

    public abstract void Ensayar();
    public abstract void Afinar();
}