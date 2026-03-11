namespace CsvJsonXmlStorae.Storage;

public interface IStorage<T> {
    public void Salvar(IEnumerable<T> items, string path);
    
    public IEnumerable<T> Cargar(string path);
}