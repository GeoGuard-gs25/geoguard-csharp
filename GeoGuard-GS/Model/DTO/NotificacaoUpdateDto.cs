using System.ComponentModel.DataAnnotations;

namespace GeoGuard_GS.Model.DTO
{
    public class NotificacaoUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [StringLength(100)]
        public string Titulo { get; set; }

        [StringLength(500)]
        public string Mensagem { get; set; }

        [StringLength(50)]
        public string TipoMensagem { get; set; }

        [Required]
        public DateTime DataEnvio { get; set; }

        [Required]
        public int UsuarioId { get; set; }
    }
}
