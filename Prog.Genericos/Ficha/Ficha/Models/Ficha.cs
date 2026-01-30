namespace Ficha.Models;

public abstract record Ficha
{
    public int Id { get; init; } = 0;
    public required string Nombre { get; set; }
    
}