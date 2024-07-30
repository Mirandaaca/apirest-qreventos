using Microsoft.EntityFrameworkCore;
using QR___Evento.Context;
using QR___Evento.DTOs;
using QR___Evento.DTOs.Entities.Persona;
using QR___Evento.DTOs.Entities.QR;
using QR___Evento.Entities;
using QR___Evento.Interfaces.Repository;
using QR___Evento.Interfaces.Services;

namespace QR___Evento.Repository
{
    public class PersonaRepository : IPersonaRepository
    {
        private readonly IConfiguration _configuration;
        private readonly QRContext _context;
        private readonly IQRGenerator _qrgnerator;
        private readonly IEmailSenderService _emailSenderService;
        private readonly IFrom64BaseStringToImage _from64BaseStringToImage;
        public PersonaRepository(QRContext context, IQRGenerator qrgnerator, IEmailSenderService emailSenderService, IFrom64BaseStringToImage from64BaseStringToImage, IConfiguration configuration)
        {
            _context = context;
            _qrgnerator = qrgnerator;
            _emailSenderService = emailSenderService;
            _from64BaseStringToImage = from64BaseStringToImage;
            _configuration = configuration;
        }
        public async Task Actualizar(UpdatePersonaDTO persona)
        {
            Persona objPersona = await _context.Personas.FirstOrDefaultAsync(x => x.IdPersona == persona.Id);
            if (objPersona == null)
            {
                throw new Exception("La persona no existe");
            }
            objPersona.Nombres = persona.Nombres;
            objPersona.Apellidos = persona.Apellidos;
            objPersona.Correo = persona.Correo;
            objPersona.Celular = persona.Celular;
            objPersona.haIngresado = persona.haIngresado;
            objPersona.haPagado = persona.haPagado;
            objPersona.haComido = persona.haComido;
            _context.Personas.Update(objPersona);
            await _context.SaveChangesAsync();
        }

        public async Task Eliminar(int id)
        {
            Persona objPersona = await _context.Personas.FirstOrDefaultAsync(x => x.IdPersona == id);
            if (objPersona == null)
            {
                throw new Exception("La persona no existe");
            }
            _context.Personas.Remove(objPersona);
            await _context.SaveChangesAsync();
        }

        public async Task Guardar(CreatePersonaDTO persona)
        {
            Persona objPersona = new Persona()
            {
                Nombres = persona.Nombres,
                Apellidos = persona.Apellidos,
                Correo = persona.Correo,
                Celular = persona.Celular,
                haIngresado = persona.haIngresado,
                haPagado = persona.haPagado,
                haComido = persona.haComido
            };
            _context.Personas.Add(objPersona);
            await _context.SaveChangesAsync();
            var url = _configuration.GetSection("QRSettings");
            string urlBase = url["URL"];
            //Generar QR
            QR objQR = new QR()
            {
                IdPersona = objPersona.IdPersona,
                InformacionCodigoQR = _qrgnerator.GenerarQR($"{urlBase}{objPersona.IdPersona}")
            };
            _context.QRs.Add(objQR);
            await _context.SaveChangesAsync();

            await _emailSenderService.SendEmailAsync(persona.Correo, "Codigo QR", objQR.InformacionCodigoQR);

        }
        public async Task ReenviarQR(int id)
        {
            Persona objPersona = await _context.Personas.FirstOrDefaultAsync(x => x.IdPersona == id);
            string qr = await _context.QRs.Where(x => x.IdPersona == objPersona.IdPersona).Select(x => x.InformacionCodigoQR).FirstOrDefaultAsync();
            
            if(objPersona == null)
            {
                throw new Exception("No existe esa persona o ese email");
            }
            await _emailSenderService.SendEmailAsync(objPersona.Correo, "Reenvío de Codigo QR", qr);
        }
        public async Task<List<ReadPersonaDTO>> ObtenerListaDePersonas()
        {
            List<ReadPersonaDTO> listPersonasDTO = new List<ReadPersonaDTO>();
            List<Persona> listPersonas = await _context.Personas.ToListAsync();
            foreach (Persona persona in listPersonas)
            {
                ReadPersonaDTO objPersonaDTO = new ReadPersonaDTO()
                {
                    Id = persona.IdPersona,
                    Nombres = persona.Nombres,
                    Apellidos = persona.Apellidos,
                    Correo = persona.Correo,
                    Celular = persona.Celular,
                    haIngresado = persona.haIngresado,
                    haPagado = persona.haPagado,
                    haComido = persona.haComido,
                };
                listPersonasDTO.Add(objPersonaDTO);
            }
            return listPersonasDTO;
        }

