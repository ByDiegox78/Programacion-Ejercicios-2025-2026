using Social_Media.Models;

namespace Social_Media.Factory;

public static class PostFactory {
    public static List<Post> DemoData() {
        var fechaActual = new DateTime(2024, 1, 15);

        return new List<Post>
        {
            new("P1", "AnaCreator", "Video de viaje a París", 15000, 800, 150, fechaActual.AddDays(-2), "Video"),
            new("P2", "AnaCreator", "Foto de comida saludable", 8000, 400, 50, fechaActual.AddDays(-5), "Imagen"),
            new("P3", "TechBlog", "Review del nuevo móvil", 25000, 1200, 300, fechaActual.AddDays(-1), "Video"),
            new("P4", "TechBlog", "5 consejos de programación", 12000, 600, 100, fechaActual.AddDays(-3), "Texto"),
            new("P5", "ViajesMax", "Mi experiencia en Tokio", 30000, 1500, 400, fechaActual.AddDays(-7), "Video"),
            new("P6", "ViajesMax", "Fotos de atardecer", 5000, 250, 30, fechaActual.AddDays(-10), "Imagen"),
            new("P7", "AnaCreator", "Tutorial de maquillaje", 10000, 700, 80, fechaActual.AddDays(-4), "Video"),
            new("P8", "TechBlog", "Unboxing tecnología", 18000, 900, 200, fechaActual.AddDays(-6), "Video"),
            new("P9", "FitnessPro", "Rutina de ejercicios", 22000, 1100, 250, fechaActual.AddDays(-2), "Video"),
            new("P10", "FitnessPro", "Tips de nutrición", 8000, 350, 45, fechaActual.AddDays(-8), "Texto"),
            new("P11", "ViajesMax", "Best beaches 2024", 15000, 800, 120, fechaActual.AddDays(-12), "Imagen"),
            new("P12", "TechBlog", "IA y el futuro", 20000, 950, 180, fechaActual.AddDays(-9), "Texto")
        };
    }
}