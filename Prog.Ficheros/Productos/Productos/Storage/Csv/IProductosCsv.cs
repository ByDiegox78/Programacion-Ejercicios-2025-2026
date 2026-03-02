using Productos.Models;
using Productos.Storage.Common;

namespace Productos.Storage.Csv;

public interface IProductosCsv : IStorage<Producto>;