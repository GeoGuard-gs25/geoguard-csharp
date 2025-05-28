namespace GeoGuard_GS.Exceptions
{
    public class NotificacaoException : Exception
    {
        private const string MENSAGEM_PADRAO = "Notificação não encontrada";

        public NotificacaoException(string? message = MENSAGEM_PADRAO) : base(message)
        {
        }
    }
}
