using ApiBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiBackend.Maps
{
    public class RondaEstadoMap : IEntityTypeConfiguration<RondaEstado>
    {
        public void Configure(EntityTypeBuilder<RondaEstado> builder)
        {
            //builder.ToTable("EstadoRondas");

            builder.HasKey(er => er.RondaEstadoId);

            builder.Property(er => er.Nombre)
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(er => er.Activo)
                .HasDefaultValue(true);

            // Seed EstadoRonda
            builder.HasData(
                new RondaEstado { RondaEstadoId = 1, Nombre = "Pendiente" },
                new RondaEstado { RondaEstadoId = 2, Nombre = "En proceso" },
                new RondaEstado { RondaEstadoId = 3, Nombre = "Finalizada", Activo = false },
                new RondaEstado { RondaEstadoId = 4, Nombre = "Cancelada", Activo = false }
            );
        }
    }
}
