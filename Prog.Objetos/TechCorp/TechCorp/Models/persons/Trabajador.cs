namespace TechCorp.Models;

public abstract record Trabajador {
    public required string Nombre { get; init; } = string.Empty;
    public int Id { get; init; }
    
    public override int GetHashCode() {
        return HashCode.Combine(Nombre);
    }
    public virtual bool Equals(Trabajador? other) {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Nombre.Equals(other.Nombre, StringComparison.OrdinalIgnoreCase);
    }
}