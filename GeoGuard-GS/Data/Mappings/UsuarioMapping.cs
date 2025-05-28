using GeoGuard_GS.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Challenge_MOTTU.Data.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("USUARIO", "RM554694");

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

            builder.Property(u => u.ReceberNotificacoes)
                .IsRequired()
                .HasColumnName("RECEBER_NOTIFICACOES")
                .HasColumnType("NUMBER(1)"); // Oracle não tem BOOLEAN, usamos NUMBER(1)
        }
    }
}
