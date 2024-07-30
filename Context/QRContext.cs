using Microsoft.EntityFrameworkCore;
using QR___Evento.Entities;

namespace QR___Evento.Context
{
    public class QRContext : DbContext
    {
        public QRContext(DbContextOptions<QRContext> options) : base(options)
        {
        }

        public virtual DbSet<Persona> Personas { get; set; }
        public virtual DbSet<QR> QRs { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        // Este método se encarga de establecer la relación uno a uno entre la entidad QR y la entidad Persona a través de la propiedad IdPersona de la entidad QR y la propiedad IdPersona de la entidad Persona. 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Persona>()
                .HasOne(p => p.QR)
                .WithOne(q => q.Persona)
                .HasForeignKey<QR>(q => q.IdPersona);
            base.OnModelCreating(modelBuilder);
        }
    }
}
