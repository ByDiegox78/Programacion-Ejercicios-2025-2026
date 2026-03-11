using Productos.Models;

namespace Productos.Service;

public interface IServiceProductos {
    Models.Productos GetById(int id);
    
    IEnumerable<Models.Productos> GetAll();
    
    Models.Productos Save(Models.Productos productos);
    
    int ImportarDatos();
    
    int ExportarDatos();
}