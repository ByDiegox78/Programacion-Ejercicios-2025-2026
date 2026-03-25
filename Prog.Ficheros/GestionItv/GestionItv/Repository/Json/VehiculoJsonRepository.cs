using System.Text.Json;
using GestionItv.Config;
using GestionItv.Models;
using GestionItv.Repository.Common;
using Serilog;

namespace GestionItv.Repository.Json;

public class VehiculoJsonRepository : IVehiculosRepository {
    
    private static readonly Lazy<VehiculoJsonRepository> Lazy = new(() => new VehiculoJsonRepository());

    private readonly ILogger _logger = Log.ForContext<VehiculoJsonRepository>();
    
    private readonly Dictionary<string, int> _matricula = new();
    private readonly Dictionary<int, Vehiculo> _porId = new();
    
    private int _idCounter;
    
    public static VehiculoJsonRepository Instance => Lazy.Value;
    
    private readonly string _filePath;
    
    private readonly JsonSerializerOptions _jsonOptions = new() {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
    
    private VehiculoJsonRepository() {
        _logger.Debug("Iniciado repositorio JSON.");
        _filePath = Path.Combine(Configuracion.DataFolder, "vehiculo.json");
        EnsureDirectory();
        Load();
    }

    private void Load() {
        try {
            if (!File.Exists(_filePath)) {
                _logger.Information("El archivo Json no existe.");
                return;
            }
            var json = File.ReadAllText(_filePath);
            var vehiculos = JsonSerializer.Deserialize<List<Vehiculo>>(json, _jsonOptions);
            
            if (vehiculos == null) return;
            
            
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
    
    private void EnsureDirectory() {
        var dir = Path.GetDirectoryName(_filePath);
        if (string.IsNullOrEmpty(dir) || Directory.Exists(dir)) return;
        _logger.Debug("Creando directorio: {Dir}", dir);
        Directory.CreateDirectory(dir);
    }

    private void Save() {
        try {
            var vehiculos = _porId.Values.ToList();
            var json = JsonSerializer.Serialize(vehiculos, _jsonOptions);
            File.WriteAllText(_filePath,json);
            _logger.Debug("Datos en el json: ", vehiculos.Count);
        }
        catch (Exception e) {
            _logger.Error(e, "Error de guardado");
            throw;
        }
    }


    public IEnumerable<Vehiculo> GetAll() =>  _porId.Values;
    

    public Vehiculo? GetById(int id) {
        _logger.Debug("Buscando vehiculo por su matricula: {Id}");
        return _porId.GetValueOrDefault(id);
    }

    public Vehiculo? Create(Vehiculo entity) {
        _logger.Debug("Creando un vehiculo {Entity}", entity);
        if (_matricula.ContainsKey(entity.Matricula)) return null;
        var nuevo = entity with {
            Id = ++_idCounter,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsDeleted = false
        };
        _porId[nuevo.Id] = nuevo;
        _matricula[nuevo.Matricula] = nuevo.Id;
        Save();
        return nuevo;

    }

    public Vehiculo? Update(int id, Vehiculo entity) {
        _logger.Debug("Actualizando el vehiculo: {Entity}", entity);
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
        Save();
        return actualizado;
    }

    public Vehiculo? Delete(int id) {
        // Forma Correcta (Estructurada)
        _logger.Debug("Eliminando vehiculo con id: {Id}", id);
        if (!_porId.Remove(id, out var vehiculo)) return null;

        _matricula.Remove(vehiculo.Matricula);

        var eliminado = vehiculo with {
            IsDeleted = true,
            UpdatedAt = DateTime.UtcNow
        };
        Save();
        return eliminado;
    }

    public bool DeleteAll() {
   
        _porId.Clear();
        _matricula.Clear();
        _idCounter = 0;

        if (File.Exists(_filePath)) {
            File.Delete(_filePath);
        }

        _logger.Information("Repositorio JSON limpiado.");
        return true;
    }

    public Vehiculo? GetByMatricula(string matricula) {
        throw new NotImplementedException();
    }
}