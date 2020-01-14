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

         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=den1.mssql7.gear.host; Initial Catalog=simuladordb; User Id=simuladordb; Password=Ja0k9ad-35~7;");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Compra>(eb =>
            {
                eb.Property(b => b.Juros).HasColumnType("decimal(5, 4)");
            });

            builder.Entity<Parcela>(eb =>
            {
                eb.Property(b => b.Juros).HasColumnType("decimal(5, 4)");
            });

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