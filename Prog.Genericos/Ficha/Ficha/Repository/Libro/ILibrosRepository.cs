using Ficha.Repository.Common;
namespace Ficha.Repository.Libro;
using Ficha.Models;


public interface ILibrosRepository : ICrudRepository<Libro, int>
{
    public int TotalLibro { get; }
    public Libro? GetLibroByAutor(string autor);

}