using Productos.Dto;
using Productos.Models;

namespace Productos.Mappers;

public static class PoductosMapper {
    public static ProductosDto ToDto(this Models.Productos productoses) {
        return new ProductosDto(
            productoses.Id,
            productoses.Name,
            productoses.Supplier,
            productoses.Categoria,
            productoses.UnitPrice,
            productoses.UnitInStock
        );
    }

    public static Models.Productos ToModels(this ProductosDto dto) {
        return new Models.Productos {
            Id = dto.Id,
            Name = dto.Name,
            Supplier = dto.Supplier,
            Categoria = dto.Categoria,
            UnitPrice = dto.UnitPrice,
            UnitInStock = dto.UnitInPrice
        };
    }
}