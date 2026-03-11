using Productos.Cache;
using Productos.Models;
using Productos.Repository;
using Productos.Service;
using Productos.Storage.Csv;
using static System.Console;

// 0. Configuración inicial de rutas y storage
// Usamos Path.Combine para asegurar compatibilidad entre Windows/Linux
string baseDir = AppContext.BaseDirectory; 
string dataDir = Path.Combine(baseDir, "data");
string inputPath = Path.Combine(dataDir, "products.csv");
string outputPath = Path.Combine(dataDir, "productos_criticos.csv");

// Inicializamos el servicio de almacenamiento
var storage = new ProductosCsStorage();
var service = new ProductosService(RepositoryProductos.Instance, new ProductosCsStorage(), new CacheLru<int, Producto>(5));

Console.WriteLine("--- Iniciando Procesador de Productos ---");
Console.WriteLine($"Buscando archivo en: {inputPath}");

// 1. Cargar datos
// El método Cargar devuelve IEnumerable<Producto>, lo convertimos a lista para trabajar en memoria
var productos = service.GetAll().ToList();

if (productos.Count == 0)
{
    Console.WriteLine("No se cargaron productos. Verifica que el archivo 'products.csv' esté en la carpeta 'data'.");
    return;
}

Console.WriteLine($"Total de productos cargados: {productos.Count}");

var pocosProductos = productos.Where(p => p.UnitInStock < 5).ToList();
Console.WriteLine($"Productos detectados con stock crítico (< 5): {pocosProductos.Count}");
foreach (var p in pocosProductos) {
    Console.WriteLine($"- ID: {p.Id} | {p.Name} (Stock: {p.UnitInStock})");
}
Console.WriteLine();
var stockMenor10 = productos
    .Where(p => p.UnitInStock < 10)
    .ToList();
Console.WriteLine($"Productos con stock < 10: {pocosProductos.Count}");
foreach (var p in stockMenor10) {
    Console.WriteLine($"- ID: {p.Id} | {p.Name} (Stock: {p.UnitInStock})");
}
WriteLine();
var stockMenor5Ordenado = productos
    .Where(s => s.UnitInStock < 5)
    .OrderBy(s => s.UnitInStock)
    .ToList();
Console.WriteLine($"Productos con stock < 5: {pocosProductos.Count}");
foreach (var p in stockMenor5Ordenado) {
    Console.WriteLine($"- ID: {p.Id} | {p.Name} (Stock: {p.UnitInStock})");
}

WriteLine();
var numeroDeProveedores = productos
    .GroupBy(s => s.Supplier)
    .Count();
WriteLine($"Cantidad de proveedores: {numeroDeProveedores}");
WriteLine();

var existenciasPorProducto = productos
    .Select(p => new { 
        Nombre = p.Name, 
        Existencias = p.UnitInStock 
    })
    .ToList();
WriteLine("Lista de existencias por producto");
foreach (var prod in existenciasPorProducto) {
    WriteLine($"Producto: {prod.Nombre,-35} | Existencias: {prod.Existencias}");
}
WriteLine();
var existenciasPorProveedor = productos
    .GroupBy(p => p.Supplier)
    .Select(s => new {
        ProveedorId = s.Key,
        Existencias = s.Count()
    }).ToList();
    
WriteLine("\n--- CANTIDAD DE PRODUCTOS POR PROVEEDOR ---");
foreach (var item in existenciasPorProveedor) {
    WriteLine($"Proveedor ID: {item.ProveedorId,-5} | Total productos: {item.Existencias}");
}

WriteLine();
WriteLine("\n--- PRECIO MEDIO DE PRODUCTOS POR PROVEEDOR ---");
var mediaProveedor = productos
    .GroupBy(p => p.Supplier)
    .Select(s => new {
        ProveedorId = s.Key,
        Media = s.Average(a => a.UnitPrice)
    }).ToList();
foreach (var item in mediaProveedor) {
    WriteLine($"Proveedor ID: {item.ProveedorId,-5} | Media precio productos: {item.Media:F2}");
}  

var productoMasCaro = productos
    .MaxBy(p => p.UnitPrice);

WriteLine(productoMasCaro != null 
    ? $"\n--- PRODUCTO MÁS CARO ---\nNombre: {productoMasCaro.Name} | Precio: {productoMasCaro.UnitPrice:F2}" 
    : "No hay productos disponibles.");

var proveedorConMas5Productos = productos
    .GroupBy(p => p.Supplier)
    .Where(g => g.Count() > 4)
    .Select(g => new {
        Name = g.Key,
        Proveedor = g.Count()
    }).ToList();

Console.WriteLine("\n--- PROVEEDORES CON MÁS DE 5 PRODUCTOS ---");
Console.WriteLine(proveedorConMas5Productos.Any() 
    ? string.Join("\n", proveedorConMas5Productos.Select(p => $"ID Proveedor: {p.Name} | Cantidad: {p.Proveedor}")) 
    : "No hay proveedores con más de 5 productos.");
//Puse 4 ya que n hay proveedores con mas de 5 productos

var a = productos
    .Where(p => p.UnitInStock > 0)
    .GroupBy(s => s.Supplier)
    .ToDictionary(k => k.Key, t => new {
        UnitInStock = t.Sum(s => s.UnitInStock),
        Averages = t.Sum(s => s.UnitInStock),
        Valor  = t.Sum(s => s.UnitPrice),
        Max = t.MaxBy(s => s.UnitPrice)
    })