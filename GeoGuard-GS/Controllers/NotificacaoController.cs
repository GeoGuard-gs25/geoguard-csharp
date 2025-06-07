using GeoGuard_GS.Dtos;
using GeoGuard_GS.Exceptions;
using GeoGuard_GS.Model.DTO;
using GeoGuard_GS.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations; // IMPORTANTE: adicionar este using

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

        /// <summary>
        /// Retorna todas as notificações cadastradas.
        /// </summary>
        /// <returns>Lista de notificações.</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Retorna todas as notificações", Description = "Obtém uma lista completa de notificações cadastradas no sistema.")]
        [SwaggerResponse(200, "Lista de notificações retornada com sucesso.")]
        [SwaggerResponse(400, "Erro de validação ou de negócio.")]
        [SwaggerResponse(500, "Erro interno no servidor.")]
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

        /// <summary>
        /// Retorna uma notificação específica pelo ID.
        /// </summary>
        /// <param name="id">ID da notificação.</param>
        /// <returns>Notificação correspondente.</returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Retorna uma notificação pelo ID", Description = "Obtém uma notificação específica com base no ID informado.")]
        [SwaggerResponse(200, "Notificação encontrada com sucesso.")]
        [SwaggerResponse(404, "Notificação não encontrada.")]
        [SwaggerResponse(400, "Erro de requisição.")]
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

        /// <summary>
        /// Cria uma nova notificação.
        /// </summary>
        /// <param name="dto">Dados para criação da notificação.</param>
        /// <returns>Notificação criada.</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Cria uma nova notificação", Description = "Cria uma nova notificação com base nos dados fornecidos.")]
        [SwaggerResponse(201, "Notificação criada com sucesso.")]
        [SwaggerResponse(400, "Erro de validação ou de negócio.")]
        [SwaggerResponse(500, "Erro interno no servidor.")]
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

        /// <summary>
        /// Atualiza uma notificação existente.
        /// </summary>
        /// <param name="id">ID da notificação a ser atualizada.</param>
        /// <param name="dto">Dados atualizados da notificação.</param>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualiza uma notificação", Description = "Atualiza os dados de uma notificação com base no ID informado.")]
        [SwaggerResponse(204, "Notificação atualizada com sucesso.")]
        [SwaggerResponse(400, "Erro de validação ou de negócio.")]
        [SwaggerResponse(500, "Erro interno no servidor.")]
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

        /// <summary>
        /// Remove uma notificação pelo ID.
        /// </summary>
        /// <param name="id">ID da notificação a ser excluída.</param>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Remove uma notificação", Description = "Remove uma notificação do sistema com base no ID informado.")]
        [SwaggerResponse(204, "Notificação removida com sucesso.")]
        [SwaggerResponse(404, "Notificação não encontrada.")]
        [SwaggerResponse(500, "Erro interno no servidor.")]
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

        /// <summary>
        /// Retorna todas as notificações associadas a um usuário.
        /// </summary>
        /// <param name="usuarioId">ID do usuário.</param>
        /// <returns>Lista de notificações do usuário.</returns>
        [HttpGet("usuario/{usuarioId}")]
        [SwaggerOperation(Summary = "Retorna notificações de um usuário", Description = "Obtém todas as notificações associadas a um usuário específico.")]
        [SwaggerResponse(200, "Lista de notificações retornada com sucesso.")]
        [SwaggerResponse(404, "Usuário ou notificações não encontradas.")]
        [SwaggerResponse(500, "Erro interno no servidor.")]
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
