using Productos.Models;
using Productos.Repository.Common;

namespace Productos.Repository;

public interface IProductosRepository : ICrudRepository<int, Producto>;