using GeoGuard_GS.Model;
using Microsoft.AspNetCore.Mvc;
using GeoGuard_GS.Exceptions;
using GeoGuard_GS.Services.Abstractions;
using GeoGuard_GS.Model.DTO;

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
