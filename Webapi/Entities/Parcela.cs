using System;
using Newtonsoft.Json;

namespace Webapi.Entities
{
    public class Parcela : Entity
    {
        [JsonIgnore]
        public Compra Compra { get; set; }
        public int CompraId { get; set;}
        public DateTime Vencimento { get; set; }
        public decimal Juros { get; set; }
        public decimal Valor { get; set; }
        
    }
}