using System.Globalization;
using RepositorioAleatorio.Dto;
using RepositorioAleatorio.Models;

namespace RepositorioAleatorio.Mapper;

public static class PersonaMapper {
    private const string IsoFormat = "s";
    private static readonly CultureInfo InvariantCulture = CultureInfo.InvariantCulture;

    public static Persona ToModel(this PersonaDto dto) {
        var createdAt = DateTime.Parse(dto.CreatedAt, InvariantCulture);
        var updatedAt = DateTime.Parse(dto.UpdatedAt, InvariantCulture);

        return new Persona(
            Id: dto.Id,
            Nombre: dto.Nombre,
            Edad: dto.Edad,
            IsDeleted: dto.IsDeleted,
            CreatedAt: createdAt,
            UpdatedAt: updatedAt
        );
    }

    public static PersonaDto ToDto(this Persona Persona) {
        return new PersonaDto(
            Persona.Id,
            Persona.Nombre,
            Persona.Edad,
            Persona.IsDeleted,
            Persona.CreatedAt.ToString(IsoFormat, InvariantCulture),
            Persona.UpdatedAt.ToString(IsoFormat, InvariantCulture)
        );
    }
}