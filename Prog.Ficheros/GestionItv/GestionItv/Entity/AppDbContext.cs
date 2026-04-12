using Microsoft.EntityFrameworkCore;

namespace GestionItv.Entity;

public class AppDbContext(string connection): DbContext {
    public DbSet<VehiculoEntity> Vehiculos { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder op) {
        op.UseSqlite(connection);
    }
}