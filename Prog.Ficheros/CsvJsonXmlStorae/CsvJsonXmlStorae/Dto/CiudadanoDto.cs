using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace CsvJsonXmlStorae.Dto;
[XmlRoot("Ciudadanos")]
[XmlType("Ciudadano")]
public record CiudadanoDto(
    [property: XmlElement("id")] int Id,

    [property: XmlElement("nombre")] string Nombre,

    [property: XmlElement("apellido")]
    string Apellido,

    [property: XmlElement("edad")] int Edad,

    [property: XmlElement("email")] string Email,

    [property: XmlElement("telefono")]
    int Telefono,

    [property: XmlElement("direccion")]
    string Direccion,

    [property: XmlElement("ciudad")] string Ciudad,

    [property: XmlElement("pais")] string Pais,

    [property: XmlElement("codigopostal")]
    int CodigoPostal,

    [property: XmlElement("profesion")]
    string Profesion,

    [property: XmlElement("empresa")]
    string Empresa,

    [property: XmlElement("salario")]
    int Salario,

    [property: XmlElement("fechanacimiento")]
    string FechaNacimiento,

    [property: XmlElement("genero")] string Genero,

    [property: XmlElement("estadocivil")]
    string EstadoCivil,

    [property: XmlElement("numhijos")]
    int NumHijos,

    [property: XmlElement("fecharegistro")]
    string FechaRegistro,

    [property: XmlElement("activo")] bool Activo
) {
    public CiudadanoDto() 
        : this(
            0,          // Id
            "",         // Nombre
            "",         // Apellido
            0,          // Edad
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
