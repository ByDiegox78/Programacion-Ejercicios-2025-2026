namespace Ficha.Models;

public sealed record Libro : Ficha
{
    public required string Editorial { get; init; }
    public required string Autor { get; init; }
    
    // Auditoria
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; init; } = DateTime.UtcNow;
    public bool IsDeleted { get; init; } = false;

    
    /*public bool Equals(Libro? other) {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

        return 
            Editorial.Equals(other.Editorial, StringComparison.OrdinalIgnoreCase) && 
            Autor.Equals(other.Autor, StringComparison.OrdinalIgnoreCase);
    }

    public override int GetHashCode() {
        return HashCode.Combine(Editorial, Autor);
    }*/
}