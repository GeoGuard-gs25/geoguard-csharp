using GeoGuard_GS.Model;
using Microsoft.EntityFrameworkCore;
using GeoGuard_GS.Services.Abstractions;
using GeoGuard_GS.Exceptions;
using GeoGuard_GS.Model.DTO;

namespace GeoGuard_GS.Services
{
    public class NotificacaoService : INotificacaoService
    {
        private readonly AppDbContext _context;

        public NotificacaoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Notificacao> CriarAsync(Notificacao notificacao)
        {
            if (string.IsNullOrWhiteSpace(notificacao.Titulo))
                throw new NotificacaoException("O campo Título é obrigatório.");

            if (string.IsNullOrWhiteSpace(notificacao.Mensagem))
                throw new NotificacaoException("O campo Mensagem é obrigatório.");

            // Validação se o usuário existe, se UsuarioId for informado
            if (notificacao.UsuarioId != 0)
            {
                var usuarioExiste =  _context.Usuarios.FirstOrDefault(u => u.Id == notificacao.UsuarioId);
                if (usuarioExiste is null)
                    throw new NotificacaoException("Usuário informado não existe.");
            }

            _context.Notificacoes.Add(notificacao);
            await _context.SaveChangesAsync();

            return notificacao;
        }

        public async Task<Notificacao> Notificar(int idUsuario)
        {
            Notificacao notificacao = _context.Notificacoes.FirstOrDefault(p => p.UsuarioId == idUsuario);

            notificacao.DataEnvio = DateTime.Now;

            return notificacao;
        }

        public async Task<IEnumerable<Notificacao>> GetAllAsync()
        {
            var notificacoes = await _context.Notificacoes.Include(n => n.Usuario).ToListAsync();

            if (notificacoes == null || !notificacoes.Any())
                throw new NotificacaoException("Nenhuma notificação encontrada.");

            return notificacoes;
        }

        public async Task<Notificacao> GetByIdAsync(int id)
        {
            var notificacao = await _context.Notificacoes.Include(n => n.Usuario)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (notificacao == null)
                throw new NotificacaoException("Notificação não encontrada.");

            return notificacao;
        }

        public async Task<Notificacao> AtualizarAsync(int id, Notificacao notificacaoAtualizada)
        {
            var notificacao = await _context.Notificacoes.FirstOrDefaultAsync(n => n.Id == id);

            if (notificacao == null)
                throw new NotificacaoException("Notificação não encontrada.");

            notificacao.Titulo =    notificacaoAtualizada.Titulo;
            notificacao.Mensagem = notificacaoAtualizada.Mensagem;
            notificacao.TipoMensagem = notificacaoAtualizada.TipoMensagem;
            notificacao.DataEnvio = notificacaoAtualizada.DataEnvio;
            notificacao.UsuarioId = notificacaoAtualizada.UsuarioId;

            await _context.SaveChangesAsync();

            return notificacao;
        }

        public async Task DeletarAsync(int id)
        {
            var notificacao = await _context.Notificacoes.FirstOrDefaultAsync(n => n.Id == id);

            if (notificacao == null)
                throw new NotificacaoException("Notificação não encontrada.");

            _context.Notificacoes.Remove(notificacao);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Notificacao>> GetByUsuarioIdAsync(int usuarioId)
        {
            var notificacoes = await _context.Notificacoes
                .Where(n => n.UsuarioId == usuarioId)
                .Include(n => n.Usuario)
                .ToListAsync();

            if (notificacoes == null || !notificacoes.Any())
                throw new NotificacaoException("Nenhuma notificação encontrada para este usuário.");

            return notificacoes;
        }
    }
}
