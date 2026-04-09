using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace GestionItv.Config;

public static class Configuracion {
    
    private static readonly IConfiguration Config;
    //private static readonly string LetrasPermitidas = "BCDFGHJKLMNPRSTVWXYZ";
    public static readonly string LetrasDniPermitidas = "TRWAGMYFPDXBNJZSQVHLCKE";
    public static readonly Regex RegexDni = new Regex($@"^[0-9]{{8}}[{LetrasDniPermitidas}]$");
    public static readonly int MinCilindrada = 800;
    public static readonly int MaxCilindrada = 3000;
    public static readonly Regex RegexMatricula = new Regex(@"^[0-9]{4}[BCDFGHJKLMNPRSTVWXYZ]{3}$");    
    static Configuracion() {
        Config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
    }
    
    public static string RepositoryType {
        get {
            var type = Config.GetValue<string>("Repository:Type") ?? "memory";
            return type.ToLower() switch {
                "memory" => "memory",
                "binary" => "binary",
                "json" => "json",
                _ => "memory"
            };
        }
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
            return Path.Combine(DataFolder, $"itv.{extension}");
        }
    }
    public static string BackupFormat {
        get {
            var format = Config.GetValue<string>("Backup:Format") ?? "json";
            return format.ToLower() switch
            {
                "json" => "json",
                "xml" => "xml",
                "csv" => "csv",
                "bin" => "bin",
                _ => "json"
            };
        }
    }
    
    public static string BackupDirectory => Path.Combine(AppContext.BaseDirectory, Config.GetValue<string>("Backup:Directory") ?? "back");
}