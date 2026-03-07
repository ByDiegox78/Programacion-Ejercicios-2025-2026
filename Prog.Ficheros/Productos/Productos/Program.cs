using System.Text;
using Productos.Cache;
using Productos.Models;
using Productos.Repository;
using Productos.Service;
using Productos.Storage.Csv;
using static System.Console;
Console.OutputEncoding = Encoding.UTF8;

// 1. Configuración de dependencias
// Usamos la instancia Singleton del repositorio que definiste
var repository = RepositoryProductos.Instance;

// Definimos un almacenamiento CSV para Productos
var storage = new ProductosCsStorage();

// Definimos una caché LRU con capacidad para 5 elementos
var cache = new CacheLru<int, Productos.Models.Productos>(5);

// Instanciamos el servicio inyectando las dependencias
var service = new ProductosService(repository, storage, cache);

Console.WriteLine("=== Sistema de Gestión de Productos ===");

try
{
    // 2. Simulación de Importación de Datos (si existe el archivo)
    Console.WriteLine("\nIntentando importar datos desde CSV...");
    int importados = service.ImportarDatos();
    Console.WriteLine($"Se importaron {importados} productos correctamente.");
}
catch (Exception ex)
{
    Console.WriteLine($"Aviso: No se pudieron importar datos (es posible que el archivo no exista aún). {ex.Message}");
    
    // 3. Si no hay datos, creamos algunos de prueba para probar el sistema
    Console.WriteLine("\nGenerando datos de prueba...");
    var p1 = new Productos.Models.Productos { Id = 1, Name = "Laptop Gaming", Supplier = 10, Categoria = 1, UnitPrice = 1200.50, UnitInStock = 5 };
    var p2 = new Productos.Models.Productos { Id = 2, Name = "Mouse Óptico", Supplier = 10, Categoria = 2, UnitPrice = 25.99, UnitInStock = 50 };
    var p3 = new Productos.Models.Productos { Id = 3, Name = "Monitor 4K", Supplier = 11, Categoria = 1, UnitPrice = 350.00, UnitInStock = 12 };

    service.Save(p1);
    service.Save(p2);
    service.Save(p3);
}

// 4. Probar la Caché y el Repositorio
Console.WriteLine("\n--- Consultando Producto con ID: 1 ---");
// La primera vez irá al repositorio y luego lo guardará en caché
var producto = service.GetById(1);
Console.WriteLine($"Producto recuperado: {producto.Name} - Precio: {producto.UnitPrice:C}");

Console.WriteLine("\n--- Consultando Producto con ID: 1 (Desde Caché) ---");
// Esta vez se recuperará directamente de CacheLru
var productoCached = service.GetById(1);
Console.WriteLine($"Producto recuperado de caché: {productoCached.Name}");

// 5. Listar todos los productos
Console.WriteLine("\n--- Listado Completo de Productos ---");
foreach (var p in service.GetAll())
{
    Console.WriteLine($"[{p.Id}] {p.Name} | Stock: {p.UnitInStock} | Cat: {p.Categoria}");
}

// 6. Exportar los datos a CSV
Console.WriteLine("\nExportando datos a la carpeta 'data'...");
int exportados = service.ExportarDatos();
Console.WriteLine($"Proceso finalizado. Se exportaron {exportados} registros.");
WriteLine();
Console.WriteLine("--- PULSE CUALQUIER TECLA PARA VER LAS CONSULTAS ---");
Console.ReadKey();
Console.Clear();


// --- CONSULTAS ADICIONALES ---
var productos = service.GetAll();
// 1. Valor total del inventario (Precio * Stock de todos los productos)
var valorTotalInventario = productos.Sum(p => p.UnitPrice * p.UnitInStock);
WriteLine($"\nVALOR TOTAL DEL INVENTARIO: {valorTotalInventario:C2}");

// 2. Producto más barato del catálogo
var productoMasBarato = productos.MinBy(p => p.UnitPrice);
WriteLine(productoMasBarato != null 
    ? $"\n--- PRODUCTO MÁS ECONÓMICO ---\nNombre: {productoMasBarato.Name} | Precio: {productoMasBarato.UnitPrice:C2}" 
    : "\nNo hay productos para determinar el más barato.");

// 3. Top 3 productos con mayor cantidad de existencias
var topStock = productos
    .OrderByDescending(p => p.UnitInStock)
    .Take(3)
    .ToList();

WriteLine("\n--- TOP 3 PRODUCTOS CON MÁS STOCK ---");
topStock.ForEach(p => WriteLine($"{p.Name,-35} | Unidades: {p.UnitInStock}"));

// 4. Resumen por Categoría: Cantidad de productos y Precio Medio
var resumenCategorias = productos
    .GroupBy(p => p.Categoria)
    .Select(g => new {
        CategoriaId = g.Key,
        Cantidad = g.Count(),
        PrecioPromedio = g.Average(p => p.UnitPrice)
    })
    .OrderBy(res => res.CategoriaId)
    .ToList();

WriteLine("\n--- RESUMEN POR CATEGORÍA ---");
foreach (var cat in resumenCategorias) {
    WriteLine($"Categoría ID: {cat.CategoriaId,-5} | Productos: {cat.Cantidad,-3} | Precio Medio: {cat.PrecioPromedio:C2}");
}

// 5. Productos cuyo precio es superior a la media general
double precioMedioGeneral = productos.Average(p => p.UnitPrice);
var productosSobreMedia = productos
    .Where(p => p.UnitPrice > precioMedioGeneral)
    .OrderByDescending(p => p.UnitPrice)
    .ToList();

WriteLine($"\n--- PRODUCTOS SOBRE EL PRECIO MEDIO ({precioMedioGeneral:C2}) ---");
foreach (var p in productosSobreMedia) {
    WriteLine($"{p.Name,-35} | Precio: {p.UnitPrice:C2}");
}

// 6. Total de unidades físicas en todo el almacén
int totalUnidades = productos.Sum(p => p.UnitInStock);
WriteLine($"\nTOTAL DE UNIDADES FÍSICAS EN ALMACÉN: {totalUnidades}");

// 7. Verificar si existe algún producto totalmente agotado (Stock = 0)
bool hayAgotados = productos.Any(p => p.UnitInStock == 0);
WriteLine($"\n¿Existen productos agotados actualmente?: {(hayAgotados ? "SÍ" : "NO")}");

// 8. Producto con el nombre más largo (curiosidad de cadena)
var nombreMasLargo = productos.MaxBy(p => p.Name.Length);
WriteLine($"\nPRODUCTO CON NOMBRE MÁS DETALLADO: {nombreMasLargo?.Name}");

// 9. Agrupación por rango de precio (Económicos < 20, Medio 20-100, Premium > 100)
var rangosPrecio = productos
    .GroupBy(p => p.UnitPrice switch {
        < 20 => "Económico ( < $20 )",
        <= 100 => "Rango Medio ( $20 - $100 )",
        _ => "Premium ( > $100 )"
    })
    .Select(g => new { Rango = g.Key, Cantidad = g.Count() })
    .ToList();

WriteLine("\n--- DISTRIBUCIÓN POR RANGOS DE PRECIO ---");
foreach (var r in rangosPrecio) {
    WriteLine($"{r.Rango,-25} : {r.Cantidad} productos");
}