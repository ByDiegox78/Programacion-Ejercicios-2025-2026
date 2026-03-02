using Productos.Models;

// Usamos el nombre en singular 'Producto'
var p1 = new Producto {
    Name = "Ejemplo",
    Supplier = 1,
    Categoria = 1,
    UnitPrice = 10.5,
    UnitInStock = 100
};
var p2 = new Producto {
    Name = "Ejemplo2",
    Supplier = 1,
    Categoria = 1,
    UnitPrice = 10.5,
    UnitInStock = 100
};

Console.WriteLine($"ID: {p1.Id} - Nombre: {p1.Name}");
Console.WriteLine($"ID: {p2.Id} - Nombre: {p2.Name}");