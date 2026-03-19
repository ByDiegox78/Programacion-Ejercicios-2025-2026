using System.Globalization;
using GestionItv.Dto;
using GestionItv.Models;

namespace GestionItv.Mapper;

public static class VehiculoMapper {
    private const string IsoFormat = "s";
    private static readonly CultureInfo InvariantCulture = CultureInfo.InvariantCulture;
    
    public static VehiculoDto ToDto(this Vehiculo vehiculo) {
        return new VehiculoDto(
            vehiculo.Matricula,
            vehiculo.Marca,
            vehiculo.cilindrada,
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
            dto.Matricula,
            dto.Marca,
            dto.Cilindrada,
            int.TryParse(dto.TipoMotor, out var tipo) ? tipo : Motor.Diese,
            
        );
    }
}