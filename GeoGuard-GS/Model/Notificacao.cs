namespace GeoGuard_GS.Model
{
    public class Notificacao 
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Mensagem { get; set; }
        public string Tipo { get; set; }
        public DateTime DataEnvio { get; set; }

        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }        
    }
}