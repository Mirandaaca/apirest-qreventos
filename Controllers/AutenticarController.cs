using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QR___Evento.DTOs.Services;
using QR___Evento.Interfaces.Services;

namespace QR___Evento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticarController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AutenticarController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost("IniciarSesion")]
        public async Task<AuthenticationResponseDTO> IniciarSesion (AuthenticationRequestDTO request)
        {
            try
            {
                return await _authenticationService.Autenticar(request);
            }
            catch
            {
                return null;
            }
        }
    }
}
