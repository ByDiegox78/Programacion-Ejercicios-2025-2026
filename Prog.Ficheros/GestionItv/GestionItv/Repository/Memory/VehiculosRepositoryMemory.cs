using GestionItv.Models;
using GestionItv.Repository.Common;
using Serilog;

namespace GestionItv.Repository.Memory;

public class VehiculosRepositoryMemory: IVehiculosRepository {
    private static readonly Lazy<VehiculosRepositoryMemory> Lazy = new(() => new VehiculosRepositoryMemory());
    private readonly ILogger _logger = Log.ForContext<VehiculosRepositoryMemory>();
    private readonly Dictionary<string, int> _matricula = new();
    private readonly Dictionary<int, Vehiculo> _porId = new();
    private int _idCounter;
    private VehiculosRepositoryMemory() { }
    public static VehiculosRepositoryMemory Instance => Lazy.Value;
    
    public IEnumerable<Vehiculo> GetAll() {
        _logger.Debug("Buscando todos los vehiculos de la ITV");
        return _porId.Values;
    }

    public Vehiculo? GetById(int id) {
        _logger.Debug($"Buscando vehiculo por su matricula: {id}");
        return _porId.GetValueOrDefault(id);
    }

    public Vehiculo? GetByMatricula(string matricula) {
        return _matricula.TryGetValue(matricula, out var id) && _porId.TryGetValue(id, out var vehiculo)
            ? vehiculo
            : null;
    }

    public Vehiculo? Create(Vehiculo entity) {
        _logger.Debug($"Creando un vehiculo {entity}", entity);
        if (_matricula.ContainsKey(entity.Matricula)) return null;
        var nuevo = entity with {
            Id = ++_idCounter,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsDeleted = false
        };
        _porId[nuevo.Id] = nuevo;
        _matricula[nuevo.Matricula] = nuevo.Id;
        return nuevo;

    }

    public Vehiculo? Update(int id, Vehiculo entity) {
        _logger.Debug($"Actualizando el vehiculo: {entity}", entity);
        if (!_porId.TryGetValue(id, out var actual)) return null;
        
        if (entity.Matricula != actual.Matricula && _matricula.TryGetValue(entity.Matricula, out var otroId) && otroId != id) {
            _logger.Warning("No se puede actualizar persona con id {Id} porque el DNI {Dni} ya está en uso por otra persona", id, entity.Id);
            return null;
        }

        var actualizado = entity with {
            Id = id,
            CreatedAt = actual.CreatedAt,
            UpdatedAt = DateTime.UtcNow,
            IsDeleted = false
        };
        _porId[id] = actualizado;
        if (actual.Matricula != actualizado.Matricula) {
            _matricula.Remove(actual.Matricula);
            _matricula[actualizado.Matricula] = id;
        }
        return actualizado;
    }

    public Vehiculo? Delete(int id) {
        _logger.Debug($"Eliminando vehiculo con id {id}", id);
        if (!_porId.Remove(id, out var vehiculo)) return null;

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