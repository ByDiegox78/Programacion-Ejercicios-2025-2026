namespace Productos.Storage.Common;

public interface IStorage<T> {
    
    public void Salvar(IEnumerable<T> items, string path);

    public IEnumerable<T> Carrgar(string path);
}