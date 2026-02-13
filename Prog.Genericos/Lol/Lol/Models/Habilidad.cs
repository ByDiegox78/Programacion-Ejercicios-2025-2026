namespace Lol.Models;

public record Habilidad {
    public string Nombre { get; set; }
    public TeclaHabilidad Tecla { get; init; }
    public HashSet<TipoDaño> Daño { get; init; }
    public int Cooldawn { get; set; }
}