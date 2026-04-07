using GestionItv.Models;

namespace GestionItv.Repository.Common;

public interface IVehiculosRepository : ICrudRepository<int, Vehiculo> {
    bool DeleteAll();
    Vehiculo? HardDelete(int id);
    Vehiculo? GetByMatricula(string matricula);
}