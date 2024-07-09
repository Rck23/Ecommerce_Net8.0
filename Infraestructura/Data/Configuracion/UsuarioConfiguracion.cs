using System.Runtime.InteropServices.Marshalling;
using Core.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructura.Data.Configuracion;

public class UsuarioConfiguracion : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuario");
        builder.Property(u => u.Id).IsRequired();
        builder.Property(u => u.Nombre).IsRequired().HasMaxLength(50);
        builder.Property(u => u.ApellidoPaterno).HasMaxLength(50);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(100);

        builder.HasMany(p => p.Roles).WithMany(p => p.Usuarios).UsingEntity<UsuariosRoles>(
            u => u.HasOne(pu => pu.Rol).WithMany(t => t.UsuariosRoles).HasForeignKey(ut => ut.RolId),
            u => u.HasOne(pu => pu.Usuario).WithMany(t => t.UsuariosRoles).HasForeignKey(ut => ut.UsuarioId),
            u => {
                u.HasKey(k => new {k.UsuarioId, k.RolId});
            }
        );
        builder.HasMany(u => u.RefreshTokens).WithOne(t => t.Usuario).HasForeignKey(t => t.UsuarioId);
    }
}
