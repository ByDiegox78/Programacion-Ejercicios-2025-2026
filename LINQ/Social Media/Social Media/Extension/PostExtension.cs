using Social_Media.Models;

namespace Social_Media.Extension;

public static class PostExtension {
    extension(List<Post> lista) {

        public List<Post> ContenidoViral() => lista
            .Where(n => n.Visualizaciones > 10000)
            .ToList();

        public List<Post> AltaIteracion() => lista
            .Where(p => p.Likes > p.Visualizaciones * 0.05)
            .ToList();
        public List<Post> Recientes() => lista
            .Where(p => p.FechaPublicacion >= DateTime.Now.AddDays(-7))
            .ToList();

        public Post Especifico() => lista
            .Where(p => p.Categoria.Contains("Video"))
            .OrderByDescending(n => n.Compartidos)
            .First();

        public object RatioDeEngagement() => lista
            .Where(p => p.Visualizaciones > 0)
            .Select(n => new {
                n.Id,
                n.Autor,
                Ratio = (double)(n.Likes + n.Compartidos) / n.Visualizaciones
            }).ToList();
        
        
        public double MediaInteraccion() => lista
            .Where(p => p.Categoria.Contains("Imagen"))
            .Average(n => n.Compartidos);

        public Dictionary<string, int> RendimientoPorCategoria() => lista
            .GroupBy(c => c.Categoria)
            .ToDictionary(
                p => p.Key, 
                p => p.Sum(c => c.Visualizaciones));

        public Dictionary<string, int> ActividadPorAitor() => lista
            .GroupBy(a => a.Autor)
            .OrderByDescending(g => g.Count())
            .ToDictionary (
                t => t.Key,
                t => t.Count()
            );

        public List<String> TopCreadores() => lista
            .GroupBy(p => p.Autor)
            .OrderByDescending(g => g.Sum(p => p.Likes))
            .Take(3)
            .Select(g => g.Key)
            .ToList();
        
        
        




    }
    
}
/*
public record Post(
    string Id,
    string Autor,
    string Contenido,
    int Visualizaciones,
    int Likes,
    int Compartidos,
    DateTime FechaPublicacion,
    string Categoria // "Video", "Imagen", "Texto"
);
*/