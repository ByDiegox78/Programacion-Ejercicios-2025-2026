// See https://aka.ms/new-console-template for more information
using static System.Console;
using CsvJsonXmlStorae.Config;
using CsvJsonXmlStorae.Enums;
using CsvJsonXmlStorae.Factories;
using CsvJsonXmlStorae.Models;
using CsvJsonXmlStorae.Repository;
using CsvJsonXmlStorae.Service;
using CsvJsonXmlStorae.Storage;
using CsvJsonXmlStorae.Storage.Json;
using CsvJsonXmlStorae.Storage.Xml;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

Main();

void Main() {
    ICiudadanosService service = new CiudadanosSerivice(CiudadanosRepository.Instance, new CiudadanosStorageXml());
    CiudadanosFactory.Seed().ToList().ForEach(p => service.Save(p));

    ExportarDatos(service);
    ImportarDatos(service);

    
}

void ExportarDatos(ICiudadanosService service) {
    WriteLine("\n📤 --- EXPORTAR DATOS A FICHERO ---");
    try {
        var exportados = service.ExportarDatos();
        WriteLine($"✅ Exportados {exportados} registros.");
    }
    catch (Exception ex) {
        WriteLine($"☠️ ERROR AL EXPORTAR: {ex.Message}");
    }
}

void ImportarDatos(ICiudadanosService service) {
    WriteLine("\n📥 --- IMPORTAR DATOS DESDE FICHERO ---");
    if (PedirConfirmacion(
            $"Desea importar los datos desde el fichero: {Configuracion.CiudadanosFile}\nEsta acción puede sobrescribir datos existentes. ¿Desea continuar?"))
        try {
            var importados = service.ImportarDatos();
            WriteLine($"✅ Importados {importados} registros.");
        }
        catch (Exception ex) {
            WriteLine($"☠️ ERROR AL IMPORTAR: {ex.Message}");
        }
}

bool PedirConfirmacion(string mensaje) {
        Write($"\n⚠️  {mensaje} (S para confirmar): ");
        var res = char.ToUpper(ReadKey(false).KeyChar) == 'S';
        WriteLine();
        return res;
    }
