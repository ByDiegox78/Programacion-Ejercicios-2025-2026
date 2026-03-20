namespace GestionItv.Storage.Common;

public interface IStorage<T> {
    /// <summary>
    ///     Salva una colección de elementos en un archivo.
    /// </summary>
    /// <param name="items">Colección de los ciudadanos que guardemos.</param>
    /// <param name="path">Ruta del archivo donde se guardarán los datos.</param>
    public void WriteToFile(IEnumerable<T> items, string path);
    /// <summary>
    ///     Carga una colección de elementos desde un archivo.
    /// </summary>
    /// <param name="path">Ruta del archivo desde donde se cargarán los datos.</param>
    /// <returns>Colección de elementos cargados desde el archivo.</returns>
    public IEnumerable<T> ReadFromFile(string path);
}