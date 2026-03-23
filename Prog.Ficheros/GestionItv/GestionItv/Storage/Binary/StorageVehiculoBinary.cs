using System.Text;
using GestionItv.Dto;
using GestionItv.Mapper;
using GestionItv.Models;

namespace GestionItv.Storage.Binary;

public class StorageVehiculoBinary : IStorageVehiculoBin {
    public void WriteToFile(IEnumerable<Vehiculo> items, string path) {
        using var writer = new BinaryWriter(File.Create(path));
        var dtos = items.Select(p => p.ToDto()).ToList();
        foreach (var d in dtos) {
            writer.Write(d.Matricula);
            writer.Write(d.Marca);
            writer.Write(d.Cilindrada);
            writer.Write(d.TipoMotor);
            writer.Write(d.DniPropietario);
            writer.Write(d.IsDelete);
            writer.Write(d.CreatedAt);
            writer.Write(d.UpdatedAt);
        }
    }

    public IEnumerable<Vehiculo> ReadFromFile(string path) {
        if (!File.Exists(path)) {
            throw new FileNotFoundException($"El archivo '{path}' no existe.");
        }

        var list = new List<Vehiculo>();
        using var reader = new BinaryReader(File.OpenRead(path), Encoding.UTF8);
        var count = reader.ReadInt32();
        
        for (int i = 0; i < count; i++) {
            var dto = new VehiculoDto(
                reader.ReadString(),
                reader.ReadString(),
                reader.ReadInt32(),
                reader.ReadString(),
                reader.ReadString(),
                reader.ReadBoolean(),
                reader.ReadString(),
                reader.ReadString()

            );
            list.Add(dto.ToModel());
        }

        return list;

    }
}