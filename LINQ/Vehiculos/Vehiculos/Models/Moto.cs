namespace Vehiculos.Models;

public record Moto(string Matricula, string Marca, string Modelo, int AñoMatriculacion, int Cilindrada, DateTime CreatedAt, DateTime UpdatedAt) : Vehiculo(Matricula, Marca, Modelo, AñoMatriculacion, CreatedAt,UpdatedAt);