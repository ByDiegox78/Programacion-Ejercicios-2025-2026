using Productos.Cache;
using Productos.Config;
using Productos.Models;
using Productos.Repository;
using Productos.Storage.Common;

namespace Productos.Service;

public class ProductosService(IProductosRepository repository, IStorage<Producto> storage, ICached<int, Producto> cached) : IServiceProductos{
    public Producto GetById(int id) {
        var cache = cached.Get(id);
        if (cache != null) {
            return cache;
        }
        var personas = repository.GetById(id) ?? throw new KeyNotFoundException("No se encontro el id");
        cached.Add(id, personas);
        return personas;
    }

    public IEnumerable<Producto> GetAll() {
        return repository.GetAll();
    }

    public Producto Save(Producto producto) {
        var nueva = repository.Save(producto) ?? throw new InvalidOperationException("No se encontro el registro");
        return nueva;
    }

    public int ExportarDatos() {
        try {
            var productos = repository.GetAll().ToList();
            storage.Salvar(productos, Configuracion.ProductoFile);  
            return productos.Count;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw new InvalidOperationException("No se encontro el registro");
        }
    }

    public int ImportarDatos() {
        try {
            var personas = storage.Cargar(Configuracion.ProductoFile).ToList();
            if (!personas.Any()) {
                throw new InvalidOperationException($"No se encontraron datos para importar en el archivo {Configuracion.ProductoFile}.");
            }
            personas.ForEach(p => repository.Save(p));
            return personas.Count;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw new InvalidOperationException("Error al importar datos");
        }
    }
}