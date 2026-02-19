namespace Vehiculos.Models;

public record Camion(string Matricula, string Marca, string Modelo, int AñoMatriculacion, double PesoMaximo, DateTime CreatedAt, DateTime UpdatedAt) : Vehiculo(Matricula, Marca, Modelo, AñoMatriculacion, CreatedAt,UpdatedAt);