using GeoGuard_GS.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeoGuard_GS.Data.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("TB_USUARIO", "RM554694");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasColumnName("ID");

            builder.Property(u => u.Nome)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("NOME");

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("EMAIL");

            builder.Property(u => u.Senha)
              .IsRequired()
              .HasMaxLength(100)
              .HasColumnName("SENHA");

            builder.Property(u => u.Localizacao)
              .IsRequired()
              .HasMaxLength(100)
              .HasColumnName("LOCALIZACAO");
        }
    }
}
