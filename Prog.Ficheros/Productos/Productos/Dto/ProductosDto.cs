namespace Productos.Dto;

public record ProductosDto(
    int Id,
    string Name,
    int Supplier,
    int Categoria,
    double UnitPrice,
    int UnitInPrice
    );