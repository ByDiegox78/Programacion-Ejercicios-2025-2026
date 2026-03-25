using System.Text;
using GestionItv.Config;
using GestionItv.Dto;
using GestionItv.Mapper;
using GestionItv.Models;

namespace GestionItv.Storage.Common.Csv;

public class StorageCsvVehiculo : IStorageCsvVehiculo {

    public StorageCsvVehiculo() {
        InitStorage();
    }
    
    public void WriteToFile(IEnumerable<Vehiculo> items, string path) {
        try {
            using var write = new StreamWriter(path, false, Encoding.UTF8);
            write.WriteLine("Matricula;Marca;Cilindrada;TipoMotor;DniPropietario;IsDelete;CreatedAt;UpdatedAt;");
            items.Select(p => p.ToDto())
                .ToList()
                .ForEach(p => {
                    write.WriteLine(
                        $"{p.Matricula};{p.Marca};{p.Cilindrada};{p.TipoMotor};{p.DniPropietario};{p.IsDelete};{p.CreatedAt};{p.UpdatedAt};");
                });
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }

    public IEnumerable<Vehiculo> ReadFromFile(string path) {
        if (!Path.Exists(path)) {
            return [];
        }
        try {
            return File.ReadLines(path, Encoding.UTF8)
                .Skip(1)
                .Select(linea => linea.Split(';'))
                .Select(campos => new VehiculoDto(
                    int.Parse(campos[0]),
                    campos[1],
                    campos[2],
                    int.Parse(campos[3]),
                    campos[4],
                    campos[5],
                    bool.Parse(campos[6]),
                    campos[7],
                    campos[7]

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