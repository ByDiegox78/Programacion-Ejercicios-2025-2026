using Ficha.Enums;

namespace Ficha.Models;

public sealed record Dvd : Ficha
{
    public required string Director { get; init; }
    public int Anio { get; init; }
    public TipoDvd Tipo { get; init; }
    public bool IsDeleted { get; init; } = false;
    
    // Auditoria
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; init; } = DateTime.UtcNow;

    /*public bool Equals(Dvd? other) {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Anio == other.Anio && Director.Equals(other.Director, StringComparison.OrdinalIgnoreCase);
    }

    public override int GetHashCode() {
        return HashCode.Combine(Anio, Director);
    }*/
}