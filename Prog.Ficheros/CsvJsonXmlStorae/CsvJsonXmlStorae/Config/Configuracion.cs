namespace CsvJsonXmlStorae.Config;

/// <summary>
/// Clase de configuración de las rutas de carpetas y archivos CSV, JSON y XML.
/// </summary>
public static class Configuracion
{
    /// <summary>
    /// Carpeta principal donde se almacenan todos los archibos.
    /// </summary>
    public static readonly string DataFolder = Path.Combine(Environment.CurrentDirectory, "data");
    
    /// <summary>
    /// Disccionario de todos los ficheros que s utilizaraz
    /// </summary>
    private static readonly Dictionary<string, string> Files = new() {
        //{ "txt", Path.Combine(DataFolder, "academia.txt") },
        { "csv", Path.Combine(DataFolder, "ciudadanos.csv") },
        { "json", Path.Combine(DataFolder, "ciudadanosJson.json") },
        { "xml", Path.Combine(DataFolder, "ciudadanosxml2.xml") },
        { "bin", Path.Combine(DataFolder,"ciudadanosbin.bin") },
    };
    
    public static string CiudadanosFile => Files["json"];
}