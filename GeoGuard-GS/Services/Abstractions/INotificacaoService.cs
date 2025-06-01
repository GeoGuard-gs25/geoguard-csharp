using GeoGuard_GS.Model;

namespace GeoGuard_GS.Services.Abstractions
{
    public interface INotificacaoService
    {
        Task<Notificacao> CriarAsync(Notificacao notificacao);

        Task<Notificacao> GetByIdAsync(int id);

        Task<IEnumerable<Notificacao>> GetAllAsync();

        Task<IEnumerable<Notificacao>> GetByUsuarioIdAsync(int usuarioId);

        Task<Notificacao> AtualizarAsync(int id, Notificacao notificacaoAtualizada);

        Task DeletarAsync(int id);
        Task<Notificacao> Notificar(int idUsuario);
    }
}
