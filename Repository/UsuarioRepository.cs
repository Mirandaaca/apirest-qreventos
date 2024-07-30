using Microsoft.EntityFrameworkCore;
using QR___Evento.Context;
using QR___Evento.DTOs.Entities.Usuario;
using QR___Evento.Entities;
using QR___Evento.Interfaces.Repository;
using QR___Evento.Services;

namespace QR___Evento.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly QRContext _context;
        public UsuarioRepository(QRContext context)
        {
            _context = context;
        }

        public async Task<bool> Crear(CreateUsuarioDTO usuario)
        {
            bool usuarioExistente = await _context.Usuarios.AnyAsync(x => x.User == usuario.username);
            if (usuarioExistente)
            {
                return false;
            }
            Usuario objUsuario = new Usuario()
            {
                User = usuario.username,
                Password = PasswordHelperService.HashPassword(usuario.password)
            };
            _context.Usuarios.Add(objUsuario);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task Editar(UpdateUsuarioDTO usuario)
        {
            Usuario actualizaUsuario = await _context.Usuarios.FirstOrDefaultAsync(aux => aux.Id.Equals(usuario.Id));

            if (actualizaUsuario == null)
                throw new Exception("El usuario no existe");

            actualizaUsuario.User = usuario.username;
            actualizaUsuario.Password = PasswordHelperService.HashPassword(usuario.password);

            _context.Usuarios.Update(actualizaUsuario);

            await _context.SaveChangesAsync();
        }

        public async Task Eliminar(int id)
        {
            Usuario usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
            if (usuario == null)
            {
                throw new Exception("El usuario no existe");
            }
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ReadUsuarioDTO>> ObtenerListaDeUsuarios()
        {
            List<ReadUsuarioDTO> listaUsuariosDTO = new List<ReadUsuarioDTO>();
            List<Usuario> listaUsuarios = await _context.Usuarios.ToListAsync();
            foreach(Usuario usuario in listaUsuarios)
            {
                listaUsuariosDTO.Add(new ReadUsuarioDTO
                {
                    Id = usuario.Id,
                    username = usuario.User,
                    password = usuario.Password
                });
            }
            return listaUsuariosDTO;
        }

        public async Task<ReadUsuarioDTO> ObtenerUsuarioPorId(int id)
        {
            Usuario usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
            if (usuario == null)
            {
                throw new Exception("El usuario no existe");
            }
            ReadUsuarioDTO readUsuarioDTO = new ReadUsuarioDTO
            {
                Id = usuario.Id,
                username = usuario.User,
                password = usuario.Password
            };
            return readUsuarioDTO;
        }
    }
}
