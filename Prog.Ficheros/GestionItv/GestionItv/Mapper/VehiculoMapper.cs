using System.Globalization;
using GestionItv.Dto;
using GestionItv.Entity;
using GestionItv.Models;

namespace GestionItv.Mapper;

public static class VehiculoMapper {
    private const string IsoFormat = "s";
    private static readonly CultureInfo InvariantCulture = CultureInfo.InvariantCulture;
    
    public static VehiculoDto ToDto(this Vehiculo vehiculo) {
        return new VehiculoDto(
            vehiculo.Id,
            vehiculo.Matricula,
            vehiculo.Marca,
            vehiculo.Cilindrada,
            vehiculo.TipoMotor.ToString(),
            vehiculo.DniPropietario,
            vehiculo.IsDeleted,
            vehiculo.CreatedAt.ToString(IsoFormat, InvariantCulture),
            vehiculo.UpdatedAt.ToString(IsoFormat, InvariantCulture)
        );
    }

    public static Vehiculo ToModel(this VehiculoDto dto) {
        var createdAt = DateTime.Parse(dto.CreatedAt, InvariantCulture);
        var updatedAt = DateTime.Parse(dto.UpdatedAt, InvariantCulture);
        
        return new Vehiculo(
            dto.Id,
            dto.Matricula,
            dto.Marca,
            dto.Cilindrada,
            Enum.TryParse(dto.TipoMotor, out Motor tipo) ? tipo : Motor.Diesel,
            dto.DniPropietario,
            dto.IsDelete,
            createdAt,
            updatedAt
        );
    }

    public static Vehiculo? ToModel(this VehiculoEntity? entity) {
        if (entity == null) return null;
        return new Vehiculo(
            entity.Id,
            entity.Matricula,
            entity.Marca,
            entity.Cilindrada,
            (Motor)entity.Motor,
            entity.Dni,
            entity.IsDeleted,
            entity.CreatedAt,
            entity.UpdatedAt
        );
    }

    public static VehiculoEntity ToEntity(this Vehiculo vehiculo) {
        return new VehiculoEntity {
            Id = vehiculo.Id,
            Matricula = vehiculo.Matricula,
            Marca = vehiculo.Marca,
            Cilindrada = vehiculo.Cilindrada,
            Motor = (int)vehiculo.TipoMotor,
            Dni = vehiculo.DniPropietario,
            IsDeleted = vehiculo.IsDeleted,
            CreatedAt = vehiculo.CreatedAt,
            UpdatedAt = vehiculo.UpdatedAt
        };
    }

    public static IEnumerable<Vehiculo> ToModel(this IEnumerable<VehiculoEntity> entities) {
        return entities.Select(ToModel).OfType<Vehiculo>();
    }
}