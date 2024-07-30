using System.ComponentModel.DataAnnotations;

namespace QR___Evento.Entities
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string User { get; set; }
        [Required]
        public string Password{ get; set; }
    }
}
