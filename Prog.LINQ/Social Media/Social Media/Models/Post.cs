namespace Social_Media.Models;

public record Post(
    string Id,
    string Autor,
    string Contenido,
    int Visualizaciones,
    int Likes,
    int Compartidos,
    DateTime FechaPublicacion,
    string Categoria // "Video", "Imagen", "Texto"


) {
    public override string ToString()
    {
        return $"{Autor}: {Contenido} ({Likes} likes, {Compartidos} compartidos, {Visualizaciones} visualizaciones)";
    }
}
