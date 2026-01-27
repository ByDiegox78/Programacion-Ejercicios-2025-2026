using System.Text.RegularExpressions;
using Ficha.Models;

namespace Ficha.Validator.LibroValidate;

public class LibroValidate : ILibroValidate
{
    private const int MinStringLength = 3;
    public static readonly string AutorEditorialRegexValidate = @"^[A-Za-zñÑ]{3,}";
    
    
    public Libro Validate(Libro libro) {

        if (string.IsNullOrWhiteSpace(libro.nombre)) {
            throw new ArgumentException("El nombre no puede estar vacio");
        }

        var nombreLongitud = libro.nombre.Length;

        if (nombreLongitud < MinStringLength) {
            throw new ArgumentOutOfRangeException(nameof(libro.nombre),
                $"El nombre no puede tener menos de ${MinStringLength} letras.");
        }

        if (!NombreValidate(libro.nombre)) {
            throw new ArgumentException("El nombre correcto no es acorde al formato");
        }
        
        
        if (string.IsNullOrWhiteSpace(libro.Editorial)) {
            throw new ArgumentException("La editorial no puede estar vacía.");
        }

        if (libro.Editorial.Length < MinStringLength) {
            throw new ArgumentOutOfRangeException(nameof(libro.Editorial),
                $"La editorial no puede tener menos de {MinStringLength} letras.");
        }

        if (!EditorialValidate(libro.Editorial)) {
            throw new ArgumentException("La editorial contiene caracteres no permitidos (solo letras y espacios).");
        }
        
        if (string.IsNullOrWhiteSpace(libro.Autor)) {
            throw new ArgumentException("El autor no puede estar vacío.");
        }

        if (libro.Autor.Length < MinStringLength) {
            throw new ArgumentOutOfRangeException(nameof(libro.Autor),
                $"El autor no puede tener menos de {MinStringLength} letras.");
        }

        if (!AutorValidate(libro.Autor)) {
            throw new ArgumentException("El autor contiene caracteres no permitidos (solo letras y espacios).");
        }

        // Si todo pasa, devolvemos el libro
        return libro;
        
    }

    private bool EditorialValidate(string editorial) {
        bool isOk = Regex.IsMatch(editorial, AutorEditorialRegexValidate);
        return isOk;
    }
    
    private bool NombreValidate(string nombre) {
        bool isOk = Regex.IsMatch(nombre, AutorEditorialRegexValidate);
        return isOk;
    }

    public bool AutorValidate(string autor) {
        bool isOk = Regex.IsMatch(autor, AutorEditorialRegexValidate);
        return isOk;
    }
}