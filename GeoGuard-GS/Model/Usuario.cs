using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GeoGuard_GS.Model
    {
    public class Usuario 
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Localizacao { get; set; }

        [JsonIgnore]
        public ICollection<Notificacao> Notificacoes { get; set; } = new List<Notificacao>();
    }
}

