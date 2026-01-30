using Ficha.Models;
using Ficha.Repository.Common;

namespace Ficha.Repository.Revista;
using Ficha.Models;

public interface IRevistasRepository : ICrudRepository<Revista, int>
{
    public int TotalRevista { get; }
    
    public Revista? GetRevistaByNumber(int number);

}