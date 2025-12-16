using ApiBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiBackend.Maps
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //builder.ToTable("Users", schema: "auth");

            builder.HasKey(u => u.UserId);

            // Requerido con un maximo de tamaño
            builder.Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(15);

            // Claves foraneas
            builder.HasOne(u => u.Rol)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RolId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed Users
            var passwordHash = "AQAAAAIAAYagAAAAEFomUkpPDl69Vck9JWzkRoHgtVPHGarN53yJm6juPSGGENF27Dr3W1nnYLOTrw6q9g==";
            builder.HasData(
                new User {
                    UserId = 1,
                    UserName = "71245503",
                    Nombre = "TOLEDO EDWIN",
                    PasswordHash = passwordHash,
                    CreatedAt = new DateTime(2025, 12, 15, 22, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 12, 15, 22, 0, 0, DateTimeKind.Utc),
                    RolId = 1
                }
            );

        }
    }
}
