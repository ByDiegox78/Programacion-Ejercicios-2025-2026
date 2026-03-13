namespace CsvJsonXmlStorae.Config;

/// <summary>
/// Clase de configuración de las rutas de carpetas y archivos CSV, JSON y XML.
/// </summary>
public static class Configuracion
{
    /// <summary>
    /// Carpeta principal donde se almacenan los archivos CSV.
    /// </summary>
    public static readonly string DataFolder = Path.Combine(Environment.CurrentDirectory, "data");

    /// <summary>
    /// Carpeta donde se almacenan los archivos en formato JSON.
    /// </summary>
    public static readonly string DataFolderJson = Path.Combine(Environment.CurrentDirectory, "json");

    /// <summary>
    /// Carpeta donde se almacenan los archivos en formato XML.
    /// </summary>
    public static readonly string DataFolderXml = Path.Combine(Environment.CurrentDirectory, "xml");

    /// <summary>
    /// Ruta completa del archivo CSV.
    /// </summary>
    public static readonly string CiudadanoFile = Path.Combine(DataFolder, "ciudadano.csv");

    /// <summary>
    /// Ruta completa del archivo JSON.
    /// </summary>
    public static readonly string CiudadanoFileJson = Path.Combine(DataFolderJson, "ciudadanoJson.json");

    /// <summary>
    /// Ruta completa del archivo XML.
    /// </summary>
    public static readonly string CiudadanoFileXml = Path.Combine(DataFolderXml, "ciudadanoXml.xml");
}