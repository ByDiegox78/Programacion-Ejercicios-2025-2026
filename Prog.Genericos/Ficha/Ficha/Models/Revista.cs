namespace Ficha.Models;

public sealed record Revista: Ficha
{
    public int NumeroLista { get; init; }
    public int AnioPublicacion { get; init; }
    
    // Auditoria
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; init; } = DateTime.UtcNow;
    public bool IsDeleted { get; init; } = false;

    
    public bool Equals(Revista? other) {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

        return AnioPublicacion == other.AnioPublicacion && NumeroLista == other.NumeroLista;
    }

    public override int GetHashCode() {
        return HashCode.Combine(AnioPublicacion, NumeroLista);
    }
}