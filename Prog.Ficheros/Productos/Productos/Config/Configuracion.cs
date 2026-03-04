namespace Productos.Config;

public static class Configuracion {
    public static readonly string DataFolder = Path.Combine(Environment.CurrentDirectory, "data");
    public static readonly string ProductoFile = Path.Combine(DataFolder, "producto.csv");
    
}