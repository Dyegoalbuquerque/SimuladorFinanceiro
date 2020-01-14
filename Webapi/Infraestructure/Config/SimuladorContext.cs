using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
                // var configuration = new ConfigurationBuilder()
                //                         .SetBasePath(Directory.GetCurrentDirectory())
                //                         .AddJsonFile($"{Caminho}appsettings.json", optional: true, reloadOnChange: true)
                //                         .Build();
                
                //optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                optionsBuilder.UseSqlServer("Data Source=localhost,1433; Initial Catalog=SimuladorDB; User Id=sa; Password=Testing123;");
            }
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