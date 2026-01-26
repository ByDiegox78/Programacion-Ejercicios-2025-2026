using Ficha.Repository.Common;

namespace Ficha.Repository.Dvd;

public interface IDvdRepository : ICrudRepository<Models.Dvd, int>
{
    public int TotalDvd { get; }
}