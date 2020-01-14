using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Webapi.Entities;
using Webapi.Infraestructure.Config;

namespace Test.src
{
    public class MassaDadosBuilder
    {
        public SimuladorContext Context { get; set; }
        public Compra Compra {get; set;}
        public List<Compra> Compras {get; set; }
        public List<Parcela> Parcelas {get; set; }

        public void Inicializar()
        {
            this.Compras = new List<Compra>();
            this.Parcelas = new List<Parcela>();
            this.Context = new SimuladorContext(new DbContextOptions<SimuladorContext>());
        }
        public void Destruir()
        {
            if(this.Compra != null)
            {
                this.Context.Set<Compra>().Remove(this.Compra);
            }
           this.Context.Set<Compra>().RemoveRange(this.Compras);
           this.Context.SaveChanges();
        } 

        public MassaDadosBuilder MontarCompra(int quantidadeDeParcelas, decimal valorTotal)
        {
            this.Compra = new Compra()
            {
                Data = DateTime.Now,
               QuantidadeParcelas = quantidadeDeParcelas,
               Juros = 0.05m,
               ValorTotal = valorTotal
            };

            return this;
        }  
        
        public MassaDadosBuilder MontarESalvarComprasComParcelas(int quantidadeDeCompras, int quantidadeDeParcelas)
        {
            for(int i = 0; i < quantidadeDeCompras; i++)
            {
                var compra = new Compra()
                {
                    Data = DateTime.Now,
                    QuantidadeParcelas = quantidadeDeParcelas,
                    Juros = 0.05m,
                    ValorTotal = 1000
                };

                 var lista = new List<Parcela>();

                for(int j = 0; j < quantidadeDeParcelas; j++)
                {   
                    lista.Add(new Parcela(){ Juros = 0.05m, Valor = 367.21m,  Vencimento = DateTime.Now.AddMonths(i + 1)});
                }

                compra.Parcelas = lista;

                this.Context.Set<Compra>().Add(compra);

                this.Compras.Add(compra);
            }
            this.Context.SaveChanges();

            this.Compras.ForEach(c => {
                this.Context.Entry(c).State = EntityState.Detached;
            }); 

            return this;
        }

        public MassaDadosBuilder MontarParcelas(int quantidadeDeParcelas)
        {
            var lista = new List<Parcela>();

            for(int i = 0; i < quantidadeDeParcelas; i++)
            {
               lista.Add(new Parcela(){ Juros = 0.05m, Valor = 367.21m,  Vencimento = DateTime.Now.AddMonths(i + 1)});
            }

            this.Parcelas = lista;

            this.Compra.Parcelas = lista;

            return this;
        }  
        
    }
}