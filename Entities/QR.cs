using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QR___Evento.Entities
{
    public class QR
    {
        [Key]
        [ForeignKey("IdPersona")]
        public int IdPersona { get; set; }
        public string InformacionCodigoQR { get; set; }
        public Persona Persona { get; set; }
    }
}
