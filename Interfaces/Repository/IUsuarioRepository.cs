using QR___Evento.DTOs.Entities.Usuario;

namespace QR___Evento.Interfaces.Repository
{
    public interface IUsuarioRepository
    {
        public Task<bool> Crear (CreateUsuarioDTO usuario);
        public Task Editar (UpdateUsuarioDTO usuario);
        public Task Eliminar (int id);
        public Task<ReadUsuarioDTO> ObtenerUsuarioPorId (int id);
        public Task<List<ReadUsuarioDTO>> ObtenerListaDeUsuarios ();
    }
}
