using CsvJsonXmlStorae.Enums;
namespace CsvJsonXmlStorae.Models;
public record Ciudadano(
    int Id,
    string Nombre,
    string Apellido,
    int Edad,
    string Email,
    int Telefono,
    string Direccion,
    string Ciudad,
    string Pais,
    int CodigoPostal,
    string Profesion, 
    string Empresa,
    int Salario,
    DateTime FechaNacimiento,
    Genero Genero,
    Estado EstadoCivil,
    int NumHijos,
    DateTime FechaRegistro,
    bool Activo
    );