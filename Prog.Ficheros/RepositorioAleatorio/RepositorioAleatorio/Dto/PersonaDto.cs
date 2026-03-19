namespace RepositorioAleatorio.Dto;

public record PersonaDto (
    int Id,
    string Nombre,
    int Edad,
    bool IsDeleted,
    string CreatedAt,
    string UpdatedAt
    );