using Ficha.Repository.Common;
namespace Ficha.Repository.Dvd;
using Ficha.Models;



public interface IDvdRepository : ICrudRepository<Dvd, int>
{
    public int TotalDvd { get; }
    public Dvd? GetDvdByDirector(string director);

}