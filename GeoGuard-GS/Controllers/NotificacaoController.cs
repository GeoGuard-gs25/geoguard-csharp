using GeoGuard_GS.Dtos;
using GeoGuard_GS.Exceptions;
using GeoGuard_GS.Model;
using GeoGuard_GS.Model.DTO;
using GeoGuard_GS.Services;
using GeoGuard_GS.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace GeoGuard_GS.Controllers
{
    [ApiController]
    [Route("notificacoes")]
    public class NotificacaoController : ControllerBase
    {
        private readonly INotificacaoService _notificacaoService;


        public NotificacaoController(INotificacaoService notificacaoService)
        {
            _notificacaoService = notificacaoService;
        }


        [HttpPost("notificar")]
        public async Task<ActionResult<Notificacao>> Notificar(int id)
        {
            var notificacao = await _notificacaoService.Notificar(id);

            return Ok($"{notificacao.Mensagem},\n Tipo: {notificacao.TipoMensagem}");
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notificacao>>> GetAll()
        {
            try
            {
                var notificacoes = await _notificacaoService.GetAllAsync();
                return Ok(notificacoes);
            }
            catch (NotificacaoException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, "Erro interno: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Notificacao>> GetById(int id)
        {
            try
            {
                var notificacao = await _notificacaoService.GetByIdAsync(id);
                return Ok(notificacao);
            }
            catch (NotificacaoException ex)
            {
                return NotFound(ex.Message);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Notificacao>> Create([FromBody] NotificacaoCreateDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.Titulo))
                    return BadRequest("O campo Título é obrigatório.");

                if (string.IsNullOrWhiteSpace(dto.Mensagem))
                    return BadRequest("O campo Mensagem é obrigatório.");

                if (!dto.UsuarioId.HasValue)
                    return BadRequest("O campo UsuarioId é obrigatório.");

                var notificacao = new Notificacao
                {
                    Titulo = dto.Titulo,
                    Mensagem = dto.Mensagem,
                    TipoMensagem = dto.TipoMensagem,
                    DataEnvio = dto.DataEnvio,
                    UsuarioId = dto.UsuarioId.Value
                };

                var notificacaoCriada = await _notificacaoService.CriarAsync(notificacao);

                return CreatedAtAction(nameof(GetById), new { id = notificacaoCriada.Id }, notificacaoCriada);
            }
            catch (NotificacaoException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] NotificacaoUpdateDto dto)
        {
            try
            {
                if (id != dto.Id)
                    return BadRequest("ID da notificação inválido.");

                var notificacao = new Notificacao
                {
                    Id = dto.Id,
                    Titulo = dto.Titulo,
                    Mensagem = dto.Mensagem,
                    TipoMensagem = dto.TipoMensagem,
                    DataEnvio = dto.DataEnvio
                };

                await _notificacaoService.AtualizarAsync(id, notificacao);
                return NoContent();
            }
            catch (NotificacaoException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, "Erro interno: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _notificacaoService.DeletarAsync(id);
                return NoContent();
            }
            catch (NotificacaoException ex)
            {
                return NotFound(ex.Message);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, "Erro interno: " + ex.Message);
            }
        }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<Notificacao>>> GetByUsuarioId(int usuarioId)
        {
            try
            {
                var notificacoes = await _notificacaoService.GetByUsuarioIdAsync(usuarioId);
                return Ok(notificacoes);
            }
            catch (NotificacaoException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno: " + ex.Message);
            }
        }
    }
}
