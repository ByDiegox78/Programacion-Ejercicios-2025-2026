using GestionItv.Models;

namespace GestionItv.Repository.Common;

public interface IVehiculosRepository : ICrudRepository<string, Vehiculo> {
    bool DeleteAll();
}