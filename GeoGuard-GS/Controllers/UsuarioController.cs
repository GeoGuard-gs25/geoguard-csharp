using GeoGuard_GS.Model;
using Microsoft.AspNetCore.Mvc;
using GeoGuard_GS.Exceptions;
using GeoGuard_GS.Services.Abstractions;
using GeoGuard_GS.Model.DTO;
using Swashbuckle.AspNetCore.Annotations; // IMPORTANTE: adicionar este using

namespace GeoGuard_GS.Controllers
{
    /// <summary>
    /// Controlador responsável pelas operações relacionadas aos usuários.
    /// </summary>
    [ApiController]
    [Route("usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        /// <summary>
        /// Construtor do controlador de usuários.
        /// </summary>
        /// <param name="usuarioService">Serviço responsável pela lógica de usuários.</param>
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Retorna todos os usuários cadastrados.
        /// </summary>
        /// <returns>Lista de usuários.</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Retorna todos os usuários", Description = "Obtém uma lista completa de usuários cadastrados no sistema.")]
        [SwaggerResponse(200, "Lista de usuários retornada com sucesso.")]
        [SwaggerResponse(400, "Erro de validação ou de negócio.")]
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
        /// Retorna um usuário pelo seu ID.
        /// </summary>
        /// <param name="id">ID do usuário.</param>
        /// <returns>Usuário correspondente ao ID informado.</returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Retorna um usuário pelo ID", Description = "Obtém um usuário específico com base no ID informado.")]
        [SwaggerResponse(200, "Usuário encontrado com sucesso.")]
        [SwaggerResponse(404, "Usuário não encontrado.")]
        [SwaggerResponse(400, "Erro de requisição.")]
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
        /// Retorna um usuário pelo e-mail.
        /// </summary>
        /// <param name="email">E-mail do usuário.</param>
        /// <returns>Usuário correspondente ao e-mail informado.</returns>
        [HttpGet("buscar")]
        [SwaggerOperation(Summary = "Retorna um usuário pelo e-mail", Description = "Obtém um usuário específico com base no e-mail informado.")]
        [SwaggerResponse(200, "Usuário encontrado com sucesso.")]
        [SwaggerResponse(404, "Usuário não encontrado.")]
        [SwaggerResponse(400, "Erro de requisição.")]
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
        /// Cria um novo usuário.
        /// </summary>
        /// <param name="usuario">DTO com os dados para criação do usuário.</param>
        /// <returns>Usuário criado com status 201.</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Cria um novo usuário", Description = "Cria um novo usuário com base nos dados fornecidos.")]
        [SwaggerResponse(201, "Usuário criado com sucesso.")]
        [SwaggerResponse(400, "Erro de validação ou de negócio.")]
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
        /// Atualiza os dados de um usuário.
        /// </summary>
        /// <param name="id">ID do usuário a ser atualizado.</param>
        /// <param name="usuarioDto">DTO com os dados atualizados.</param>
        /// <returns>Status 204 em caso de sucesso.</returns>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualiza um usuário", Description = "Atualiza os dados de um usuário com base no ID informado.")]
        [SwaggerResponse(204, "Usuário atualizado com sucesso.")]
        [SwaggerResponse(400, "Erro de validação ou ID inválido.")]
        [SwaggerResponse(500, "Erro interno no servidor.")]
        public async Task<IActionResult> Update(int id, UsuarioUpdateDto usuarioDto)
        {
            try
            {
                if (id != usuarioDto.Id)
                    return BadRequest("ID do usuário inválido.");

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
        /// Remove um usuário pelo ID.
        /// </summary>
        /// <param name="id">ID do usuário a ser deletado.</param>
        /// <returns>Status 204 em caso de sucesso.</returns>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Remove um usuário", Description = "Remove um usuário do sistema com base no ID informado.")]
        [SwaggerResponse(204, "Usuário removido com sucesso.")]
        [SwaggerResponse(404, "Usuário não encontrado.")]
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
