using Productos.Models;

namespace Productos.Service;

public interface IServiceProductos {
    Producto GetById(int id);
    
    IEnumerable<Producto> GetAll();
    
    Producto Save(Producto producto);
    
    int ImportarDatos();
    
    int ExportarDatos();
}