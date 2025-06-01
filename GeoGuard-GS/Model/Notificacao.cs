using System.Text.Json.Serialization;

namespace GeoGuard_GS.Model
{
    public class Notificacao 
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Mensagem { get; set; }
        public string TipoMensagem { get; set; }
        public DateTime DataEnvio { get; set; }
        public int UsuarioId { get; set; }

        [JsonIgnore]
        public Usuario Usuario { get; set; }        
    }
}