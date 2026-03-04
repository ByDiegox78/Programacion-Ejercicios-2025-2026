using Productos.Models;
using Productos.Storage.Csv;

var storage = new ProductosCsStorage();
var path = Path.Combine("data", "products.csv");

// Prueba esto para depurar
Console.WriteLine($"Ruta absoluta: {Path.GetFullPath(path)}");

var productos = storage.Cargar(path).ToList();
foreach (var prod in productos) {
    Console.WriteLine(prod);
}

var pocosProductos = productos.Where(p => p.UnitInStock < 5).ToList();

string outputPath = Path.Combine("data", "productos_criticos.csv");
storage.Salvar(pocosProductos, outputPath);

Console.WriteLine($"Se han guardado {pocosProductos.Count} productos en {outputPath}");


var todos = productos.ToList();
string outputPath1 = Path.Combine("data", "todosProductos.csv");
storage.Salvar(todos, outputPath1);

