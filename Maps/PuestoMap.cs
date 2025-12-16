using ApiBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiBackend.Maps
{
    public class PuestoMap : IEntityTypeConfiguration<Puesto>
    {
        public void Configure(EntityTypeBuilder<Puesto> builder)
        {
            builder.Property(er => er.Nombre)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(er => er.Activo)
                .HasDefaultValue(true);

            //builder.Property(er => er.CreatedAt)
            //    .HasDefaultValue("CURRENT_TIMESTAMP");

            //builder.Property(e => e.UpdatedAt)
            //    .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Seed EstadoRonda
            builder.HasData(
                new Puesto {
                    PuestoId = 1,
                    Nombre = "Tunshuruco",
                    CreatedAt = new DateTime(2025, 12, 15, 22, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 12, 15, 22, 0, 0, DateTimeKind.Utc)
                }
            );
        }
    }
}
