namespace Vehiculos.Models;

public record Coche(string Matricula, string Marca, string Modelo, int AñoMatriculacion, int NumPuertas, DateTime CreatedAt, DateTime UpdatedAt) : Vehiculo(Matricula, Marca, Modelo, AñoMatriculacion, CreatedAt,UpdatedAt);