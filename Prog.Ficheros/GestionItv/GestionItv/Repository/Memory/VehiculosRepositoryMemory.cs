using GestionItv.Models;
using GestionItv.Repository.Common;
using Serilog;

namespace GestionItv.Repository.Memory;

public class VehiculosRepositoryMemory: IVehiculosRepository {
    private static readonly Lazy<VehiculosRepositoryMemory> Lazy = new(() => new VehiculosRepositoryMemory());
    private readonly ILogger _logger = Log.ForContext<VehiculosRepositoryMemory>();
    private readonly Dictionary<string, int> _matricula = new();
    private readonly Dictionary<int, Vehiculo> _porId = new();
    private readonly Dictionary<string, HashSet<int>> _porDni = new();
    private int _idCounter;
    private VehiculosRepositoryMemory() { }
    public static VehiculosRepositoryMemory Instance => Lazy.Value;
    
    public IEnumerable<Vehiculo> GetAll() {
        _logger.Debug("Buscando todos los vehiculos de la ITV");
        return _porId.Values.Where(v => !v.IsDeleted);
    }

    public Vehiculo? GetById(int id) {
        _logger.Debug("Buscando vehiculo por su matricula: {Id}", id);
        return _porId.TryGetValue(id, out var vehiculo) && !vehiculo.IsDeleted ? vehiculo : null;
    }
    
    public Vehiculo? GetByMatricula(string matricula) {
        return _matricula.TryGetValue(matricula, out var id) &&
               _porId.TryGetValue(id, out var vehiculo) && !vehiculo.IsDeleted
            ? vehiculo
            : null;
    }

    public Vehiculo? Create(Vehiculo entity) {
        _logger.Debug("Creando un vehiculo {Entity}", entity);
        if (_matricula.ContainsKey(entity.Matricula) || !VerificarCochePropietario(entity.DniPropietario)) return null;
        
        
        var nuevo = entity with {
            Id = ++_idCounter,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsDeleted = false
        };
        _porId[nuevo.Id] = nuevo;
        _matricula[nuevo.Matricula] = nuevo.Id;
        AgregarVehiculoDni(nuevo.DniPropietario, nuevo.Id);
        return nuevo;
    }

    public Vehiculo? Update(int id, Vehiculo entity) {
        _logger.Debug("Actualizando el vehiculo: {Entity}", entity);
        if (!_porId.TryGetValue(id, out var actual)) return null;
        if (entity.Matricula != actual.Matricula && _matricula.TryGetValue(entity.Matricula, out var otroId) && otroId != id) {
            _logger.Warning("No se puede actualizar el vehículo con id {Id} porque la matrícula {Matricula} ya está en uso por otro vehículo",
                id, entity.Matricula); 
            return null;
        }
        if (entity.DniPropietario != actual.DniPropietario) {
            if (!VerificarCochePropietario(entity.DniPropietario)) {
                _logger.Warning("El propietario con DNI {Dni} ya tiene 3 vehículos", entity.DniPropietario);
                return null;
            }
            QuitarVehiculoDni(actual.DniPropietario,actual.Id);
            AgregarVehiculoDni(entity.DniPropietario, id);
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
        _logger.Debug("Eliminando vehiculo con id {Id}", id);
        if (!_porId.TryGetValue(id, out var vehiculo)) return null;
        
        var eliminado =  vehiculo with {
            IsDeleted = true,
            UpdatedAt = DateTime.UtcNow
        };
        _porId[id] = eliminado;
        return eliminado;
    }
    
    public Vehiculo? HardDelete(int id) {
        if (!_porId.Remove(id, out var vehiculo)) return null;

        _matricula.Remove(vehiculo.Matricula);
        QuitarVehiculoDni(vehiculo.DniPropietario, vehiculo.Id);
    
        return vehiculo;
    }
    
    public bool DeleteAll() {
        _logger.Warning("Eliminando permanentemente todos los vehiculos");
        _matricula.Clear();
        _porId.Clear();
        _porDni.Clear();
        _idCounter = 0;

        return true;
    }

    private bool VerificarCochePropietario(string dni) { 
        return !_porDni.TryGetValue(dni, out var list) || list.Count < 4;
    }
    private void AgregarVehiculoDni(string dni, int id) {
        if (!_porDni.TryGetValue(dni, out var lista)) {
            lista = new HashSet<int>();
            _porDni[dni] = lista;
        }
        lista.Add(id);
    }
    private void QuitarVehiculoDni(string dni, int id) {
        if (_porDni.TryGetValue(dni, out var lista)) {
            lista.Remove(id);
            if (lista.Count == 0) _porDni.Remove(dni);
        }
    }
}