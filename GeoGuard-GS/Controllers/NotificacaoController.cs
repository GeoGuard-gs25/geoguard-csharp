using GeoGuard_GS.Dtos;
using GeoGuard_GS.Exceptions;
using GeoGuard_GS.Model.DTO;
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

                if (dto.UsuarioId <= 0) 
                    return BadRequest("O campo UsuarioId é obrigatório e deve ser maior que zero.");

                var notificacao = new Notificacao
                {
                    Titulo = dto.Titulo,
                    Mensagem = dto.Mensagem,
                    TipoMensagem = dto.TipoMensagem,
                    DataEnvio = dto.DataEnvio,
                    UsuarioId = dto.UsuarioId
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
                var notificacaoAtualizada = new Notificacao
                {
                    Id = id,
                    Titulo = dto.Titulo,
                    Mensagem = dto.Mensagem,
                    TipoMensagem = dto.TipoMensagem,
                    DataEnvio = dto.DataEnvio,
                    UsuarioId = dto.UsuarioId
                };

                await _notificacaoService.AtualizarAsync(id, notificacaoAtualizada);
                return NoContent();
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
