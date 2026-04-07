using GestionItv.Models;
using GestionItv.Repository.Common;
using GestionItv.Storage.Common;

namespace GestionItv.Service;

public class VehiculoService(
    IVehiculosRepository repository,
    IStorage<Vehiculo> storage,
    
    
    ) : IVehiculoService {
    public int TotalVehiculos { get; }
    public IEnumerable<Vehiculo> GetAll() {
        throw new NotImplementedException();
    }

    public Vehiculo GetById(int id) {
        throw new NotImplementedException();
    }

    public Vehiculo GetByMatricula(string matricula) {
        throw new NotImplementedException();
    }

    public Vehiculo Save(Vehiculo vehiculo) {
        throw new NotImplementedException();
    }

    public Vehiculo Update(int id, Vehiculo vehiculo) {
        throw new NotImplementedException();
    }

    public Vehiculo Delete(int id) {
        throw new NotImplementedException();
    }

    public Vehiculo HardDelete(int id) {
        throw new NotImplementedException();
    }

    public Vehiculo DeleteAll(int id) {
        throw new NotImplementedException();
    }

    public int ImportarDatos() {
        throw new NotImplementedException();
    }

    public int ExportarDatos() {
        throw new NotImplementedException();
    }
}