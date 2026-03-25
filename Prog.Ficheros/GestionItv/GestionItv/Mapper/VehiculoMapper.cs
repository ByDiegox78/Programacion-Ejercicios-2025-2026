using System.Globalization;
using GestionItv.Dto;
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
            Enum.TryParse(dto.TipoMotor, out Motor tipo) ? tipo : Motor.Diese,
            dto.DniPropietario,
            dto.IsDelete,
            createdAt,
            updatedAt
        );
    }
}