using GestionItv.Models;
using GestionItv.Repository.Common;
using Serilog;

namespace GestionItv.Repository.Memory;

public class VehiculosRepositoryMemory: IVehiculosRepository {
    private static readonly Lazy<VehiculosRepositoryMemory> Lazy = new(() => new VehiculosRepositoryMemory());
    private readonly ILogger _logger = Log.ForContext<VehiculosRepositoryMemory>();
    private readonly Dictionary<string, Vehiculo> _matricula = new();
    
    public IEnumerable<Vehiculo> GetAll() {
        _logger.Debug("Buscando todos los vehiculos de la ITV");
        return _matricula.Values;
    }

    public Vehiculo? GetByMatricula(string matricuña) {
        _logger.Debug($"Buscando vehiculo por su matricula: {matricuña}");
        return _matricula.GetValueOrDefault(matricuña);
    }

    public Vehiculo? Create(Vehiculo entity) {
        _logger.Debug($"Creando un vehiculo {entity}", entity);
        if (_matricula.ContainsValue(entity)) return null;
        var nuevo = entity with {
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsDeleted = false
        };
        _matricula.Add(nuevo.Matricula, nuevo);
        return nuevo;

    }

    public Vehiculo? Update(string matricula, Vehiculo entity) {
        _logger.Debug($"Actualizando el vehiculo: {entity}", entity);
        if (!_matricula.TryGetValue(matricula, out var actual)) return null;

        var actualizado = entity with {
            CreatedAt = actual.CreatedAt,
            UpdatedAt = DateTime.UtcNow,
            IsDeleted = false
        };

        _matricula[matricula] = actualizado;
        return actualizado;
    }

    public Vehiculo? Delete(string matricula) {
        _logger.Debug($"Eliminando vehiculo con matricula {matricula}", matricula);
        if (!_matricula.Remove(matricula, out var vehiculo)) return null;

        _matricula.Remove(vehiculo.Matricula);

        return vehiculo with {
            IsDeleted = true,
            UpdatedAt = DateTime.UtcNow
        };
    }
    
    public bool DeleteAll() {
        _logger.Warning("Eliminando permanentemente todos los vehiculos");
        _matricula.Clear();
        return true;
    }
}