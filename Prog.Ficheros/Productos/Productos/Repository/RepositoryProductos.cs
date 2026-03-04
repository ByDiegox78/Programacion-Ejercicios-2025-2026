using Productos.Models;

namespace Productos.Repository;

public class RepositoryProductos : IProductosRepository{
    private static readonly Lazy<RepositoryProductos> lazy = new(() => new());
    
    private readonly Dictionary<int, Producto> _dictionary = new();

    private RepositoryProductos() {}
    
    public static RepositoryProductos Instance => lazy.Value;
    
    
    public IEnumerable<Producto> GetAll() {
        return _dictionary.Values;    
    }

    public Producto? GetById(int id) {
        return _dictionary.GetValueOrDefault(id);
    }

    public Producto? Save(Producto value) {
        if (_dictionary.ContainsKey(value.Id)) return null;
        
        _dictionary[value.Id] = value;
        //_dictionary.Add(value.Id, value);
        return value;
    }
}