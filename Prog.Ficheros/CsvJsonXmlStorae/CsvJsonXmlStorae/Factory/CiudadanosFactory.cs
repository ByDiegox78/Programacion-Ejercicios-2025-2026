using CsvJsonXmlStorae.Enums;
using CsvJsonXmlStorae.Models;

namespace CsvJsonXmlStorae.Factories;

/// <summary>
/// Factoría con datos semilla de ciudadanos.
/// </summary>
public static class CiudadanosFactory {
    public static IEnumerable<Ciudadano> Seed() {
        var lista = new List<Ciudadano>();
        lista.Add(new Ciudadano(
            1,
            "Diego",
            "Gonzalez",
            21,
            "diego@email.com",
            612345678,
            "Calle Mayor 15",
            "Madrid",
            "España",
            28001,
            "Programador",
            "Tech Solutions",
            25000,
            new DateTime(2004, 3, 12),
            Genero.Masculino,
            Estado.Soltero,
            0,
            DateTime.Now,
            true
        ));
        lista.Add(new Ciudadano(
            2,
            "Laura",
            "Martinez",
            27,
            "laura@email.com",
            645987321,
            "Calle Luna 8",
            "Barcelona",
            "España",
            08001,
            "Diseñadora",
            "Creative Studio",
            30000,
            new DateTime(1998, 6, 15),
            Genero.Femenino,
            Estado.Casado,
            1,
            DateTime.Now,
            true
        ));
        lista.Add(new Ciudadano(
            3,
            "Carlos",
            "Lopez",
            35,
            "carlos@email.com",
            612459876,
            "Avenida Sol 22",
            "Valencia",
            "España",
            46001,
            "Ingeniero",
            "InfraTech",
            40000,
            new DateTime(1989, 11, 20),
            Genero.Masculino,
            Estado.Casado,
            2,
            DateTime.Now,
            true
        ));
        lista.Add(new Ciudadano(
            4,
            "Ana",
            "Fernandez",
            29,
            "ana@email.com",
            678123456,
            "Avenida Central 10",
            "Sevilla",
            "España",
            41001,
            "Abogada",
            "LegalCorp",
            35000,
            new DateTime(1996, 2, 28),
            Genero.Femenino,
            Estado.Soltero,
            0,
            DateTime.Now,
            true
        ));
        lista.Add(new Ciudadano(
            5,
            "Javier",
            "Ruiz",
            42,
            "javier@email.com",
            699876543,
            "Calle Verde 5",
            "Bilbao",
            "España",
            48001,
            "Doctor",
            "Hospital Central",
            50000,
            new DateTime(1983, 9, 10),
            Genero.Masculino,
            Estado.Casado,
            3,
            DateTime.Now,
            true
        ));
        return lista;
    }
}