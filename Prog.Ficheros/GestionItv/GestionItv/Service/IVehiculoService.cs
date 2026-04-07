using GestionItv.Models;

namespace GestionItv.Service;

public interface IVehiculoService {
    
    int TotalVehiculos { get; }

    IEnumerable<Vehiculo> GetAll();

    Vehiculo GetById(int id);

    Vehiculo GetByMatricula(string matricula);

    Vehiculo Save(Vehiculo vehiculo);

    Vehiculo Update(int id, Vehiculo vehiculo);

    Vehiculo Delete(int id);

    Vehiculo HardDelete(int id);

    bool DeleteAll();

    int ImportarDatos();
    /// <summary>
    /// Exporta el listado de vehiculos al storage correspondiente
    /// </summary>
    /// <returns></returns>
    int ExportarDatos();
    
}