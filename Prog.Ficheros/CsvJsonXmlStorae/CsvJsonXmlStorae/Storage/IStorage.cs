namespace CsvJsonXmlStorae.Storage;
/// <summary>
///     
/// </summary>
/// <typeparam name="T">Tipo de dato generico</typeparam>
public interface IStorage<T> {
    /// <summary>
    ///     Salva una colección de elementos en un archivo.
    /// </summary>
    /// <param name="items">Colección de los ciudadanos que guardemos.</param>
    /// <param name="path">Ruta del archivo donde se guardarán los datos.</param>
    public void Salvar(IEnumerable<T> items, string path);
    /// <summary>
    ///     Carga una colección de elementos desde un archivo.
    /// </summary>
    /// <param name="path">Ruta del archivo desde donde se cargarán los datos.</param>
    /// <returns>Colección de elementos cargados desde el archivo.</returns>
    public IEnumerable<T> Cargar(string path);
}