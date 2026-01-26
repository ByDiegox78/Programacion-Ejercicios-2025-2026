namespace Biblioteca.Models;

public sealed class Dvd : Ficha
{
    public enum TipoDvd
    {
        Documental,
        Pelicula
    }
    public override int Id { get; }
    public override required string Titulos { get; set; }
    public required string Director { get; set; }
    public required string Fecha { get; set; }
    public required TipoDvd Tipo { get; set; }



}