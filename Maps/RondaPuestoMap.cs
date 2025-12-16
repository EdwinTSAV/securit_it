using ApiBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiBackend.Maps
{
    public class RondaPuestoMap : IEntityTypeConfiguration<RondaPuesto>
    {
        public void Configure(EntityTypeBuilder<RondaPuesto> builder)
        {
            builder.HasKey(u => u.RondaPuestoId);

            builder.HasOne(u => u.Ronda)
                .WithMany(r => r.RondaPuestos)
                .HasForeignKey(u => u.RondaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.Puesto)
                .WithMany(r => r.RondaPuestos)
                .HasForeignKey(r => r.PuestoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.RondaPuestoEstado)
                .WithMany(r => r.RondaPuestos)
                .HasForeignKey(r => r.RondaPuestoEstadoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
