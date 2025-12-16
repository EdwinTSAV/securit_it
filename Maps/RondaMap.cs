using ApiBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiBackend.Maps
{
    public class RondaMap : IEntityTypeConfiguration<Ronda>
    {
        public void Configure(EntityTypeBuilder<Ronda> builder)
        {
            builder.HasKey(r => r.RondaId);

            //builder.Property(er => er.CreatedAt)
            //    .HasDefaultValue("CURRENT_TIMESTAMP");

            //builder.Property(e => e.UpdatedAt)
            //    .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.CoordinadorId)
                .HasDefaultValue(null)
                 .IsRequired(false);

            builder.HasOne(u => u.Supervisor)
                .WithMany(r => r.RondasSupervisores)
                .HasForeignKey(u => u.SupervisorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(u => u.Coordinador)
                .WithMany(r => r.RondasCoordinadores)
                .HasForeignKey(u => u.CoordinadorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.RondaEstado)
                .WithMany(r => r.Rondas)
                .HasForeignKey(r => r.EstadoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
