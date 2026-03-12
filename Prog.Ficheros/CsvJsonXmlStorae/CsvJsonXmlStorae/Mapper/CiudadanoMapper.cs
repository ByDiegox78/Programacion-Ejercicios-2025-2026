using System.Globalization;
using CsvJsonXmlStorae.Dto;
using CsvJsonXmlStorae.Enums;
using CsvJsonXmlStorae.Models;

namespace CsvJsonXmlStorae.Mapper;

public static class CiudadanoMapper {
    private const string IsoFormat = "t";
    private static readonly CultureInfo InvariantCulture = CultureInfo.InvariantCulture;

    public static Ciudadano ToModel(this CiudadanoDto dto) {
        var fechaNac = DateTime.TryParse(dto.FechaNacimiento, out var f) ? f : DateTime.UtcNow;
        var fechRegistro= DateTime.TryParse(dto.FechaNacimiento, out var fc) ? fc: DateTime.UtcNow;
        return new Ciudadano(
            Id: dto.Id,
            Nombre: dto.Nombre,
            Apellido: dto.Apellido,
            Edad: dto.Edad,
            Email: dto.Email,
            Telefono: dto.Telefono,
            Direccion: dto.Direccion,
            Ciudad: dto.Ciudad,
            Pais: dto.Pais,
            CodigoPostal: dto.CodigoPostal,
            Profesion: dto.Profesion,
            Empresa: dto.Empresa,
            Salario: dto.Salario,
            FechaNacimiento: fechaNac,
            Genero: Enum.TryParse(dto.Genero, out Genero gen) ? gen : Genero.Femenino,
            EstadoCivil: Enum.TryParse(dto.Genero, out Estado es) ? es : Estado.Casada,
            NumHijos: dto.NumHijos,
            FechaRegistro: fechRegistro,
            Activo: dto.Activo
        );
    }

    public static CiudadanoDto ToDto(this Ciudadano ciudadano) {
        return new CiudadanoDto(
            ciudadano.Id,
            ciudadano.Nombre,
            ciudadano.Apellido,
            ciudadano.Edad,
            ciudadano.Email,
            ciudadano.Telefono,
            ciudadano.Direccion,
            ciudadano.Ciudad,
            ciudadano.Pais,
            ciudadano.CodigoPostal,
            ciudadano.Profesion,
            ciudadano.Empresa,
            ciudadano.Salario,
            ciudadano.FechaNacimiento.ToString(IsoFormat, InvariantCulture),
            ciudadano.Genero.ToString(),
            ciudadano.EstadoCivil.ToString(),
            ciudadano.NumHijos,
            ciudadano.FechaRegistro.ToString(IsoFormat, InvariantCulture),
            ciudadano.Activo
        );
    }
}