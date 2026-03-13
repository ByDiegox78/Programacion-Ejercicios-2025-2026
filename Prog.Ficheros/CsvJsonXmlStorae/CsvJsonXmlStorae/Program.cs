// See https://aka.ms/new-console-template for more information

using CsvJsonXmlStorae.Config;
using CsvJsonXmlStorae.Enums;
using CsvJsonXmlStorae.Models;
using CsvJsonXmlStorae.Storage;
using CsvJsonXmlStorae.Storage.Json;
using CsvJsonXmlStorae.Storage.Xml;

Console.WriteLine("Hello, World!");

var storage = new CiudadanoStorageCsv();
var storajeJson = new CiudadanoStorageJson();
var storageXml = new CiudadanosStorageXml();

var nuevo = new Ciudadano(
    2, "Laura", "Martínez", 28, "laura@email.com", 623456789,
    "Av. de la Paz 45", "Madrid", "España", 28010,
    "Diseñadora", "CreativeStudio", 32000, DateTime.Parse("1996-07-22"),
    Genero.Femenino, Estado.Casado, 1, DateTime.Now, true
);

//ciudadanos.Add(nuevo);
//storage.Salvar(ciudadanos, Configuracion.CiudadanoFile);

var cargados = storage.Cargar(Configuracion.CiudadanoFile).ToList();
cargados.Add(nuevo);
Console.WriteLine("--- DATOS DEL CSV ---");
foreach (var c in cargados) {
    Console.WriteLine($"{c.Id} - {c.Nombre} {c.Apellido}");
}
Console.WriteLine("--- DATOS DEL JSON ---");
var jsonCargado = storajeJson.Cargar(Configuracion.CiudadanoFileJson).ToList();
jsonCargado.Add(nuevo);
foreach (var c in jsonCargado) {
    Console.WriteLine($"{c.Id} - {c.Nombre} {c.Apellido} {c.FechaNacimiento}");
}
Console.WriteLine("--- DATOS DEL XML ---");
var xmlCargado = storageXml.Cargar(Configuracion.CiudadanoFileXml).ToList();
xmlCargado.Add(nuevo);
foreach (var c in xmlCargado) {
    Console.WriteLine($"{c.Id} - {c.Nombre} {c.Apellido} {c.FechaNacimiento}");
}