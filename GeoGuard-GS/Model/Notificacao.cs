using GeoGuard_GS.Model;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
public class Notificacao
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Titulo { get; set; }

    [Required, StringLength(500)]
    public string Mensagem { get; set; }

    [StringLength(50)]
    public string TipoMensagem { get; set; }

    public DateTime DataEnvio { get; set; } = DateTime.UtcNow;

    [Required]
    public int UsuarioId { get; set; }

    [JsonIgnore]
    public Usuario Usuario { get; set; }
}

