using Ficha.Models;
using Ficha.Repository.Common;

namespace Ficha.Repository.Revista;

public interface IRevistasRepository : ICrudRepository<Models.Revista, int>
{
    public int TotalRevista { get; }

}