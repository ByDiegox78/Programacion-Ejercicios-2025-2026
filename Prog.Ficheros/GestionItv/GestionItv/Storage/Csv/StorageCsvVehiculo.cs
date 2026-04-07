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
            // Escribe en el archivo sobrescribiendo su contenido (UTF-8) y se cierra automáticamente
            using var write = new StreamWriter(path, false, Encoding.UTF8);
            //Escribimos el encabezado del csv
            write.WriteLine("Matricula;Marca;Cilindrada;TipoMotor;DniPropietario;IsDelete;CreatedAt;UpdatedAt;");
            //Cada vehiculo lo convierte a dto y los escribe en el fichero
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
            //Leemos el archvo linea a linea
            return File.ReadLines(path, Encoding.UTF8)
                //Ignoramos la 1 linea
                .Skip(1)
                //Convierte cada linea en un array cuando encuentre: ";"
                .Select(linea => linea.Split(';'))
                //Construye el objeto Dto con los campos
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
        //Comprueba si esiste y si no lo crea
        if (Directory.Exists(Configuracion.DataFolder))
            return;
        Directory.CreateDirectory("data");
    }
}