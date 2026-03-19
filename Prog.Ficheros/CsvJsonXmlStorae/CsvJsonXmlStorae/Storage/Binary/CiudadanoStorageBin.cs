using CsvJsonXmlStorae.Dto;
using CsvJsonXmlStorae.Mapper;
using CsvJsonXmlStorae.Models;

namespace CsvJsonXmlStorae.Storage.bin;

public class CiudadanoStorageBin : ICiudadanoBinStorage{
    public void WriteToFile(IEnumerable<Ciudadano> items, string path) {
        //Escribe
        using var writer = new BinaryWriter(File.Create(path));
        var dtos = items.Select(p => p.ToDto()).ToList();
        foreach (var dto in dtos) {
            {
                writer.Write(dto.Id);
                writer.Write(dto.Nombre);
                writer.Write(dto.Apellido);
                writer.Write(dto.Edad);
                writer.Write(dto.Email);
                writer.Write(dto.Telefono);
                writer.Write(dto.Direccion);
                writer.Write(dto.Ciudad);
                writer.Write(dto.Pais);
                writer.Write(dto.CodigoPostal);
                writer.Write(dto.Profesion);
                writer.Write(dto.Empresa);
                writer.Write(dto.Salario);
                writer.Write(dto.FechaNacimiento);
                writer.Write(dto.Genero);
                writer.Write(dto.EstadoCivil);
                writer.Write(dto.NumHijos);
                writer.Write(dto.FechaRegistro);
                writer.Write(dto.Activo);
            }
        }
    }

    public IEnumerable<Ciudadano> ReadFromFile(string path) {
        if (!File.Exists(path)) {
            throw new FileNotFoundException($"El archivo '{path}' no existe.");
        }
        
        using var reader = new BinaryReader(File.OpenRead(path));
        var ciudadanos = new List<Ciudadano>();

        while (reader.BaseStream.Position < reader.BaseStream.Length) {
            var id = reader.ReadInt32();
            var nombre = reader.ReadString();
            var apellido = reader.ReadString();
            var edad = reader.ReadInt32();
            var email = reader.ReadString();
            var telefono = reader.ReadInt32();
            var direccion = reader.ReadString();
            var ciudad = reader.ReadString();
            var pais = reader.ReadString();
            var codigoPostal = reader.ReadInt32();
            var profesion = reader.ReadString();
            var empresa = reader.ReadString();
            var salario = reader.ReadInt32();
            var fechaNacimiento = reader.ReadString();
            var genero = reader.ReadString();
            var estadoCivil = reader.ReadString();
            var numHijos = reader.ReadInt32();
            var fechaRegistro = reader.ReadString();
            var activo = reader.ReadBoolean();
            var ciudadano = new CiudadanoDto(
                id,
                nombre,
                apellido,
                edad,
                email,
                telefono,
                direccion,
                ciudad,
                pais,
                codigoPostal,
                profesion,
                empresa,
                salario,
                fechaNacimiento,
                genero,
                estadoCivil,
                numHijos,
                fechaRegistro,
                activo
            );
            ciudadanos.Add(ciudadano.ToModel());
        }
        return ciudadanos;
    }
}