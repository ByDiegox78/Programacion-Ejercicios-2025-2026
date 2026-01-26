using Ficha.Repository.Common;
namespace Ficha.Repository.Libro;
using Ficha.Models;


public interface ILibrosRepository : ICrudRepository<Libro, int>
{
    public int TotalDvd { get; }

}