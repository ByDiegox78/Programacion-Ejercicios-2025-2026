using Vehiculos.Models;

namespace Vehiculos.Repository;

public class VehiculoRepository : IVehiculosRepository {
    private readonly Dictionary<string, Vehiculo> _vehiculos = new();
    
    
    public IEnumerable<Vehiculo> GetAll() {
        return _vehiculos.Values;
    }

    public Vehiculo? GetByMatricula(string matricula) {
        return _vehiculos.GetValueOrDefault(matricula);
    }

    public Vehiculo? Create(Vehiculo entity) {
        if (_vehiculos.ContainsKey(entity.Matricula)) return null;

        var nuevo = entity with {
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        _vehiculos.Add(entity.Matricula, nuevo);
        return nuevo;
    }

    public Vehiculo? Update(string matricula, Vehiculo entity) {
        if (!_vehiculos.TryGetValue(matricula, out var vehiculo)) return null;

        var actualizada = entity with {
            Matricula =  matricula,
            CreatedAt = vehiculo.CreatedAt,
            UpdatedAt = DateTime.UtcNow,
        };
        _vehiculos[matricula] = actualizada;
        return actualizada;
    }

    public Vehiculo? Delete(string matricula) {
        if (!_vehiculos.Remove(matricula, out var vehiculo)) return null;
        return vehiculo with {
            UpdatedAt = DateTime.UtcNow
        };
    }
}