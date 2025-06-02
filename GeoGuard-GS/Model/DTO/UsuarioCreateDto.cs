using System.ComponentModel.DataAnnotations;

namespace GeoGuard_GS.Model.DTO
{
    public class UsuarioCreateDto
    {
        [Required, StringLength(100)]
        public string Nome { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, StringLength(100, MinimumLength = 6)]
        public string Senha { get; set; }

        [StringLength(150)]
        public string Localizacao { get; set; }
    }
}
