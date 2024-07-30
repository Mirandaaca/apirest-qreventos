using Microsoft.Identity.Client;
using QR___Evento.DTOs.Services;

namespace QR___Evento.Interfaces.Services
{
    public interface IAuthenticationService
    {
        public Task<AuthenticationResponseDTO> Autenticar(AuthenticationRequestDTO request);
    }
}
