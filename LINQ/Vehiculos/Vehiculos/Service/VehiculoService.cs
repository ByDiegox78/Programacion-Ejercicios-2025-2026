using Vehiculos.Errors;
using Vehiculos.Models;
using Vehiculos.Repository;
using Vehiculos.Validator;

namespace Vehiculos.Service;

public class VehiculoService(IValidador<Vehiculo> cocheValidador, IValidador<Vehiculo> motoValidador, IValidador<Vehiculo> camionValidador, IVehiculosRepository vehiculosRepository ) : IVehiculoService {
    
    
    public int TotalVehiculos => vehiculosRepository.GetAll().Count();
    
    public IEnumerable<Vehiculo> GetAll() {
        return vehiculosRepository.GetAll();
    }

    public Vehiculo GetByMatricula(string matricula) {
        var vehiculo = vehiculosRepository.GetByMatricula(matricula) ?? throw new VehiculosException.VehiculoNotFoundException(matricula);
        return vehiculo;
    }

    public Vehiculo Save(Vehiculo vehiculo) {
        var errores = vehiculo switch {
            Coche => cocheValidador.Validar(vehiculo),
            Camion => camionValidador.Validar(vehiculo),
            Moto => motoValidador.Validar(vehiculo),
            _ => ["Tipo de entidad no soportada para validación."]
        };
        var nuevo =  vehiculosRepository.Create(vehiculo) ?? throw new VehiculosException.InvalidMatriculaException(vehiculo.Matricula);
        
    }

    public Vehiculo Update(string matricula, Vehiculo vehiculo) {
        throw new NotImplementedException();
    }

    public Vehiculo Delete(string matricula) {
        throw new NotImplementedException();
    }
}