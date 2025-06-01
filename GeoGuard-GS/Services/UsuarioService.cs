using GeoGuard_GS.Exceptions;
using GeoGuard_GS.Model;
using GeoGuard_GS.Model.DTO;
using GeoGuard_GS.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace GeoGuard_GS.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AppDbContext _usuarioService;
        private readonly INotificacaoService _notificacaoService;

        public UsuarioService(AppDbContext context, INotificacaoService notificacaoService)
        {
            _usuarioService = context;
            _notificacaoService = notificacaoService;
        }

        public async Task<Usuario> CriarAsync(UsuarioDTO usuariodto)
        {
            if (string.IsNullOrWhiteSpace(usuariodto.Nome))
                throw new UsuarioException("O campo Nome é obrigatório.");

            if (string.IsNullOrWhiteSpace(usuariodto.Email))
                throw new UsuarioException("O campo Email é obrigatório.");

            var emailEmUso = _usuarioService.Usuarios.FirstOrDefault(p => p.Email.Trim() == usuariodto.Email.Trim());

            if (emailEmUso != null)
                throw new UsuarioException("O email informado já está em uso.");

            var usuario = new Usuario()
            {
                Nome = usuariodto.Nome,
                Email = usuariodto.Email,
                Senha = usuariodto.Senha,
                Localizacao = usuariodto.Localizacao
            };

            _usuarioService.Usuarios.Add(usuario);
            _usuarioService.SaveChanges();

            Notificacao notificacao = new Notificacao()
            {
                Mensagem = $"Olá {usuario.Nome} está ocorrendo uma chama na sua localização {usuario.Localizacao}",
                UsuarioId = usuario.Id,
                DataEnvio = DateTime.Now,
                Titulo = "Notificação"
            };

           await _notificacaoService.CriarAsync(notificacao);

            return usuario;
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            var usuarios = await _usuarioService.Usuarios.ToListAsync();

            if (usuarios == null || !usuarios.Any())
                throw new UsuarioException("Nenhum usuário encontrado.");

            return usuarios;
        }

        public async Task<Usuario> GetByEmail(string? email)
        {
            if (string.IsNullOrEmpty(email))
                throw new UsuarioException("E-mail está vazio");

            Usuario? usuario = _usuarioService.Usuarios.FirstOrDefault(p => p.Email == email);

            if (usuario is null)
                throw new UsuarioException("Usuário com este e-mail não existe");

            return usuario;
        }

        public async Task<Usuario> GetById(int id)
        {
            var usuario = _usuarioService.Usuarios.FirstOrDefault(u => u.Id == id);

            if (usuario is null)
                throw new UsuarioException();

            return usuario;
        }

        public async Task<Usuario> AtualizarAsync(int id, Usuario usuarioAtualizado)
        {
            var usuario = await _usuarioService.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
            if (usuario == null)
                throw new UsuarioException("Usuário não encontrado.");

            usuario.Nome = usuarioAtualizado.Nome;
            usuario.Email = usuarioAtualizado.Email;
            usuario.Senha = usuarioAtualizado.Senha;

            await _usuarioService.SaveChangesAsync();
            return usuario;
        }

        public async Task DeletarAsync(int id)
        {
            var usuario = await _usuarioService.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
            if (usuario == null)
                throw new UsuarioException("Usuário não encontrado.");

            _usuarioService.Usuarios.Remove(usuario);
            await _usuarioService.SaveChangesAsync();
        }
    }
}
