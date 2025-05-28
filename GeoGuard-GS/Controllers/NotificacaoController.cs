using GeoGuard_GS.Exceptions;
using GeoGuard_GS.Model;
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
        public async Task<ActionResult<Notificacao>> Create(Notificacao notificacao)
        {
            try
            {
                var notificacaoEntity = await _notificacaoService.CriarAsync(notificacao);
                return CreatedAtAction(nameof(GetById), new { id = notificacaoEntity.Id }, notificacaoEntity);
            }
            catch (NotificacaoException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Notificacao notificacaoAtualizada)
        {
            try
            {
                if (id != notificacaoAtualizada.Id)
                    return BadRequest("ID da notificação inválido.");

                await _notificacaoService.AtualizarAsync(id, notificacaoAtualizada);
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
    }
}
