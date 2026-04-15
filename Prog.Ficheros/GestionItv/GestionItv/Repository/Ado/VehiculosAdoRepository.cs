using System.Data;
using GestionItv.Config;
using GestionItv.Entity;
using GestionItv.Factory.Vehiculos;
using GestionItv.Mapper;
using GestionItv.Models;
using GestionItv.Repository.Common;
using Microsoft.Data.Sqlite;
using Serilog;

namespace GestionItv.Repository.Ado;

public class VehiculosAdoRepository : IVehiculosRepository {
    private static readonly Lazy<VehiculosAdoRepository> Lazy = new(() => new VehiculosAdoRepository());
    public static VehiculosAdoRepository Instance => Lazy.Value;
    private readonly ILogger _logger = Log.ForContext<VehiculosAdoRepository>();
    private readonly string _connectionString;
    
    public VehiculosAdoRepository() : this(Configuracion.DropData, Configuracion.SeedData) { }

    public VehiculosAdoRepository(bool dropData, bool seedData) {
        _logger.Debug("Iniciando Repositorio Ado");
        _connectionString = Configuracion.ConnectionString;
        EnsureDataFolder();
        EnsureTable();
        // if (dropData) {
        //     _logger.Warning("Borrando todos los datos...");
        //     DeleteAll();
        // }
        //
        // // if (dropData || seedData) {
        // //     _logger.Information("Cargando datos de semilla...");
        // //     foreach (var v in VehiculosFactory.Seed()) {
        // //         Create(v);
        // //     }
        // //     _logger.Information("Datos cargados exitosamente");
        // // }
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
        command.ExecuteNonQuery();
    }
    public IEnumerable<Vehiculo> GetAll() {
        _logger.Debug("Obteniendo todos los datos de vehiculos...");
        var entities = new List<VehiculoEntity>();
        using var connection = CreateConnection();
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Vehiculos WHERE IsDeleted = 0;";
        using var reader = command.ExecuteReader();
        while (reader.Read()) {
            entities.Add(ReadEntity(reader));
        }

        return entities.ToModel();
    }

    public Vehiculo? GetById(int id) {
        _logger.Debug("Obteniendo vehiculo con id: {Id}", id);
        using var connection = CreateConnection();
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Vehiculos WHERE Id = @Id";
        command.Parameters.Add(new SqliteParameter("Id", id));
        using var reader = command.ExecuteReader();
        return reader.Read() ? ReadEntity(reader).ToModel() : null;
    }

