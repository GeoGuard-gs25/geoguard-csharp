using GeoGuard_GS.Model;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

/// <summary>
/// Representa uma notificação enviada para um usuário.
/// </summary>
public class Notificacao
{
    /// <summary>
    /// Identificador único da notificação.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Título da notificação.
    /// </summary>
    [Required, StringLength(100)]
    public string Titulo { get; set; }

    /// <summary>
    /// Mensagem de conteúdo da notificação.
    /// </summary>
    [Required, StringLength(500)]
    public string Mensagem { get; set; }

    /// <summary>
    /// Tipo da mensagem (ex: alerta, aviso, informativo).
    /// </summary>
    [StringLength(50)]
    public string TipoMensagem { get; set; }

    /// <summary>
    /// Data e hora em que a notificação foi enviada (UTC).
    /// </summary>
    public DateTime DataEnvio { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Chave estrangeira que representa o ID do usuário relacionado.
    /// </summary>
    [Required]
    public int UsuarioId { get; set; }

    /// <summary>
    /// Objeto de navegação para o usuário associado.
    /// </summary>
    [JsonIgnore]
    public Usuario Usuario { get; set; }
}
