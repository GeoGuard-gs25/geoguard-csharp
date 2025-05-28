using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoGuard_GS.Model
    {
    public class Usuario 
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Localizacao { get; set; }

        [Column(TypeName = "NUMBER(1)")]
        public bool ReceberNotificacoes { get; set; }

        public ICollection<Notificacao> Notificacoes { get; set; }
    }
}

