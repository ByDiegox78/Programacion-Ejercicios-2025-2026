namespace Vehiculos.Models;

public abstract record Vehiculo(string Matricula, string Marca, string Modelo, int AñoMatriculacion, DateTime CreatedAt, DateTime UpdatedAt);