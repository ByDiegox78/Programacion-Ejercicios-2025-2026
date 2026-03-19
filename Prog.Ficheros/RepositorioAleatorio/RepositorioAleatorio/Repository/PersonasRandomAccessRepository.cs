using System.Linq.Expressions;
using RepositorioAleatorio.Dto;
using RepositorioAleatorio.Mapper;
using RepositorioAleatorio.Models;

namespace RepositorioAleatorio.Repository;

public class PersonasRandomAccessRepository : IPersonasRepository {
    
    // ============================================================
    // ARCHIVOS QUE USA EL SISTEMA
    // ============================================================

    /// <summary>
    /// Archivo principal donde se guardan las personas.
    /// Aquí están los datos reales (nombre, edad, email).
    /// Es como el "almacén".
    /// </summary>
    private const string FileDatos = "Data/personas.dat";

    /// <summary>
    /// Archivo índice.
    /// Sirve para saber en qué posición está cada persona dentro del archivo de datos.
    /// Evita tener que leer todo el archivo.
    /// 
    /// Guarda:
    /// - ID
    /// - posición (offset)
    /// - tamaño del registro
    /// 
    /// Es como el índice de un libro.
    /// </summary>
    private const string FileIndices = "Data/personas.idx";

    /// <summary>
    /// Archivo de huecos libres.
    /// Guarda los espacios vacíos que quedan cuando borras personas.
    /// 
    /// Así se pueden reutilizar en vez de ocupar más espacio.
    /// </summary>
    private const string FileHuecos = "Data/personas.frx";

    /// <summary>
    /// Porcentaje máximo de huecos permitido (30%).
    /// 
    /// Si el archivo tiene demasiados huecos vacíos,
    /// se reorganiza (compacta) para optimizar espacio.
    /// </summary>
    private const double FragmentationThreshold = 0.3;

    // ============================================================
    // ATRIBUTOS
    // ============================================================

    /// <summary>
    /// Contador para generar IDs únicos automáticamente.
    /// 
    /// Ejemplo:
    /// 1, 2, 3, 4...
    /// 
    /// Es estático → todas las instancias comparten el mismo valor.
    /// </summary>
    private static int _nextId = 1;
    
    private readonly List<(long posicion, int longitud)> _huecos;
    
    private readonly Dictionary<int, (long offset, int length)> _indice;

    public PersonasRandomAccessRepository() {
        if (!Directory.Exists("Data")) {
            Directory.CreateDirectory("Data");
        }
        _indice = new Dictionary<int, (long, int)>();
        _huecos = new List<(long, int)>();
        
        CargarArchivos();
    }
    
    public IEnumerable<Persona> GetAll() {
        var persona = _indice.Keys
             //Recorro todos los ids que hay en _indices
            .Select(GetById)
             //Filtro si solo es persona
            .OfType<Persona>()
            .ToList();
        return persona;
    }

    public Persona? GetById(int id) {
        if (!_indice.TryGetValue(id, out var persona)) {
            return null;
        }

        try {
            using var stream = new FileStream(FileIndices, FileMode.Open, FileAccess.Read);
            stream.Seek(persona.offset, SeekOrigin.Begin);
            
            var bytes = new byte[persona.length];
            var total = 0;
            while (total < persona.length) {
                var leido = stream.Read(bytes, total, persona.length - total);
                if (leido == 0) break;
                total += leido;
            }
            
            var personaSinId = DeserializePersona(bytes);
            return personaSinId with { Id = id };
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }

    public Persona? Create(Persona entity) {
        throw new NotImplementedException();
    }

    public Persona? Update(int id, Persona entity) {
        throw new NotImplementedException();
    }

    public Persona? Delete(int id) {
        throw new NotImplementedException();
    }
    
    
    private void CargarArchivos() {
        // Cargamos primero el índice y después los huecos
        CargarIndices();
        CargarHuecos();
    }
    
    
    /// <summary>
    /// Carga el archivo indices en personas.idx
    /// </summary>
    private void CargarIndices() {
        if (!File.Exists(FileIndices))
            return;

        try {
            using var stream = new FileStream(FileIndices, FileMode.Open, FileAccess.Read);
            using var reader = new BinaryReader(stream);
            
            _nextId = reader.ReadInt32();
            var cantidad = reader.ReadInt32();

            for (int i = 0; i < cantidad; i++) {
                var id = reader.ReadInt32();
                var offset = reader.ReadInt64();
                var longitud = reader.ReadInt32();
                
                _indice[id] = (offset, longitud);
            }
        }
        catch {
            // ignored
        }
    }
    /// <summary>
    /// Guarda los huecos libres en personas.frx
    /// </summary>
    private void CargarHuecos() {
        if (!File.Exists(FileHuecos))
            return;

        using var stream = new FileStream(FileHuecos, FileMode.Open, FileAccess.Read);
        using var reader = new BinaryReader(stream);

        var cantidad = reader.ReadInt32();
        for (var i = 0; i < cantidad; i++) {
            var posicion = reader.ReadInt64();
            var longitud = reader.ReadInt32();
            _huecos.Add((posicion, longitud));
        }
    }
    /// <summary>
    /// Guarda el indice en el archivo llamandolo cada vez que se modificque
    /// </summary>
    private void GuardarIndices() {
        using var stream = new FileStream(FileIndices, FileMode.Create, FileAccess.Write);
        using var writer = new BinaryWriter(stream);
        
        writer.Write(_nextId);
        writer.Write(_indice.Count);
        
        foreach (var kvp in _indice) {
            writer.Write(kvp.Key);
            writer.Write(kvp.Value.offset);
            writer.Write(kvp.Value.length);
        }
    }
    /// <summary>
    /// Guarda los huecos cada vez que se llama en el delete
    /// </summary>
    private void GuardarHuecos() {
        using var stream = new FileStream(FileHuecos, FileMode.Create, FileAccess.Write);
        using var writer = new BinaryWriter(stream);

        // Escribimos la cantidad de huecos
        writer.Write(_huecos.Count);

        // Escribimos cada hueco: posición + longitud
        foreach (var (posicion, longitud) in _huecos) {
            writer.Write(posicion);
            writer.Write(longitud);
        }
    }
    
    private Persona DeserializePersona(byte[] datos) {
        using var ms = new MemoryStream(datos);
        using var reader = new BinaryReader(ms);

        var nombre = reader.ReadString();
        var edad = reader.ReadInt32();
        var IsDeleted = reader.ReadBoolean();
        var CreatedAt = reader.ReadString();
        var UpdatedAt = reader.ReadString();
        
        var PersonaDTo = new PersonaDto(0, nombre, edad, IsDeleted, CreatedAt, UpdatedAt);

        return PersonaDTo.ToModel();

    }
}

