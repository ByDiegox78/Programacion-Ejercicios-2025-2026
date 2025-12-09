namespace FunkoPop.Models;

public record Funko {
    public enum CategoriaFunko { Heroe, Anime, Disnay}
    public int Id { get; init; } = 0;
    public required string Nombre { get; init; }
    public required decimal Precio { get; set; }
    public required CategoriaFunko Categoria  { get; init; }
}