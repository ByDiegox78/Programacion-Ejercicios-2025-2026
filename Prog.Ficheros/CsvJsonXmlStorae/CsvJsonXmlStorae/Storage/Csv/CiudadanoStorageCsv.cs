using System.Text;
using CsvJsonXmlStorae.Config;
using CsvJsonXmlStorae.Dto;
using CsvJsonXmlStorae.Mapper;
using CsvJsonXmlStorae.Models;

namespace CsvJsonXmlStorae.Storage;

public class CiudadanoStorageCsv : ICiudadanoStorageCsv {

    public CiudadanoStorageCsv() {
        InitStorage(); 
    }
    
    public void Salvar(IEnumerable<Ciudadano> items, string path) {
        try {
            using var writer = new StreamWriter(path, false, Encoding.UTF8);
            writer.WriteLine("Id;Nombre;Apellido;Edad;Email;Telefono;Direccion;Ciudad;Pais;CodigoPostal;Profesion;Empresa;Salario;FechaNacimiento;Genero;EstadoCivil;NumHijos;FechaRegistro;Activo");
            items.Select(p => p.ToDto())
                .ToList()
                .ForEach(dto => {
                    writer.WriteLine($"{dto.Id};{dto.Nombre};{dto.Apellido};{dto.Edad};{dto.Email};{dto.Telefono};{dto.Direccion};{dto.Ciudad};{dto.Pais};{dto.CodigoPostal};{dto.Profesion};{dto.Empresa};{dto.Salario};{dto.FechaNacimiento};{dto.Genero};{dto.EstadoCivil};{dto.NumHijos};{dto.FechaRegistro};{dto.Activo}");
                });
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
    /// <inheritdoc cref="IStorage{T}.Cargar"/>
    public IEnumerable<Ciudadano> Cargar(string path) {
        if (!Path.Exists(path)) {
            return [];
        }

        try {
            return File.ReadLines(path, Encoding.UTF8)
                .Skip(1)
                .Select(linea => linea.Split(';'))
                .Select(campos => new CiudadanoDto(
                    int.Parse(campos[0]),
                    campos[1],
                    campos[2],
                    int.Parse(campos[3]),
                    campos[4],
                    int.Parse(campos[5]),
                    campos[6],
                    campos[7],
                    campos[8],
                    int.Parse(campos[9]),
                    campos[10],
                    campos[11],
                    int.Parse(campos[12]),
                    campos[13],
                    campos[14],
                    campos[15],
                    int.Parse(campos[16]),
                    campos[17],
                    bool.Parse(campos[18])
                ).ToModel()).ToList();
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
    
    private void InitStorage() {
        if (Directory.Exists(Configuracion.DataFolder))
            return;
        Directory.CreateDirectory("data");
    }
}