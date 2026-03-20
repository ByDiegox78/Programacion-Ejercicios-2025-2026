namespace GestionItv.Models;

public record Vehiculo(
    string Matricula,
    string Marca,
    int cilindrada,
    Motor TipoMotor,
    string DniPropietario,
    bool IsDeleted,
    DateTime CreatedAt,
    DateTime UpdatedAt
    );