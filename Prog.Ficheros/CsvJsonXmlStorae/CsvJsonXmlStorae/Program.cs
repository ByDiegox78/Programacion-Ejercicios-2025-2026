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

var ciudadanos = storage.Cargar(Configuracion.CiudadanoFile).ToList();
var ciudadanosJson = storajeJson.Cargar(Configuracion.CiudadanoFileJson).ToList();
var ciudadanosXml = storageXml.Cargar(Configuracion.CiudadanoFileXml).ToList();


//ciudadanos.Add(nuevo);
storage.Salvar(ciudadanos, Configuracion.CiudadanoFile);
//ciudadanosJson.Add(nuevo);
storajeJson.Salvar(ciudadanosJson, Configuracion.CiudadanoFileJson);
//ciudadanosXml.Add(nuevo);
storageXml.Salvar(ciudadanosXml, Configuracion.CiudadanoFileXml);

Console.WriteLine("CSV guardado");

// Leer el CSV
var cargados = storage.Cargar(Configuracion.CiudadanoFile);

foreach (var c in cargados) {
    Console.WriteLine($"{c.Id} - {c.Nombre} {c.Apellido}");
}

var jsonCargado = storajeJson.Cargar(Configuracion.CiudadanoFileJson);
foreach (var c in jsonCargado) {
    Console.WriteLine($"{c.Id} - {c.Nombre} {c.Apellido} {c.FechaNacimiento}");
}

var xmlCargado = storageXml.Cargar(Configuracion.CiudadanoFileXml);
foreach (var c in xmlCargado) {
    Console.WriteLine($"{c.Id} - {c.Nombre} {c.Apellido} {c.FechaNacimiento}");
}