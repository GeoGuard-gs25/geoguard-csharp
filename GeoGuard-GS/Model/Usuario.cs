using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GeoGuard_GS.Model
{
    public class Usuario
    {
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Nome { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

    [Required, StringLength(100, MinimumLength = 6)]
    public string Senha { get; set; }

    [StringLength(150)]
    public string Localizacao { get; set; }

    [JsonIgnore]
    public ICollection<Notificacao> Notificacoes { get; set; } = new List<Notificacao>();
    
    }
}

