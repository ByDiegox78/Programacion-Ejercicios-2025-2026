using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace CsvJsonXmlStorae.Dto;
[XmlRoot("Ciudadanos")]
[XmlType("CiudadanoDto")]
public record CiudadanoDto(
    [property: XmlElement("Id")] int Id,

    [property: XmlElement("Nombre")] string Nombre,

    [property: XmlElement("Apellido")] string Apellido,

    [property: XmlElement("Edad")] int Edad,    

    [property: XmlElement("Email")] string Email,

    [property: XmlElement("Telefono")]
    int Telefono,

    [property: XmlElement("Direccion")]
    string Direccion,

    [property: XmlElement("Ciudad")] string Ciudad,

    [property: XmlElement("Pais")] string Pais,

    [property: XmlElement("CodigoPostal")]
    int CodigoPostal,

    [property: XmlElement("Profesion")]
    string Profesion,

    [property: XmlElement("Empresa")]
    string Empresa,

    [property: XmlElement("Salario")]
    int Salario,

    [property: XmlElement("FechaNacimiento")]
    string FechaNacimiento,

    [property: XmlElement("Genero")] string Genero,

    [property: XmlElement("EstadoCivil")]
    string EstadoCivil,

    [property: XmlElement("NumHijos")]
    int NumHijos,

    [property: XmlElement("FechaRegistro")]
    string FechaRegistro,

    [property: XmlElement("Activo")] bool Activo
) {
    public CiudadanoDto() 
        : this(
            0,         
            "",         
            "",         // Apellido
            0,          
            "",         // Email
            0,          // Telefono
            "",         // Direccion
            "",         // Ciudad
            "",         // Pais
            0,          // CodigoPostal
            "",         // Profesion
            "",         // Empresa
            0,          // Salario
            "",    // FechaNacimiento
            "",         // Genero
            "",         // EstadoCivil
            0,          // NumHijos
            "",    // FechaRegistro
            false       // Activo
        )
    { }
}
