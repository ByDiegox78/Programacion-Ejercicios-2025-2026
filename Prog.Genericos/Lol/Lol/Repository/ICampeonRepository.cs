using Ficha.Collections.Lista;
using Lol.Models;
using Lol.Repository.Common;

namespace Lol.Repository;

public interface ICampeonRepository : ICrudRepository<int, Campeon> {
    Campeon? GetByHabilidad(Habilidad habilidad);
    bool ExisteNombre(string nombre);
    
    Campeon? GetByNombre(string nombre);
}