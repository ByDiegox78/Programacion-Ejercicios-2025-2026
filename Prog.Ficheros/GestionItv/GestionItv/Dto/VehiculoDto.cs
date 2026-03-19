namespace GestionItv.Dto;

public record VehiculoDto(
    string Matricula,
    string Marca,
    int Cilindrada,
    string TipoMotor,
    IEnumerable<string> DniPropietario,
    bool IsDelete,
    string CreatedAt,
    string UpdatedAt
    );