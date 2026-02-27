// See https://aka.ms/new-console-template for more information

using Social_Media.Factory;
using Social_Media.Models;
using Vehiculos.Cache;

Console.WriteLine("Hello, World!");
var lista = PostFactory.DemoData();
var cache = new CacheLru<DateTime, List<Post>>();
var hoy = lista.Max(p => p.FechaPublicacion);
Console.WriteLine();
Console.WriteLine("==================================");
Console.WriteLine("Top 3 Posts del día");
Console.WriteLine("==================================");
Console.WriteLine();
List<Post> topDelDia = cache.Get(hoy);
if (topDelDia == null) {
    topDelDia = lista
        .Where(p => p.FechaPublicacion.Date == hoy.Date)
        .OrderByDescending(p => p.Likes + p.Compartidos)
        .Take(3)
        .ToList();
    cache.Add(hoy, topDelDia);
}
foreach (var p in topDelDia) {
    Console.WriteLine($"- {p.Autor}: {p.Contenido} ({p.Likes} likes)");
}
Console.WriteLine();
Console.WriteLine("\n==================================");
Console.WriteLine("Contenido Viral (>10,000 visualizaciones)");
Console.WriteLine("==================================");
Console.WriteLine();
var contenidoViral = lista
    .Where(n => n.Visualizaciones > 10000)
    .ToList();

contenidoViral.ForEach(Console.WriteLine);
Console.WriteLine();
Console.WriteLine("\n==================================");
Console.WriteLine("Alta Interacción (>5% de likes sobre visualizaciones)");
Console.WriteLine("==================================");
Console.WriteLine();
var altaInteraccion = lista
    .Where(p => p.Likes > p.Visualizaciones * 0.05)
    .ToList();

altaInteraccion.ForEach(Console.WriteLine);
Console.WriteLine();
Console.WriteLine("\n==================================");
Console.WriteLine("Posts Recientes (últimos 7 días)");
Console.WriteLine("==================================");
Console.WriteLine();
var recientes = lista
    .Where(p => p.FechaPublicacion >= new DateTime(2024, 1, 15).AddDays(-7))
    .ToList();

foreach (var p in recientes) {
    Console.WriteLine($"{p.Autor}: {p.Contenido} ({p.Likes} likes, {p.Compartidos} compartidos, {p.Visualizaciones} visualizaciones, {p.FechaPublicacion:dd/MM/yyyy})");
}
Console.WriteLine();
Console.WriteLine("\n==================================");
Console.WriteLine("Post más compartido en Video");
Console.WriteLine("==================================");
Console.WriteLine();
var especifico = lista
    .Where(p => p.Categoria == "Video")
    .OrderByDescending(n => n.Compartidos)
    .First();
Console.WriteLine($"Post más compartido en Video: {especifico.Autor}: {especifico.Contenido} ({especifico.Compartidos} compartidos)");

Console.WriteLine("\n==================================");
Console.WriteLine("Estadísticas Generales");
Console.WriteLine("==================================");
var totalGlobalVisualizaciones = lista.Sum(p => p.Visualizaciones);
var mediaInteraccion = lista
    .Where(p => p.Categoria == "Imagen")
    .Average(n => n.Likes);
Console.WriteLine($"Total de visualizaciones: {totalGlobalVisualizaciones}");
Console.WriteLine($"Media de likes en imágenes: {mediaInteraccion:F2}");

Console.WriteLine("\n==================================");
Console.WriteLine("Rendimiento por Categoría");
Console.WriteLine("==================================");
var rendimientoPorCategoria = lista
    .GroupBy(c => c.Categoria)
    .ToDictionary(g => g.Key, g => g.Sum(c => c.Visualizaciones));
foreach (var kv in rendimientoPorCategoria)
{
    Console.WriteLine($"{kv.Key}: {kv.Value} visualizaciones");
}

Console.WriteLine("\n==================================");
Console.WriteLine("Actividad por Autor");
Console.WriteLine("==================================");
var actividadPorAutor = lista
    .GroupBy(a => a.Autor)
    .OrderByDescending(g => g.Count())
    .ToDictionary(g => g.Key, g => g.Count());
foreach (var kv in actividadPorAutor)
{
    Console.WriteLine($"{kv.Key}: {kv.Value} posts");
}

Console.WriteLine("\n==================================");
Console.WriteLine("Análisis Temporal (media visualizaciones por mes)");
Console.WriteLine("==================================");
var analisisTemporal = lista
    .GroupBy(g => g.FechaPublicacion.Month)
    .ToDictionary(g => g.Key, g => g.Average(p => p.Visualizaciones));
foreach (var kv in analisisTemporal)
{
    Console.WriteLine($"Mes {kv.Key}: {kv.Value:F2} visualizaciones promedio");
}

Console.WriteLine("\n==================================");
Console.WriteLine("Ranking de Calidad");
Console.WriteLine("==================================");
var rankingCalidad = lista
    .OrderByDescending(n => n.Likes)
    .ThenByDescending(n => n.FechaPublicacion)
    .ToList();
foreach (var p in rankingCalidad)
{
    Console.WriteLine($"{p.Autor}: {p.Contenido} ({p.Likes} likes, {p.FechaPublicacion:dd/MM/yyyy})");
}

Console.WriteLine("\n==================================");
Console.WriteLine("Peores Registros (no Texto)");
Console.WriteLine("==================================");
var peoresRegistros = lista
    .Where(p => p.Categoria != "Texto")
    .OrderBy(n => n.Visualizaciones)
    .Take(5)
    .ToList();
foreach (var p in peoresRegistros)
{
    Console.WriteLine($"{p.Autor}: {p.Contenido} ({p.Visualizaciones} visualizaciones)");
}

Console.WriteLine("\n==================================");
Console.WriteLine("Categorías con >1000 interacciones");
Console.WriteLine("==================================");
var distinctCategorias = lista
    .Where(p => (p.Likes + p.Compartidos) > 1000)
    .Select(p => p.Categoria)
    .Distinct()
    .ToList();
foreach (var c in distinctCategorias)
{
    Console.WriteLine(c);
}