namespace Biblioteca.Models;

public class Revistas : Ficha
{
    public override int Id { get; }
    public override required string Titulos { get; set; }
    public required string Fecha { get; set; }
    public required int NumeroDeRevista { get; set; }
}