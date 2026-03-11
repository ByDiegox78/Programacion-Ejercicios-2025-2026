using System.Globalization;
using CsvJsonXmlStorae.Dto;
using CsvJsonXmlStorae.Enums;
using CsvJsonXmlStorae.Models;

namespace CsvJsonXmlStorae.Mapper;

public static class CiudadanoMapper {
    private const string IsoFormat = "s";
    private static readonly CultureInfo InvariantCulture = CultureInfo.InvariantCulture;

    public static Ciudadano ToModel(this CiudadanoDto dto) {
        var fechaNac = DateTime.Parse(dto.FechaNacimiento, InvariantCulture);
        var fechRegistro= DateTime.Parse(dto.FechaNacimiento, InvariantCulture);
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
            Genero: Enum.Parse<Genero>(dto.Genero),
            EstadoCivil: Enum.Parse<Estado>(dto.EstadoCivil),
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