using Microsoft.EntityFrameworkCore;
using QR___Evento.Context;
using QR___Evento.DTOs.Services;
using QR___Evento.Entities;
using QR___Evento.Interfaces.Services;

namespace QR___Evento.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly QRContext _context;
        public AuthenticationService(QRContext context)
        {
            _context = context;
        }
        public async Task<AuthenticationResponseDTO> Autenticar(AuthenticationRequestDTO request)
        {
            Usuario usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.User == request.username);
            if (usuario == null || !PasswordHelperService.VerifyPassword(usuario.Password, request.password))
            {
                return null;
            }
           AuthenticationResponseDTO response = new AuthenticationResponseDTO
           {
                Id = usuario.Id,
                username = usuario.User,
                password = usuario.Password
            };
            return response;
        }
    }
}
