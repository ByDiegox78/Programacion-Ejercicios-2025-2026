using RepositorioAleatorio.Models;
using RepositorioAleatorio.Repository.Common;

namespace RepositorioAleatorio.Repository;

public interface IPersonasRepository : ICrudRepository<int, Persona>;