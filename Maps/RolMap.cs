using ApiBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace ApiBackend.Maps
{
    public class RolMap : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            //builder.ToTable("Roles", schema: "auth");

            builder.HasKey(r => r.RolId);

            builder.Property(r => r.Nombre)
                .IsRequired()
                .HasMaxLength(50);

            // Seed Roles
            builder.HasData(
                new Rol { RolId = 1, Nombre = "Administrador" },
                new Rol { RolId = 2, Nombre = "CECOM" },
                new Rol { RolId = 3, Nombre = "CordinadorOperativo" },
                new Rol { RolId = 4, Nombre = "SupervisorArea" }
            );
        }
    }
}
