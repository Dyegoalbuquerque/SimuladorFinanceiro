using System;
using System.Collections.Generic;

namespace Webapi.Entities
{
    public class Compra : Entity
    {
        public decimal ValorTotal { get; set; }
        public decimal Juros {get; set; } 
        public DateTime Data { get; set; }
        public int QuantidadeParcelas {get; set; }
        public List<Parcela> Parcelas { get; set; }
        
    }
}