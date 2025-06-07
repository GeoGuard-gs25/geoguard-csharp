using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GeoGuard_GS.Model
{
    /// <summary>
    /// Representa um usuário do sistema.
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Identificador único do usuário.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome completo do usuário.
        /// </summary>
        [Required, StringLength(100)]
        public string Nome { get; set; }

        /// <summary>
        /// Endereço de e-mail do usuário.
        /// </summary>
        [Required, EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Senha do usuário (mínimo de 6 caracteres).
        /// </summary>
        [Required, StringLength(100, MinimumLength = 6)]
        public string Senha { get; set; }

        /// <summary>
        /// Localização ou endereço do usuário.
        /// </summary>
        [StringLength(150)]
        public string Localizacao { get; set; }

        /// <summary>
        /// Lista de notificações relacionadas a este usuário.
        /// </summary>
        [JsonIgnore]
        public ICollection<Notificacao> Notificacoes { get; set; } = new List<Notificacao>();
    }
}
