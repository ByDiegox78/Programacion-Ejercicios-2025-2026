namespace Ficha.Models;

public abstract record Ficha(int Id, string Nombre)
{
    public required int Id { get; init; } = Id;
    public required string Nombre { get; set; } = Nombre;
    
}