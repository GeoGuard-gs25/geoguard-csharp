namespace GeoGuard_GS.DTO
{
    public class NotificacaoCreateDto
    {
        public string Titulo { get; set; }
        public string Mensagem { get; set; }
        public string Tipo { get; set; }
        public DateTime DataEnvio { get; set; }
        public int UsuarioId { get; set; }
    }
}
