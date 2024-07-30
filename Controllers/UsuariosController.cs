using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QR___Evento.DTOs.Entities.Usuario;
using QR___Evento.Interfaces.Repository;

namespace QR___Evento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuariosController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        [HttpPost("CrearUsuario")]
        public async Task<string> CrearUsuario(CreateUsuarioDTO usuario)
        {
            bool resultado = await _usuarioRepository.Crear(usuario);
            if(resultado)
            {
              return "Usuario creado";
            }
            else
            {
              return "Usuario ya existe";
            }
        }
        [HttpDelete("EliminarUsuario")]
        public async Task<string> EliminarUsuario(int id)
        {
            await _usuarioRepository.Eliminar(id);
            return "Usuario eliminado";
        }
        [HttpPatch("EditarUsuario")]
        public async Task<string> EditarUsuario(UpdateUsuarioDTO usuario)
        {
            await _usuarioRepository.Editar(usuario);
            return "Usuario editado";
        }
        [HttpGet("ObtenerUsuarioPorId")]
        public async Task<ReadUsuarioDTO> ObtenerUsuarioPorId(int id)
        {
            return await _usuarioRepository.ObtenerUsuarioPorId(id);
        }
        [HttpGet("ObtenerListaDeUsuarios")]
        public async Task<List<ReadUsuarioDTO>> ObtenerListaDeUsuarios()
        {
            return await _usuarioRepository.ObtenerListaDeUsuarios();
        }
    }
}
