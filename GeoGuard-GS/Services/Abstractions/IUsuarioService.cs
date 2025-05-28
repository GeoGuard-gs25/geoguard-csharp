using GeoGuard_GS.Model;

namespace GeoGuard_GS.Services.Abstractions
{
    public interface IUsuarioService
    {
        Task<Usuario> CriarAsync(Usuario usuario);

        Task<Usuario> GetById(int id);

        Task<Usuario> GetByEmail(string email);

        Task<IEnumerable<Usuario>> GetAllAsync();

        Task<Usuario> AtualizarAsync(int id, Usuario usuarioAtualizado);

        Task DeletarAsync(int id);
    }
}