using MediatR;
using MensajesExternos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Negocio.Interfaces;

namespace VulApp.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IUsuarioRepo _usuarioRepo;
        public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepo usuarioRepo)
        {
            _logger = logger;
            _usuarioRepo = usuarioRepo;
        }

        [Authorize]
        [HttpGet(Name = "DameTodosUsuarios")]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
        public async Task<IActionResult> DameTodosUsuarios()
        {
            var result = await _usuarioRepo.DameTodosUsuarios();
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id}",Name = "DameUsuarioPorId")]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
        public async Task<IActionResult> DameUsuarioPorId(int id)
        {
            var result = await _usuarioRepo.DameUsuarioPorId(id);
            return Ok(result);
        }

        [Authorize]
        [HttpPut(Name = "ActualizaUsuario")]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
        public async Task<IActionResult> ActualizaUsuario(ActualizaUsuarioEntrada entrada)
        {
            var result = await _usuarioRepo.Actualizausuario(entrada);
            return Ok(result);
        }



        [HttpPost(Name = "Login")]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(LoginRequest loginrequest)
        {
            var result = await _usuarioRepo.Login(loginrequest.Email, loginrequest.Password);
            return Ok(result);
        }


    }
}
