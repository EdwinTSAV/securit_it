using ApiBackend.Maps;
using ApiBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Rol> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Puesto> Puestos { get; set; }
        public DbSet<RondaEstado> RondaEstados { get; set; }
        public DbSet<RondaPuestoEstado> RondaPuestoEstados { get; set; }
        public DbSet<Ronda> Rondas { get; set; }
        public DbSet<RondaPuesto> RondaPuestos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RolMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new PuestoMap());
            modelBuilder.ApplyConfiguration(new RondaEstadoMap());
            modelBuilder.ApplyConfiguration(new RondaPuestoEstadoMap());
            modelBuilder.ApplyConfiguration(new RondaMap());
            modelBuilder.ApplyConfiguration(new RondaPuestoMap());

            modelBuilder.Entity<Ronda>()
                .HasQueryFilter(r => !r.IsDeleted);
        }
        public override int SaveChanges()
        {
            ApplyAuditInfo();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            ApplyAuditInfo();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void ApplyAuditInfo()
        {
            var entries = ChangeTracker.Entries<BaseEntity>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    entry.Entity.IsDeleted = false;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
            }
        }
    }
}
