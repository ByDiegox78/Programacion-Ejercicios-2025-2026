using Ficha.Models;
using Ficha.Repository.Common;

namespace Ficha.Repository.Revista;

public interface IRevistasRepository : ICrudRepository<Revistas, int>
{
    public int TotalDvd { get; }

}