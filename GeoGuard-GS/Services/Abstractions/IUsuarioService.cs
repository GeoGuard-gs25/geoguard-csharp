using GeoGuard_GS.Model;
using GeoGuard_GS.Model.DTO;

namespace GeoGuard_GS.Services.Abstractions
{
    public interface IUsuarioService
    {
        Task<Usuario> CriarAsync(UsuarioDTO usuario);

        Task<Usuario> GetById(int id);

        Task<Usuario> GetByEmail(string email);

        Task<IEnumerable<Usuario>> GetAllAsync();

        Task<Usuario> AtualizarAsync(int id, Usuario usuarioAtualizado);

        Task DeletarAsync(int id);
    }
}