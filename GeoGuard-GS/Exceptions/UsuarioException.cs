using System.Runtime.ConstrainedExecution;

namespace GeoGuard_GS.Exceptions
{
    public class UsuarioException : Exception
    {
        private const string MENSAGEM_PADRAO = "Usuário não encontrado";

        public UsuarioException(string? message = MENSAGEM_PADRAO) : base(message)
        {
        }
    }
}
