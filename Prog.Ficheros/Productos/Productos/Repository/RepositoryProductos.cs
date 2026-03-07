using Productos.Models;

namespace Productos.Repository;

public class RepositoryProductos : IProductosRepository{
    private static readonly Lazy<RepositoryProductos> lazy = new(() => new());
    
    private readonly Dictionary<int, Models.Productos> _dictionary = new();

    private RepositoryProductos() {}
    
    public static RepositoryProductos Instance => lazy.Value;
    
    
    public IEnumerable<Models.Productos> GetAll() {
        return _dictionary.Values;    
    }

    public Models.Productos? GetById(int id) {
        return _dictionary.GetValueOrDefault(id);
    }

    public Models.Productos? Save(Models.Productos value) {
        if (_dictionary.ContainsKey(value.Id)) return null;
        
        _dictionary[value.Id] = value;
        //_dictionary.Add(value.Id, value);
        return value;
    }
}