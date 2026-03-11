using System.Text.Json.Serialization;

namespace CsvJsonXmlStorae.Dto;

public record CiudadanoDto(
    [property: JsonPropertyName("id")]
    int Id,
    
    [property: JsonPropertyName("nombre")]
    string Nombre,
    
    [property: JsonPropertyName("apellido")]
    string Apellido,
    
    [property: JsonPropertyName("edad")]
    int Edad,
    
    [property: JsonPropertyName("email")]
    string Email,
    
    [property: JsonPropertyName("telefono")]
    int Telefono,
    
    [property: JsonPropertyName("direccion")]
    string Direccion,
    
    [property: JsonPropertyName("ciudad")]
    string Ciudad,
    
    [property: JsonPropertyName("pais")]
    string Pais,
    
    [property: JsonPropertyName("codigopostal")]
    int CodigoPostal,
    
    [property: JsonPropertyName("profesion")]
    string Profesion,
    
    [property: JsonPropertyName("empresa")]
    string Empresa,
    
    [property: JsonPropertyName("salario")]
    int Salario,
    
    [property: JsonPropertyName("fechanacimiento")]
    string FechaNacimiento,
    
    [property: JsonPropertyName("genero")]
    string Genero,
    
    [property: JsonPropertyName("estadocivil")]
    string EstadoCivil,
    
    [property: JsonPropertyName("numhijos")]
    int NumHijos,
    
    [property: JsonPropertyName("fecharegistro")]
    string FechaRegistro,
    
    [property: JsonPropertyName("activo")]
    bool Activo
);