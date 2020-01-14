using TechTalk.SpecFlow;
using System;
using NUnit.Framework;
using Test.src;
using Microsoft.AspNetCore.Mvc;
using Webapi.Controllers;
using Webapi.Entities;
using Webapi.Domain.Services.Concrete;
using Webapi.Infraestructure.Repositorys.Concrete;
using System.Collections.Generic;
using System.Linq;

namespace Webapi.Test.src.steps
{
    [Binding]
    public class VisualizarSimulacaoSteps
    {
        private MassaDadosBuilder MassaBuilder { get; set; }
        private void Inicializar()
        {
            this.MassaBuilder = new MassaDadosBuilder();
            this.MassaBuilder.Inicializar();
         }

        private void Destruir()
        {
            this.MassaBuilder.Destruir();
        }

        
        [Given(@"simulações persistidas anteriormente")]
        public void GivenSimulacoesPersistidasAnteriormente()
        {
            Inicializar();

            this.MassaBuilder.MontarESalvarComprasComParcelas(3, 3);                                                       
         }

        [When(@"sistema visualizar as simulações de compra")]
        public  void WhenSistemaVisualizarAsSimulacoesDeCompra()
        {
            var service = new CompraService(new CompraRepository(this.MassaBuilder.Context));
            
            var controller = new CompraController(service);
            var result =  controller.Get().ConfigureAwait(false); 

            var okResponse = result.GetAwaiter().GetResult() as OkObjectResult;

            var compras = okResponse.Value as List<Compra>;

            var comprasIds = this.MassaBuilder.Compras.Select(c => c.Id).ToList();

            this.MassaBuilder.Compras = compras.Where(c => comprasIds.Contains(c.Id)).ToList();
        } 

        [Then(@"o sistema apresenta simulações de compras persistidas")]
        public void ThenOsistemaApresentaSimulacoesDeComprasPersistidas()
        {
            Assert.IsNotNull(this.MassaBuilder.Compras);
        } 

        [Then(@"o sistema apresenta detalhes de simulações como quantidade de parcelas, valor total, valor do juros e data da compra")]
        public void ThenOsistemaApresentadetalhesDeSimulacoesComoQuantidadeDeParcelasValorTotalValorDoJurosEDataDaCompra()
        {
            this.MassaBuilder.Compras.ForEach(c => {
                Assert.IsTrue(c.Id > 0);
                Assert.IsTrue(c.QuantidadeParcelas > 0);
                Assert.IsTrue(c.ValorTotal > 0);
                Assert.IsTrue(c.Juros > 0);
                Assert.IsTrue(c.Data.Year > 1970);
            });

            Destruir();
        }   
    }
}