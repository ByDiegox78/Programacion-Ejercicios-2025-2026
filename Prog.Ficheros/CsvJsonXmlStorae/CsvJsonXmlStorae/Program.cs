// See https://aka.ms/new-console-template for more information

using CsvJsonXmlStorae.Config;
using CsvJsonXmlStorae.Enums;
using CsvJsonXmlStorae.Models;
using CsvJsonXmlStorae.Storage;
using CsvJsonXmlStorae.Storage.Json;

Console.WriteLine("Hello, World!");

var storage = new CiudadanoStorageCsv();
var storajeJson = new CiudadanoStorageJson();

var ciudadanos = storage.Cargar(Configuracion.CiudadanoFile).ToList();
var ciudadanosJson = storage.Cargar(Configuracion.CiudadanoFileJson).ToList();

// Crear algunos ciudadanos
var nuevo = new Ciudadano(
    21,
    "Diego",
    "Manzanero",
    21,
    "diego@email.com",
    612345678,
    "Calle Ejemplo 5",
    "Madrid",
    "España",
    28020,
    "Estudiante",
    "Universidad",
    0,
    new DateTime(2004, 1, 10),
    Genero.Masculino,
    Estado.Soltero,
    0,
    DateTime.Now,
    true
);

// Guardar usando la ruta de Configuracion
ciudadanos.Add(nuevo);
storage.Salvar(ciudadanos, Configuracion.CiudadanoFile);

ciudadanosJson.Add(nuevo);
storajeJson.Salvar(ciudadanosJson, Configuracion.CiudadanoFileJson);

Console.WriteLine("CSV guardado");

// Leer el CSV
var cargados = storage.Cargar(Configuracion.CiudadanoFile);

foreach (var c in cargados) {
    Console.WriteLine($"{c.Id} - {c.Nombre} {c.Apellido}");
}