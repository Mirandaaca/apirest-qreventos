namespace QR___Evento.DTOs
{
    public class UpdatePersonaDTO
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
