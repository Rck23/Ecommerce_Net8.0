using Core.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructura.Data.Configuracion;

public class MarcasConfiguracion : IEntityTypeConfiguration<Marca>
{
    public void Configure(EntityTypeBuilder<Marca> builder)
    {
        builder.ToTable("Marca"); 
        builder.Property(p => p.Id).IsRequired();
        builder.Property(p => p.Nombre).IsRequired().HasMaxLength(100);
    }
}
