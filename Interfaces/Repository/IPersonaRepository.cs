using QR___Evento.DTOs;
using QR___Evento.DTOs.Entities.Persona;
using QR___Evento.DTOs.Entities.QR;

namespace QR___Evento.Interfaces.Repository
{
    public interface IPersonaRepository
    {
        public Task Guardar(CreatePersonaDTO persona);
        public Task Actualizar(UpdatePersonaDTO persona);
        public Task Eliminar(int id);
        public Task ReenviarQR(int id);
        public Task<VerQRDTO> VerQR(int id);
        public Task<ReadPersonaYSuQRDTO> ObtenerPersonaPorIdYSuQR(int id);
        public Task<ReadPersonaDTO> ObtenerPersonaPorId(int id);
        public Task<List<ReadPersonaDTO>> ObtenerListaDePersonas();
        public Task<List<ReadPersonaYSuQRDTO>> ObtenerListaDePersonasYSuQR();
    }
}
