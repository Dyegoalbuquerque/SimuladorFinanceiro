using Microsoft.EntityFrameworkCore;
using Webapi.Entities;

namespace Webapi.Infraestructure.Config
{
    public class SimuladorContext: DbContext
    {
        public SimuladorContext(DbContextOptions<SimuladorContext> options) : base(options)
        { 
             this.Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Parcela>()
                    .HasOne(i => i.Compra)
                    .WithMany(c => c.Parcelas)
                    .HasForeignKey(c => c.CompraId)
                    .OnDelete(DeleteBehavior.Cascade);
 
        }

        public DbSet<Compra> Compras { get; set; }
        public DbSet<Parcela> Parcelas { get; set; }
    
    }
}