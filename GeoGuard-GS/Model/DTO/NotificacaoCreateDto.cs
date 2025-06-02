using System;
using System.ComponentModel.DataAnnotations;

namespace GeoGuard_GS.Dtos
{
    public class NotificacaoCreateDto
    {
        [Required, StringLength(100)]
        public string Titulo { get; set; }

        [Required, StringLength(500)]
        public string Mensagem { get; set; }

        [Required, StringLength(50)]
        public string TipoMensagem { get; set; }

        [Required]
        public DateTime DataEnvio { get; set; }

        [Required]
        public int UsuarioId { get; set; }
    }
}
