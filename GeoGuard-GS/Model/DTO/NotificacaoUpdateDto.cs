namespace GeoGuard_GS.Model.DTO
{
    public class NotificacaoUpdateDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Mensagem { get; set; }
        public string TipoMensagem { get; set; }
        public DateTime DataEnvio { get; set; }
        
    }
}
