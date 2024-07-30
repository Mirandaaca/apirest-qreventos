using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QR___Evento.Entities
{
    public class Persona
    {
        [Key]
        public int IdPersona { get; set; }
        [Required]
        [MaxLength(150)]
        public string Nombres { get; set; }
        [MaxLength(150)]
        public string Apellidos { get; set; }
        [Required]
        public string Correo { get; set; }
        public string Celular { get; set; }
        [DefaultValue(false)]
        public bool haIngresado { get; set;}
        public DateTime FechaIngreso { get; set; }
        [DefaultValue(false)]
        public bool haPagado { get; set;}
        public DateTime FechaPago { get; set; }
        [DefaultValue(false)]
        public bool haComido { get; set;}
        public DateTime FechaComida { get; set; }
        public QR QR { get; set; }
    }
}
