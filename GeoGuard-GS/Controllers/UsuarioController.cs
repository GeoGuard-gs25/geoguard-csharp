using GeoGuard_GS.Model;
using Microsoft.AspNetCore.Mvc;
using GeoGuard_GS.Exceptions;
using GeoGuard_GS.Services.Abstractions;
using GeoGuard_GS.Model.DTO;
using Swashbuckle.AspNetCore.Annotations; // IMPORTANTE: adicionar este using

namespace GeoGuard_GS.Controllers
{
    /// <summary>
    /// Controlador respons�vel pelas opera��es relacionadas aos usu�rios.
    /// </summary>
    [ApiController]
    [Route("usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        /// <summary>
        /// Construtor do controlador de usu�rios.
        /// </summary>
        /// <param name="usuarioService">Servi�o respons�vel pela l�gica de usu�rios.</param>
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Retorna todos os usu�rios cadastrados.
        /// </summary>
        /// <returns>Lista de usu�rios.</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Retorna todos os usu�rios", Description = "Obt�m uma lista completa de usu�rios cadastrados no sistema.")]
        [SwaggerResponse(200, "Lista de usu�rios retornada com sucesso.")]
        [SwaggerResponse(400, "Erro de valida��o ou de neg�cio.")]
        [SwaggerResponse(500, "Erro interno no servidor.")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetAll()
        {
            try
            {
                var usuarios = await _usuarioService.GetAllAsync();
                return Ok(usuarios);
            }
            catch (UsuarioException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, "Erro interno: " + ex.Message);
            }
        }

        /// <summary>
        /// Retorna um usu�rio pelo seu ID.
        /// </summary>
        /// <param name="id">ID do usu�rio.</param>
        /// <returns>Usu�rio correspondente ao ID informado.</returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Retorna um usu�rio pelo ID", Description = "Obt�m um usu�rio espec�fico com base no ID informado.")]
        [SwaggerResponse(200, "Usu�rio encontrado com sucesso.")]
        [SwaggerResponse(404, "Usu�rio n�o encontrado.")]
        [SwaggerResponse(400, "Erro de requisi��o.")]
        public async Task<ActionResult<Usuario>> GetById(int id)
        {
            try
            {
                var usuario = await _usuarioService.GetById(id);
                return Ok(usuario);
            }
            catch (UsuarioException ex)
            {
                return NotFound(ex.Message);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retorna um usu�rio pelo e-mail.
        /// </summary>
        /// <param name="email">E-mail do usu�rio.</param>
        /// <returns>Usu�rio correspondente ao e-mail informado.</returns>
        [HttpGet("buscar")]
        [SwaggerOperation(Summary = "Retorna um usu�rio pelo e-mail", Description = "Obt�m um usu�rio espec�fico com base no e-mail informado.")]
        [SwaggerResponse(200, "Usu�rio encontrado com sucesso.")]
        [SwaggerResponse(404, "Usu�rio n�o encontrado.")]
        [SwaggerResponse(400, "Erro de requisi��o.")]
        public async Task<ActionResult<Usuario>> GetByEmail([FromQuery] string email)
        {
            try
            {
                var usuario = await _usuarioService.GetByEmail(email);
                return Ok(usuario);
            }
            catch (UsuarioException ex)
            {
                return NotFound(ex.Message);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Cria um novo usu�rio.
        /// </summary>
        /// <param name="usuario">DTO com os dados para cria��o do usu�rio.</param>
        /// <returns>Usu�rio criado com status 201.</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Cria um novo usu�rio", Description = "Cria um novo usu�rio com base nos dados fornecidos.")]
        [SwaggerResponse(201, "Usu�rio criado com sucesso.")]
        [SwaggerResponse(400, "Erro de valida��o ou de neg�cio.")]
        public async Task<ActionResult<Usuario>> Create(UsuarioCreateDto usuario)
        {
            try
            {
                var usuarioCriado = await _usuarioService.CriarAsync(usuario);
                return CreatedAtAction(nameof(GetById), new { id = usuarioCriado.Id }, usuarioCriado);
            }
            catch (UsuarioException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza os dados de um usu�rio.
        /// </summary>
        /// <param name="id">ID do usu�rio a ser atualizado.</param>
        /// <param name="usuarioDto">DTO com os dados atualizados.</param>
        /// <returns>Status 204 em caso de sucesso.</returns>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualiza um usu�rio", Description = "Atualiza os dados de um usu�rio com base no ID informado.")]
        [SwaggerResponse(204, "Usu�rio atualizado com sucesso.")]
        [SwaggerResponse(400, "Erro de valida��o ou ID inv�lido.")]
        [SwaggerResponse(500, "Erro interno no servidor.")]
        public async Task<IActionResult> Update(int id, UsuarioUpdateDto usuarioDto)
        {
            try
            {
                if (id != usuarioDto.Id)
                    return BadRequest("ID do usu�rio inv�lido.");

                await _usuarioService.AtualizarAsync(id, usuarioDto);
                return NoContent();
            }
            catch (UsuarioException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, "Erro interno: " + ex.Message);
            }
        }

        /// <summary>
        /// Remove um usu�rio pelo ID.
        /// </summary>
        /// <param name="id">ID do usu�rio a ser deletado.</param>
        /// <returns>Status 204 em caso de sucesso.</returns>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Remove um usu�rio", Description = "Remove um usu�rio do sistema com base no ID informado.")]
        [SwaggerResponse(204, "Usu�rio removido com sucesso.")]
        [SwaggerResponse(404, "Usu�rio n�o encontrado.")]
        [SwaggerResponse(500, "Erro interno no servidor.")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _usuarioService.DeletarAsync(id);
                return NoContent();
            }
            catch (UsuarioException ex)
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
