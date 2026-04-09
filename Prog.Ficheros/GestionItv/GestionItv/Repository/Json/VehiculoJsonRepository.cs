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
        private readonly Dictionary<string, HashSet<int>> _porDni = new();

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
                foreach (var v in vehiculos) {
                    _porId[v.Id] = v;
                    if (!v.IsDeleted) {
                        _matricula[v.Matricula] = v.Id;
                        AgregarVehiculoDni(v.DniPropietario, v.Id);
                    }
                    if (v.Id > _idCounter) _idCounter = v.Id;
                }
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


        public IEnumerable<Vehiculo> GetAll() {
            _logger.Debug("Buscando todos los vehiculos de la ITV");
            return _porId.Values.Where(v => !v.IsDeleted);
        }
        

        public Vehiculo? GetById(int id) {
            _logger.Debug("Buscando vehiculo por su matricula: {Id}", id);
            return _porId.TryGetValue(id, out var vehiculo) && !vehiculo.IsDeleted ? vehiculo : null;
        }

        public Vehiculo? Create(Vehiculo entity) {
            _logger.Debug("Creando un vehiculo {Entity}", entity);
            if (_matricula.ContainsKey(entity.Matricula)) {
                _logger.Warning("La matrícula {Matricula} ya existe", entity.Matricula);
                return null;
            }

            if (!VerificarCochePropietario(entity.DniPropietario)) {
                _logger.Warning("El propietario {Dni} ya alcanzó el límite de 3 vehículos", entity.DniPropietario);
                return null;
            }
          
            var nuevo = entity with {
                Id = ++_idCounter,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };
            _porId[nuevo.Id] = nuevo;
            _matricula[nuevo.Matricula] = nuevo.Id;
            AgregarVehiculoDni(nuevo.DniPropietario, nuevo.Id);
            Save();
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
                AgregarVehiculoDni(entity.DniPropietario, entity.Id);
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
            _idCounter = 0;
            if (File.Exists(_filePath)) {
                File.Delete(_filePath);
            }
            _logger.Information("Repositorio JSON limpiado.");
            return true;
        }

        public Vehiculo? GetByMatricula(string matricula) {
            _logger.Debug("Buscando vehiculo por matricula: {Matricula}", matricula);
            return _matricula.TryGetValue(matricula, out var id) &&
                   _porId.TryGetValue(id, out var vehiculo) && !vehiculo.IsDeleted
                ? vehiculo
                : null;
        }
        
        private bool VerificarCochePropietario(string dni) => 
            !_porDni.TryGetValue(dni, out var list) || list.Count < 3;
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