using System.ComponentModel.DataAnnotations;

namespace GeoGuard_GS.Model.DTO
{
    public class UsuarioUpdateDto
    {
        [Required]
        public int Id { get; set; } 

        [StringLength(100)]
        public string Nome { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [StringLength(100, MinimumLength = 6)]
        public string Senha { get; set; }

        [StringLength(150)]
        public string Localizacao { get; set; }
    }
}
