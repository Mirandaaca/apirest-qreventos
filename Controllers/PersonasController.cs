using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QR___Evento.DTOs;
using QR___Evento.DTOs.Entities.Persona;
using QR___Evento.DTOs.Entities.QR;
using QR___Evento.Interfaces.Repository;

namespace QR___Evento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly IPersonaRepository _personaRepository;
        public PersonasController(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }
        [HttpPost("GuardarPersonaYEnviarQR")]
        public async Task GuardarPersona (CreatePersonaDTO persona)
        {
            await _personaRepository.Guardar(persona);
        }
        [HttpDelete("EliminarPersona")]
        public async Task EliminarPersona (int id)
        {
            await _personaRepository.Eliminar(id);
        }
        [HttpPatch("ActualizarPersona")]
        public async Task ActualizarPersona (UpdatePersonaDTO persona)
        {
            await _personaRepository.Actualizar(persona);
        }
        [HttpGet("ObtenerListaDePersonas")]
        public async Task<List<ReadPersonaDTO>> ObtenerListaDePersonas()
        {
            return await _personaRepository.ObtenerListaDePersonas();
        }
        [HttpGet("ObtenerListaDePersonasYSuQR")]
        public async Task<List<ReadPersonaYSuQRDTO>> ObtenerListaDePersonasYSuQR()
        {
            return await _personaRepository.ObtenerListaDePersonasYSuQR();
        }
        [HttpPost("ReenviarQR")]
        public async Task ReenviarQR(int id)
        {
            await _personaRepository.ReenviarQR(id);
        }
        [HttpGet("VerQR")]
        public async Task<VerQRDTO> VerQR(int id)
        {
            return await _personaRepository.VerQR(id);
        }
        [HttpGet("ObtenerPersonaPorIdYSuQR")]
        public async Task<ReadPersonaYSuQRDTO> ObtenerPersonaPorIdYSuQR(int id)
        {
            return await _personaRepository.ObtenerPersonaPorIdYSuQR(id);
        }
        [HttpGet("ObtenerPersonaPorId")]
        public async Task<ReadPersonaDTO> ObtenerPersonaPorId(int id)
        {
            return await _personaRepository.ObtenerPersonaPorId(id);
        }
    }
}
