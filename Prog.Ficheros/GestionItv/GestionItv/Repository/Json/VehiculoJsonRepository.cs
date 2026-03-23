using System.Text.Json;
using GestionItv.Config;
using GestionItv.Models;
using GestionItv.Repository.Common;
using Serilog;

namespace GestionItv.Repository.Json;

public class VehiculoJsonRepository : IVehiculosRepository {
    
    private static readonly Lazy<VehiculoJsonRepository> Lazy = new(() => new VehiculoJsonRepository());

    private readonly ILogger _logger = Log.ForContext<VehiculoJsonRepository>();
    
    private readonly Dictionary<string, Vehiculo> _vehiculos = new Dictionary<string, Vehiculo>();
    
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
        _logger.Debug("Creando directorio: {dir}", dir);
        Directory.CreateDirectory(dir);
    }
    
    
    public IEnumerable<Vehiculo> GetAll() {
        
    }

    public Vehiculo? GetByMatricula(string matricula) {
        throw new NotImplementedException();
    }

    public Vehiculo? Create(Vehiculo entity) {
        throw new NotImplementedException();
    }

    public Vehiculo? Update(string matricula, Vehiculo entity) {
        throw new NotImplementedException();
    }

    public Vehiculo? Delete(string matricula) {
        throw new NotImplementedException();
    }

    public bool DeleteAll() {
        throw new NotImplementedException();
    }
}