using Productos.Models; 

namespace Productos.Models; 

public record Producto {
    public int Id { get; set; }
    public required string Name { get; set; }
    public required int Supplier { get; init; }
    public required int Categoria { get; init; }
    public required double UnitPrice { get; set; }
    public required int UnitInStock { get; set; } 
}