using Vehiculos.Models;

namespace Vehiculos.Service;

public interface IVehiculoService  {
    int TotalVehiculos{ get; }
    
    IEnumerable<Vehiculo> GetAll();
    
    Vehiculo GetByMatricula(string matricula);
    
    Vehiculo Save(Vehiculo vehiculo);
    
    Vehiculo Update(string matricula, Vehiculo vehiculo);
    
    Vehiculo Delete(string matricula);
}