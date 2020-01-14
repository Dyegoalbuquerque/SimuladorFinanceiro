using TechTalk.SpecFlow;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using Webapi.Controllers;
using Webapi.Domain.Services.Concrete;
using Webapi.Infraestructure.Repositorys.Concrete;
using Webapi.Entities;

namespace Test.src.steps
{
    [Binding]
    public class SalvarSimulacaoSteps
    {
        private MassaDadosBuilder MassaBuilder { get; set; }
        private IActionResult Result { get; set; }
        private void Inicializar()
        {
            this.MassaBuilder = new MassaDadosBuilder();
            this.MassaBuilder.Inicializar();
        }

        private void Destruir()
        {
            this.MassaBuilder.Destruir();
        }

        [Given(@"uma simulação realizada pelo comprador")]
        public void GivenUmaSimulacaoRealizadaPeloComprador()
        {
            Inicializar();

            this.MassaBuilder.MontarCompra(5, 1000)
                             .MontarParcelas(5);                                             
         }

        [When(@"sistema salvar a simulação de compra")]
        public  void WhenSistemaSalvarAsimulacaoDeCompra()
        {
            var service = new CompraService(new CompraRepository(this.MassaBuilder.Context));
            
            var controller = new CompraController(service);
            var result =  controller.Post(this.MassaBuilder.Compra).ConfigureAwait(false); 

            var createdResponse = result.GetAwaiter().GetResult() as CreatedResult;

            this.MassaBuilder.Compra = createdResponse.Value as Compra;
        } 

        [Then(@"o sistema apresenta uma simulação de compra persistida")]
        public void ThenOsistemaApresentaUmaSimulacaoDeCompraPersistida()
        {
            Assert.IsTrue(this.MassaBuilder.Compra.Id > 0);
            Assert.IsTrue(this.MassaBuilder.Compra.Parcelas.Count > 0);

            Destruir();
        }
    }
}