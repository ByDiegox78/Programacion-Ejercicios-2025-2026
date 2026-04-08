using System.Text;
using GestionItv.Models;
using GestionItv.Repository.Common;
using Serilog;

namespace GestionItv.Repository.Binary;

public class VehiculoBinSecRepository : IVehiculosRepository {
    private static readonly Lazy<VehiculoBinSecRepository> Lazy = new(() => new VehiculoBinSecRepository());
    private const string FilePath = "Data/vehiculos_sec.dat";
    private readonly ILogger _logger = Log.ForContext<VehiculoBinSecRepository>();

    private static int _nextId = 1;
    
    private readonly Dictionary<string, int> _matricula = new();
    private readonly Dictionary<int, Vehiculo> _porId;
    private readonly Dictionary<string, HashSet<int>> _porDni = new();
    public static VehiculoBinSecRepository Instance => Lazy.Value;
    public VehiculoBinSecRepository() {
        if (!Directory.Exists("Data")) {
            Directory.CreateDirectory("Data");
        }

        _porId = Load();
    }
    
    public IEnumerable<Vehiculo> GetAll() {
        _logger.Debug("Buscando todos los vehiculos de la ITV");
        return _porId.Values.Where(v => !v.IsDeleted);
    }

    public Vehiculo? GetById(int id) {
        _logger.Debug("Buscando vehiculo por su matricula: {Id}", id);
        return _porId.TryGetValue(id, out var vehiculo) && !vehiculo.IsDeleted ? vehiculo : null;
    }

    public Vehiculo? Create(Vehiculo entity) {
        if (_matricula.ContainsKey(entity.Matricula) || !VerificarCochePropietario(entity.DniPropietario)) return null;
        var id = _nextId++;
        var nuevo = entity with {
            Id = id,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsDeleted = false
        };
        _porId[id] = nuevo;
        _matricula[nuevo.Matricula] = nuevo.Id;
        AgregarVehiculoDni(nuevo.DniPropietario, nuevo.Id);
        Save();
        return nuevo;
    }

    public Vehiculo? Update(int id, Vehiculo entity) {
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
        Save();
        return actualizado;
    }

    public Vehiculo? Delete(int id) {
        _logger.Debug("Eliminando vehiculo con id: {Id}", id);
        if (!_porId.TryGetValue(id, out var vehiculo)) return null;
        
        _matricula.Remove(vehiculo.Matricula);
        QuitarVehiculoDni(vehiculo.DniPropietario, vehiculo.Id);
        var eliminado = vehiculo with {
            IsDeleted = true,
            UpdatedAt = DateTime.UtcNow
        };
        _porId[id] = eliminado;
        Save();
        return eliminado;
        
    }
    public Vehiculo? HardDelete(int id) {
        if (!_porId.Remove(id, out var vehiculo)) return null;

        _matricula.Remove(vehiculo.Matricula);
        QuitarVehiculoDni(vehiculo.DniPropietario, vehiculo.Id);
        Save();
        return vehiculo;
    }

    public bool DeleteAll() {
        _porId.Clear();
        _matricula.Clear(); 
        _porDni.Clear();
        _nextId = 0;
        if (File.Exists(FilePath)) File.Delete(FilePath);
        _logger.Information("Repositorio BIN limpiado.");
        return true;
    }

    public Vehiculo? GetByMatricula(string matricula) {
        return _matricula.TryGetValue(matricula, out var id) &&
               _porId.TryGetValue(id, out var vehiculo) && !vehiculo.IsDeleted
            ? vehiculo
            : null;  
    }

    private Dictionary<int, Vehiculo> Load() {
        if (!File.Exists(FilePath)) {
            return new Dictionary<int, Vehiculo>();
        }

        using var stream = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
        using var reader = new BinaryReader(stream, Encoding.UTF8);

        var cantidad = reader.ReadInt32();
        _nextId = reader.ReadInt32();

        var vehiculos = new Dictionary<int, Vehiculo>();

        for (int i = 0; i < cantidad; i++) {
            var id = reader.ReadInt32();
            var matricula = reader.ReadString();
            var marca = reader.ReadString();
            var cilindrada = reader.ReadInt32();
            var motor = (Motor)reader.ReadInt32();
            var dni = reader.ReadString();
            var isDelete = reader.ReadBoolean();
            var createAt = DateTime.Parse(reader.ReadString());
            var updateAt = DateTime.Parse(reader.ReadString());
            var vehiculo = new Vehiculo(id, matricula, marca, cilindrada, motor, dni, isDelete, createAt, updateAt);
            vehiculos[id] = vehiculo;
            _matricula[matricula] = id;
            AgregarVehiculoDni(dni, id);
        }

        return vehiculos;
    }

    private void Save() {
        using var stream = new FileStream(FilePath, FileMode.Create, FileAccess.Write);
        using var writer = new BinaryWriter(stream, Encoding.UTF8);
        
        writer.Write(_porId.Count);
        writer.Write(_nextId);

        foreach (var v in _porId.Values) {
            writer.Write(v.Id);
            writer.Write(v.Matricula);
            writer.Write(v.Marca);
            writer.Write(v.Cilindrada);
            writer.Write((int)v.TipoMotor);
            writer.Write(v.DniPropietario);
            writer.Write(v.IsDeleted);
            writer.Write(v.CreatedAt.ToString("O")); 
            writer.Write(v.UpdatedAt.ToString("O"));
            
        }
    }
    
    private bool VerificarCochePropietario(string dni) { 
        return !_porDni.TryGetValue(dni, out var list) || list.Count < 3;
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