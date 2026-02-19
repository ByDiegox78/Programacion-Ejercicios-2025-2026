using Vehiculos.Errors;
using Vehiculos.Models;
using Vehiculos.Repository;
using Vehiculos.Validator;
using Vehiculos.Cache;

namespace Vehiculos.Service;

public class VehiculoService(ICached<string, Vehiculo> cache, IValidador<Vehiculo> cocheValidador, IValidador<Vehiculo> motoValidador, IValidador<Vehiculo> camionValidador, IVehiculosRepository repository ) : IVehiculoService {
    
    
    public int TotalVehiculos => repository.GetAll().Count();
    
    public IEnumerable<Vehiculo> GetAll() {
        return repository.GetAll();
    }

    public Vehiculo GetByMatricula(string matricula) {
        var cached = cache.Get(matricula);
        if (cached != null) return cached;
        var vehiculo = repository.GetByMatricula(matricula) ?? throw new VehiculosException.VehiculoNotFoundException(matricula);
        cache.Add(matricula, vehiculo);
        return vehiculo;
    }

    public Vehiculo Save(Vehiculo vehiculo) {
        var errores = vehiculo switch {
            Coche => cocheValidador.Validar(vehiculo),
            Camion => camionValidador.Validar(vehiculo),
            Moto => motoValidador.Validar(vehiculo),
            _ => ["Tipo de entidad no soportada para validación."]
        };
        var nuevo =  repository.Create(vehiculo) ?? throw new VehiculosException.InvalidMatriculaException(vehiculo.Matricula);
        cache.Add(nuevo.Matricula, nuevo );
        return nuevo;
    }

    public Vehiculo Update(string matricula, Vehiculo vehiculo) {
        var errores = vehiculo switch {
            Coche => cocheValidador.Validar(vehiculo),
            Camion => camionValidador.Validar(vehiculo),
            Moto => motoValidador.Validar(vehiculo),
            _ => ["Tipo de entidad no soportada para validación."]
        };
        var actualizada = repository.Update(matricula, vehiculo) ??
                          throw new VehiculosException.VehiculoNotFoundException(matricula);
        cache.Remove(matricula);
        return actualizada;
    }

    public Vehiculo Delete(string matricula) {
        var eliminado = repository.Delete(matricula) ?? throw new VehiculosException.VehiculoNotFoundException(matricula);
        cache.Remove(matricula);
        return eliminado;
    }

    public InformeVehiculos GenerarInformaDeVehiculos() {
        var coche = GetAllCoches().Count;
        var moto = GetAllMotos().Count;
        var camiones = GetAllCamiones().Count;
        var masFamosa = repository.GetAll()
            .GroupBy(p => p.Marca)
            .OrderByDescending(p => p.Count())
            .Select(g => g.Key)
            .First()
            .ToString();
        var camionPesado = GetAllCamiones().Where(c => c.AñoMatriculacion > 2015).Average(c => c.PesoMaximo);
        return new InformeVehiculos(
            coche,
            moto,
            camiones,
            masFamosa,
            camionPesado
        );
    }

    public List<Coche> GetAllCoches() => repository.GetAll().OfType<Coche>().ToList();
    

    public List<Moto> GetAllMotos() => repository.GetAll().OfType<Moto>().ToList();



    public List<Camion> GetAllCamiones() => repository.GetAll().OfType<Camion>().ToList();

    private void masFamosa() => repository.GetAll()
        .GroupBy(p => p.Marca)
        .OrderByDescending(p => p.Count())
        .Select(g => new {
            Marca = g.Key,
            Camtidad = g.Count()
        }).First();
}