using System.Text.RegularExpressions;

namespace Biblioteca.Models;

public abstract class Ficha {
    private static int _idContador = 0;
    public virtual int Id { get; }

    public virtual required string Titulos { get; set ; }
    protected Ficha()
    {
        Id = _idContador++;
    }
}