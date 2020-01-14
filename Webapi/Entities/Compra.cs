using System;
using System.Collections.Generic;
using System.Linq;

namespace Webapi.Entities
{
    public class Compra : Entity
    {
        public decimal ValorTotal { get; set; }
        public decimal Juros {get; set; } 
        public DateTime Data { get; set; }
        public int QuantidadeParcelas {get; set; }
        public List<Parcela> Parcelas { get; set; }
        
        public decimal CalcularMontante()
        {
            if(this.Parcelas != null && this.Parcelas.Any())
            {
                var valor = this.Parcelas.Sum(p => p.Valor);
                return Math.Round(valor, 2);
            }
            return 0;
        }
    }
}