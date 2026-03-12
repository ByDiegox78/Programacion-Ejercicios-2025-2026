namespace CsvJsonXmlStorae.Config;

public static class Configuracion {
    public static readonly string DataFolder = Path.Combine(Environment.CurrentDirectory, "data");
    public static readonly string DataFolderJson = Path.Combine(Environment.CurrentDirectory, "json");
    public static readonly string DataFolderXml = Path.Combine(Environment.CurrentDirectory, "xml");


    public static readonly string CiudadanoFile = Path.Combine(DataFolder, "ciudadano.csv");
    public static readonly string CiudadanoFileJson = Path.Combine(DataFolderJson, "ciudadanoJson.json");
    public static readonly string CiudadanoFileXml = Path.Combine(DataFolderXml, "ciudadanoXml.xml");

    
}