namespace QR___Evento.DTOs.Entities.Persona
{
    public class ReadPersonaDTO
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Celular { get; set; }
        public bool haIngresado { get; set; }
        public bool haPagado { get; set; }
        public bool haComido { get; set; }
    }
}
