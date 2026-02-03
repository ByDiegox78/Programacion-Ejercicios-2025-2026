namespace Ficha.Models;

public abstract record Ficha
{
    public int Id { get; init; } = 0;
    public required string Nombre { get; set; }
    
    public virtual bool Equals(Ficha? other) {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Nombre.Equals(other.Nombre, StringComparison.OrdinalIgnoreCase);
    }
    public override int GetHashCode() {
        return HashCode.Combine(Nombre);
    }
    
}