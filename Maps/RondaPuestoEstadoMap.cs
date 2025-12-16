using ApiBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiBackend.Maps
{
    public class RondaPuestoEstadoMap : IEntityTypeConfiguration<RondaPuestoEstado>
    {
        public void Configure(EntityTypeBuilder<RondaPuestoEstado> builder)
        {
            //builder.ToTable("EstadosRondasPuestos");

            builder.HasKey(er => er.RondaPuestoEstadoId);

            builder.Property(er => er.Nombre)
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(er => er.Activo)
                .HasDefaultValue(true);

            // Seed EstadoRonda
            builder.HasData(
                new RondaPuestoEstado { RondaPuestoEstadoId = 1, Nombre = "Pendiente" },
                new RondaPuestoEstado { RondaPuestoEstadoId = 2, Nombre = "En supervisión" },
                new RondaPuestoEstado { RondaPuestoEstadoId = 3, Nombre = "Con observaciones" },
                new RondaPuestoEstado { RondaPuestoEstadoId = 4, Nombre = "Completado", Activo = false },
                new RondaPuestoEstado { RondaPuestoEstadoId = 5, Nombre = "No visitado", Activo = false }
            );
        }
    }
}
