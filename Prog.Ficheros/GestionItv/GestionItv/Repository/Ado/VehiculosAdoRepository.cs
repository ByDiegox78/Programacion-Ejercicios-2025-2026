using GestionItv.Config;
using GestionItv.Factory.Vehiculos;
using GestionItv.Models;
using GestionItv.Repository.Common;
using Microsoft.Data.Sqlite;
using Serilog;

namespace GestionItv.Repository.Ado;

public class VehiculosAdoRepository : IVehiculosRepository {

    private readonly ILogger _logger = Log.ForContext<VehiculosAdoRepository>();
    private readonly string _connectionString;
    
    public VehiculosAdoRepository() : this(Configuracion.DropData, Configuracion.SeedData) { }

    public VehiculosAdoRepository(bool dropData, bool seedData) {
        _logger.Debug("Iniciando Repositorio Ado");
        _connectionString = Configuracion.ConnectionString;
        EnsureDataFolder();
        EnsureTable();
        if (dropData) {
            _logger.Warning("Borrando todos los datos...");
            DeleteAll();
        }

        if (dropData || seedData) {
            _logger.Information("Cargando datos de semilla...");
            foreach (var v in VehiculosFactory.Seed()) {
                Create(v);
            }
            _logger.Information("Datos cargados exitosamente");
        }
    }

    private SqliteConnection CreateConnection() => new(_connectionString);

    private void EnsureDataFolder() {
        if (!Directory.Exists(Configuracion.DataFolder)) {
            Directory.CreateDirectory(Configuracion.DataFolder);
        }
    }

    private void EnsureTable() {
        using var connection = CreateConnection();
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = @"
        DROP TABLE IF EXISTS Vehiculos;
        CREATE TABLE Vehiculos(
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Matricula TEXT NOT NULL,
            Marca TEXT NOT NULL,
            Cilindrada INTEGER NOT NULL,
            Motor INTEGER NOT NULL,
            Dni TEXT NOT NULL,
            CreatedAt TEXT NOT NULL,
            UpdatedAt TEXT NOT NULL,
            IsDeleted INTEGER NOT NULL
            )";
    }
    public IEnumerable<Vehiculo> GetAll() {
        throw new NotImplementedException();
    }

    public Vehiculo? GetById(int id) {
        throw new NotImplementedException();
    }

    public Vehiculo? Create(Vehiculo entity) {
        throw new NotImplementedException();
    }

    public Vehiculo? Update(int id, Vehiculo entity) {
        throw new NotImplementedException();
    }

    public Vehiculo? Delete(int id) {
        throw new NotImplementedException();
    }

    public bool DeleteAll() {
        throw new NotImplementedException();
    }

    public Vehiculo? HardDelete(int id) {
        throw new NotImplementedException();
    }

    public Vehiculo? GetByMatricula(string matricula) {
        throw new NotImplementedException();
    }
}