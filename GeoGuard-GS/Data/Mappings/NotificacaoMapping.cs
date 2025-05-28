using GeoGuard_GS.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeoGuard_GS.Data.Mappings
{
    public class NotificacaoMapping : IEntityTypeConfiguration<Notificacao>
    {
        public void Configure(EntityTypeBuilder<Notificacao> builder)
        {
            builder.ToTable("NOTIFICACAO", "RM554694"); 

            builder.HasKey(n => n.Id);

            builder.Property(n => n.Id)
                .HasColumnName("ID");

            builder.Property(n => n.Titulo)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("TITULO");

            builder.Property(n => n.Mensagem)
                .IsRequired()
                .HasMaxLength(1000)
                .HasColumnName("MENSAGEM");

            builder.Property(n => n.DataEnvio)
                .IsRequired()
                .HasColumnName("DATA_ENVIO");

            // FK opcional (se houver relacionamento com Usuario)
            builder.Property(n => n.UsuarioId)
                .HasColumnName("USUARIO_ID");

            builder.HasOne(n => n.Usuario)
                .WithMany(u => u.Notificacoes)
                .HasForeignKey(n => n.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
