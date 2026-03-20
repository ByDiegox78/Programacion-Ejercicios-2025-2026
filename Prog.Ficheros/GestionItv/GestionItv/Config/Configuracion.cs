using Microsoft.Extensions.Configuration;

namespace GestionItv.Config;

public static class Configuracion {
    private static readonly IConfiguration Config;
    static Configuracion() {
        // NOTA PARA EL ALUMNO: Cargamos la configuración desde el archivo JSON externo.
        // Esto permite cambiar el tipo de almacenamiento o la ruta sin recompilar el código.
        Config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
    }
    
    public static string DataFolder => Path.Combine(Environment.CurrentDirectory, Config.GetValue<string>("Repository:Directory") ?? "data");
    
    public static string StorageType => Config.GetValue<string>("Storage:Type") ?? "json";

    public static string VehiculoFile {
        get {
            var extension = StorageType.ToLower() switch {
                "json" => "json",
                "xml" => "xml",
                "csv" => "csv",
                "bin" => "bin",
                _ => "json" 
            };
            return Path.Combine(DataFolder, $"academia.{extension}");
        }
    }
}