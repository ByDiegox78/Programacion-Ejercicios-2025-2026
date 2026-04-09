using GestionItv.Config;
using GestionItv.Exceptions.Vehiculos;
using GestionItv.Models;
using GestionItv.Repository.Common;
using GestionItv.Service.Backup;
using GestionItv.Storage.Common;
using GestionItv.Validator;
using Productos.Cache;
using Serilog;

namespace GestionItv.Service;

public class VehiculoService(
    IVehiculosRepository repository,
    IStorage<Vehiculo> storage,
    ICached<int, Vehiculo> cache,
    IVehiculoValidator<Vehiculo> validator,
    IBackupService backupService
    ) : IVehiculoService {
    private readonly ILogger _logger = Log.ForContext<VehiculoService>();
    private readonly IBackupService _backupService = backupService;

    public int TotalVehiculos => repository.GetAll().Count();
    public IEnumerable<Vehiculo> GetAll() {
        _logger.Information("Obteniendo todas los vehiculos.");
        return repository.GetAll();
    }

    public Vehiculo GetById(int id) {
        _logger.Information("Obteniendo vehiculo con ID {id}", id);
        
        var cached = cache.Get(id);
        if (cached != null) return cached;
        
        var vehiculo = repository.GetById(id) ?? throw new VehiculoException.NotFound(id.ToString());
        cache.Add(id, vehiculo);
        
        return vehiculo;
    }

    public Vehiculo GetByMatricula(string matricula) {
        var listVehiculo = repository.GetAll().ToList();
        var v = listVehiculo.FirstOrDefault(v => v.Matricula == matricula);
        if (v == null) {
            throw new VehiculoException.NotFound(matricula);
        }
        var cached = cache.Get(v.Id);
        if (cached != null) return cached;
        
        var vehiculo = repository.GetById(v.Id) ?? throw new VehiculoException.NotFound(v.Id.ToString());
        cache.Add(v.Id, vehiculo);
        
        return vehiculo;
    }

    public Vehiculo Save(Vehiculo vehiculo) {
        _logger.Information("Guardando nuevo vehiculo: {vehiculo}", vehiculo);
        ValidarVehiculo(vehiculo);
        if (repository.GetByMatricula(vehiculo.Matricula) != null) {
            throw new VehiculoException.AlreadyExists(vehiculo.Matricula);
        }
        var nuevoVehiculo = repository.Create(vehiculo);
        if (nuevoVehiculo == null) {
            throw new VehiculoException.Validation(new List<string> { 
                $"El propietario {vehiculo.DniPropietario} ya tiene 3 vehículos registrados." 
            });
        }
        return nuevoVehiculo;
    }

    public Vehiculo Update(int id, Vehiculo vehiculo) {
        _logger.Information("Actualizando nuevo vehiculo: {vehiculo}", vehiculo);
        ValidarVehiculo(vehiculo);
        var actualizado = repository.Update(id,vehiculo) ?? throw new VehiculoException.NotFound(id.ToString());
        cache.Remove(id);
        return actualizado;
    }

    public Vehiculo Delete(int id) {
        _logger.Information("Eliminando vehiculo con ID: {id}", id);
        var eliminado = repository.Delete(id) ?? throw new VehiculoException.NotFound(id.ToString());
        cache.Remove(id);
        return eliminado;
    }

    public Vehiculo HardDelete(int id) {
        _logger.Information("Eliminando vehiculo con ID: {id}", id);
        var eliminado = repository.HardDelete(id) ?? throw new VehiculoException.NotFound(id.ToString());
        cache.Remove(id);
        return eliminado;
    }

    public bool DeleteAll() {
        _logger.Information("Eliminando todos los vehiculos");
        return repository.DeleteAll();
    }

    public IEnumerable<Informe> GenerarTodosInformeVehiculo() {
        var lista = repository.GetAll().Where(v => !v.IsDeleted).ToList();
        return lista.Select(v => new Informe {
            Id = v.Id,
            Matricula = v.Matricula,
            Marca = v.Marca,
            DatosMotor = $"{v.Cilindrada}cc {v.TipoMotor}",
            PropietarioDni = v.DniPropietario
        });
    }
    
    public Informe GenerarInformeVehiculPorId(int id) {
        var v = repository.GetById(id) ?? throw new Exception();
        return new Informe {
            Id = v.Id,
            Matricula = v.Matricula,
            Marca = $"{v.Marca} {v.Marca}",
            DatosMotor = $"{v.Cilindrada}L {v.TipoMotor}",
            PropietarioDni = v.DniPropietario
        };
    }
    public int ImportarDatos() {
        _logger.Information("Importando datos desde almacenamiento externo");
        try {
            var vehiculos = storage.ReadFromFile(Configuracion.VehiculoFile); 
            repository.DeleteAll();
            var contador = 0;
            foreach (var v in vehiculos) {
                Save(v);
                contador++;
            }
            _logger.Information("Datos importados correctamente. Total de vehivulos: {count}", contador);
            return contador;
        }
        catch (Exception e) {
            _logger.Error(e, "Error al importar datos: {message}", e.Message);
            throw new VehiculoException.StorageError(e.Message);
            
        } 
    }

    public int ExportarDatos() {
        _logger.Information("Exportando datos a almacenamiento externo.");
        try {
            var vehiculo = repository.GetAll();
            var count = vehiculo.Count();
            
            _logger.Information("Exportando datos a almacenamiento externo. Total de vehículos: {count}", count);
            storage.WriteToFile(vehiculo, Configuracion.VehiculoFile);
            _logger.Information("Datos exportados correctamente a {file}.", Configuracion.DataFolder);
            return count;
        }
        catch (Exception ex) {
            _logger.Error(ex, "Error al exportar datos: {message}", ex.Message);
            throw new VehiculoException.StorageError(ex.Message);
        }
    }

    public string RealizarBackup() {
        _logger.Information("Realizando backup del sistema.");
        var vehiculos = repository.GetAll();
        return _backupService.RealizarBackup(vehiculos);    }

    public int RestaurarBackup(string archivoBackup) {
        _logger.Information("Restaurando backup desde: {archivo}", archivoBackup);
        var vehiculos = _backupService.RestaurarBackup(archivoBackup).ToList();
        repository.DeleteAll();
        var contador = 0;
        foreach (var p in vehiculos) {
            Save(p);
            contador++;
        }
        _logger.Information("Restauración completada. Total registros: {count}", contador);
        return contador;    }

    public IEnumerable<string> ListarBackups() {
        return _backupService.ListarBackups();
        
    }

    private void ValidarVehiculo(Vehiculo vehiculo) {
        var errores = validator.Validar(vehiculo);
        if (errores.Any()) {
            throw new VehiculoException.Validation(errores.ToList());
        }
    }
}