        public async Task<List<ReadPersonaYSuQRDTO>> ObtenerListaDePersonasYSuQR()
        {
            List<ReadPersonaYSuQRDTO> listPersonasDTO = new List<ReadPersonaYSuQRDTO>();
            List<Persona> listPersonas = await _context.Personas.ToListAsync();
            foreach(Persona persona in listPersonas)
            {
                ReadPersonaYSuQRDTO objPersonaDTO = new ReadPersonaYSuQRDTO()
                {
                    Id = persona.IdPersona,
                    Nombres = persona.Nombres,
                    Apellidos = persona.Apellidos,
                    Correo = persona.Correo,
                    Celular = persona.Celular,
                    haIngresado = persona.haIngresado,
                    haPagado = persona.haPagado,
                    haComido = persona.haComido,
                    QR = await _context.Personas.Where(x => x.IdPersona == persona.IdPersona).Select(x => x.QR.InformacionCodigoQR).FirstOrDefaultAsync()
                };
                listPersonasDTO.Add(objPersonaDTO);
            }
            return listPersonasDTO;
        }
        public async Task<ReadPersonaDTO> ObtenerPersonaPorId(int id)
        {
            Persona objPersona = await _context.Personas.FirstOrDefaultAsync(x => x.IdPersona == id);
            if (objPersona == null)
            {
                throw new Exception("La persona no existe");
            }
            ReadPersonaDTO objPersonaDTO = new ReadPersonaDTO()
            {
                Id = objPersona.IdPersona,
                Nombres = objPersona.Nombres,
                Apellidos = objPersona.Apellidos,
                Correo = objPersona.Correo,
                Celular = objPersona.Celular,
                haIngresado = objPersona.haIngresado,
                haPagado = objPersona.haPagado,
                haComido = objPersona.haComido,
            };
            return objPersonaDTO;
        }
        public async Task<VerQRDTO> VerQR(int id)
        {
            Persona objPersona = await _context.Personas.FirstOrDefaultAsync(x => x.IdPersona == id);
            string qr = await _context.QRs.Where(x => x.IdPersona == objPersona.IdPersona).Select(x => x.InformacionCodigoQR).FirstOrDefaultAsync();
            VerQRDTO objQR = new VerQRDTO()
            {
                Base64String = qr
            };
            return objQR;
        }
        public async Task<ReadPersonaYSuQRDTO> ObtenerPersonaPorIdYSuQR(int id)
        {
            Persona objPersona = await _context.Personas.FirstOrDefaultAsync(x => x.IdPersona == id);
            if(objPersona == null)
            {
                throw new Exception("La persona no existe");
            }
            ReadPersonaYSuQRDTO objPersonaDTO = new ReadPersonaYSuQRDTO()
            {
                Id = objPersona.IdPersona,
                Nombres = objPersona.Nombres,
                Apellidos = objPersona.Apellidos,
                Correo = objPersona.Correo,
                Celular = objPersona.Celular,
                haIngresado = objPersona.haIngresado,
                haPagado = objPersona.haPagado,
                haComido = objPersona.haComido,
                QR = await _context.Personas.Where(x => x.IdPersona == objPersona.IdPersona).Select(x => x.QR.InformacionCodigoQR).FirstOrDefaultAsync()
            };
            return objPersonaDTO;
        }

    }
}
