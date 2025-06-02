using GeoGuard_GS.Model;
using GeoGuard_GS.Model.DTO;

namespace GeoGuard_GS.Services.Abstractions
{
    public interface IUsuarioService
    {
        Task<Usuario> CriarAsync(UsuarioCreateDto usuario);
        Task<Usuario> GetById(int id);
        Task<Usuario> GetByEmail(string email);
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task DeletarAsync(int id);
        Task<Usuario> AtualizarAsync(int id, UsuarioUpdateDto usuarioDto);
    }
}