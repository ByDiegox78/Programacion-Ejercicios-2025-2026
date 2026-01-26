namespace Biblioteca.Models;

public sealed class Libro : Ficha {
    public override int Id { get; }
    public override required string Titulos { get; set; }
    public required string Autor { get; set; }
    public required string Editorial { get; init; }
    
    
}