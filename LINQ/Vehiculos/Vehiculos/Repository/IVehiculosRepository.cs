using Vehiculos.Models;
using Vehiculos.Repository.Common;

namespace Vehiculos.Repository;

public interface IVehiculosRepository : ICrudRepository<string, Vehiculo>;