    public Vehiculo? Create(Vehiculo entity) {
        _logger.Debug("Creando vehiculo con id: {Id}", entity.Id);
        if (ExisteMatricula(entity.Matricula)) return null;
        if (!VerificarCochePropietario(entity.DniPropietario)) return null;
        var vehiculoEntity = entity.ToEntity();
        vehiculoEntity.Id = 0;
        vehiculoEntity.CreatedAt = DateTime.Now;
        vehiculoEntity.UpdatedAt = DateTime.Now;
        vehiculoEntity.IsDeleted = false;
        
        using var connection = CreateConnection();
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = @"INSERT INTO Vehiculos (
            Matricula, Marca, Cilindrada, Motor, Dni, IsDeleted, CreatedAt, UpdatedAt) 
            VALUES (
            @Matricula, @Marca, @Cilindrada, @Motor, @Dni, @IsDeleted, @CreatedAt, @UpdatedAt);
            SELECT last_insert_rowid();";
        AddParameters(command, vehiculoEntity);
        vehiculoEntity.Id = Convert.ToInt32(command.ExecuteScalar());
        return GetById(vehiculoEntity.Id);
    }

    public Vehiculo? Update(int id, Vehiculo entity) {
        var exists = GetById(id);
        if (exists == null) return null;
        if (entity.Matricula != exists.Matricula) {
            var other = GetByMatricula(entity.Matricula);
            if (other != null && other.Id != id) {
                _logger.Warning("No se puede actualizar persona con id {Id} porque el DNI {Dni} ya está en uso por otra persona", id, entity.DniPropietario);
                return null; 
            }
        }
        if (entity.DniPropietario != exists.DniPropietario) {
            if (!VerificarCochePropietario(entity.DniPropietario)) {
                _logger.Warning("El propietario con DNI {Dni} no es válido o ya tiene 3 vehículos", entity.DniPropietario);
                return null;
            }
        }
        var vEntity = entity.ToEntity();
        vEntity.Id = id;
        vEntity.CreatedAt = exists.CreatedAt;
        vEntity.UpdatedAt = DateTime.Now;
        vEntity.IsDeleted = false;
        
        using var connection = CreateConnection();
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = @"
            UPDATE Vehiculos 
            SET Matricula = @Matricula, Marca = @Marca, Cilindrada = @Cilindrada, Motor = @Motor, Dni = @Dni, IsDeleted = @IsDeleted, CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt
            WHERE Id = @Id;";
        AddParameters(command, vEntity);
        command.ExecuteNonQuery();
        return GetById(id);
    }

    public Vehiculo? Delete(int id) {
        var exists = GetById(id);
        if (exists == null) return null;
        using var connection = CreateConnection();
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "UPDATE Vehiculos SET IsDeleted = 1, UpdatedAt = @UpdatedAt WHERE Id = @Id";
        command.Parameters.Add(new SqliteParameter("@Id", id));
        command.Parameters.Add(new SqliteParameter("@UpdatedAt", DateTime.UtcNow.ToString("o")));
        command.ExecuteNonQuery();

        return GetById(id);
    }

    public bool DeleteAll() {
        using var connection = CreateConnection();
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Vehiculos;";
        return command.ExecuteNonQuery() >= 0;
    }

    public Vehiculo? HardDelete(int id) {
        var exists = GetById(id);
        if (exists == null) return null;
        using var connection = CreateConnection();
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Vehiculos WHERE Id = @Id";
        command.Parameters.Add(new SqliteParameter("@Id", id));
        command.ExecuteNonQuery();
        return exists;
    }

    public Vehiculo? GetByMatricula(string matricula) {
        using var connection = CreateConnection();
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Vehiculos WHERE Matricula = @Matricula";
        command.Parameters.Add(new SqliteParameter("@Matricula", matricula));
        
        using var reader = command.ExecuteReader();
        return reader.Read() ? ReadEntity(reader).ToModel() : null;
    }

    private bool ExisteMatricula(string matricula) {
        using var connection = CreateConnection();
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = @"SELECT COUNT(1) FROM Vehiculos WHERE Matricula = @Matricula";
        command.Parameters.Add(new SqliteParameter("Matricula", matricula));
        return Convert.ToInt32(command.ExecuteScalar()) > 0;
    }
    
    private VehiculoEntity ReadEntity(SqliteDataReader reader) {
        return new VehiculoEntity {
            Id = reader.GetInt32(reader.GetOrdinal("id")),
            Matricula = reader.GetString(reader.GetOrdinal("matricula")),
            Marca = reader.GetString(reader.GetOrdinal("marca")),
            Cilindrada = reader.GetInt32(reader.GetOrdinal("cilindrada")),
            Motor = reader.GetInt32(reader.GetOrdinal("motor")),
            Dni = reader.GetString(reader.GetOrdinal("dni")),
            IsDeleted = reader.GetInt32(reader.GetOrdinal("isdeleted")) == 1,
            CreatedAt = DateTime.Parse(reader.GetString(reader.GetOrdinal("createdat"))),
            UpdatedAt = DateTime.Parse(reader.GetString(reader.GetOrdinal("updatedat")))
        };
        
    }
    private void AddParameters(IDbCommand command, VehiculoEntity entity, int? id = null) { 
        if (id.HasValue) {
            command.Parameters.Add(new SqliteParameter("@Id", id.Value));
        }
        command.Parameters.Add(new SqliteParameter("@Matricula", entity.Matricula));
        command.Parameters.Add(new SqliteParameter("@Marca", entity.Marca));
        command.Parameters.Add(new SqliteParameter("@Cilindrada", entity.Cilindrada));
        command.Parameters.Add(new SqliteParameter("@Motor", entity.Motor));
        command.Parameters.Add(new SqliteParameter("@Dni", entity.Dni));
        command.Parameters.Add(new SqliteParameter("@IsDeleted", entity.IsDeleted ? 1 : 0));
        command.Parameters.Add(new SqliteParameter("@CreatedAt", entity.CreatedAt.ToString("s")));
        command.Parameters.Add(new SqliteParameter("@UpdatedAt", entity.UpdatedAt.ToString("s")));
    }
    private bool VerificarCochePropietario(string dni) {
        using var connection = CreateConnection();
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT COUNT(*) FROM Vehiculos WHERE Dni = @dni AND IsDeleted = 0";
        command.Parameters.AddWithValue("@dni", dni);
        int cantidadCoches = Convert.ToInt32(command.ExecuteScalar());
        return cantidadCoches < 3;
    }
    
}