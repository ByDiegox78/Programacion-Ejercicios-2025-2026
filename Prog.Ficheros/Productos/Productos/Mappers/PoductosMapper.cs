using Productos.Dto;
using Productos.Models;

namespace Productos.Mappers;

public static class PoductosMapper {
    public static ProductosDto ToDto(this Producto productos) {
        return new ProductosDto(
            productos.Id,
            productos.Name,
            productos.Supplier,
            productos.Categoria,
            productos.UnitPrice,
            productos.UnitInStock
        );
    }

    public static Producto ToModels(this ProductosDto dto) {
        return new Producto {
            Id = dto.Id,
            Name = dto.Name,
            Supplier = dto.Supplier,
            Categoria = dto.Categoria,
            UnitPrice = dto.UnitPrice,
            UnitInStock = dto.UnitInPrice
        };
    }
}