using TechTalk.SpecFlow;
using Microsoft.AspNetCore.Mvc;
using Webapi.Domain.Services.Concrete;
using Webapi.Controllers;
using Webapi.Infraestructure.Repositorys.Concrete;
using Webapi.Entities;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Linq;
using Webapi.Domain;

namespace Test.src.steps
{
    [Binding]
    public class SimularParcelamentoSteps
    {
        private MassaDadosBuilder MassaBuilder { get; set; }
        private Task<IActionResult> Result { get; set; }
        private void Inicializar()
        {
            this.MassaBuilder = new MassaDadosBuilder();
            this.MassaBuilder.Inicializar();
         }

        private void Destruir()
        {
            this.MassaBuilder.Destruir();
        }

        [Given(@"um comprador que informou os dados de uma compra")]
        public void GivenUmCompradorQueInformouOsDadosDeUmaCompra()
        {
            Inicializar();

            this.MassaBuilder.MontarCompra(5, 1000);             
         }

        [When(@"sistema simular a compra")]
        public void WhenSistemaSimularAcompra()
        {
            var service = new CompraService(new CompraRepository(this.MassaBuilder.Context));
            
            var controller = new CompraController(service);
            var result = controller.Simular(this.MassaBuilder.Compra); 

            var okResponse = result as OkObjectResult;
            this.MassaBuilder.Compra = okResponse.Value as Compra;
        } 

        [Then(@"o sistema apresenta uma compra com as informações de quantidade de parcelas, valor total, valor do juros e data da compra")]
        public void ThenOsistemaApresentaUmaCompraComAsinformacoesDeQuantidadeDeParcelasValorTotalValorDoJurosEdataDaCompra()
        {
            Assert.IsNotNull(this.MassaBuilder.Compra);
            Assert.IsTrue(this.MassaBuilder.Compra.QuantidadeParcelas > 0);
            Assert.IsTrue(this.MassaBuilder.Compra.ValorTotal > 0);
            Assert.IsTrue(this.MassaBuilder.Compra.Juros > 0);
            Assert.IsTrue(this.MassaBuilder.Compra.Data.Year > 1970);
        }

        [Then(@"um montante do valor total que sera pago")]
        public void ThenUmMontanteDoValorTotalQueSeraPago()
        {
            var montante = this.MassaBuilder.Compra.CalcularMontante();
             
            Assert.IsTrue(montante > 0);
        }

        [Then(@"os valores do juros das parcelas podem ser representados em ate quatro casas decimais")]
        public void ThenOsValoresDoJurosDasParcelasPodemSerRepresentadosEmAteQuatroCasasDecimais()
        {
            foreach(var item in this.MassaBuilder.Compra.Parcelas.ToList())
            {
                var casasDecimais = AtributosDeNumeros.ObterCasasDecimais(item.Juros);
                Assert.IsTrue(casasDecimais <= 4);
            }           
        }

        [Then(@"os valores da compra como o valor das parcelas e montante devem conter duas casas decimais")]
        public void ThenOsValoresDaCompraComoOValorDasParcelasEmontanteDevemConterDuasCasasDecimais()
        {
            var montante = this.MassaBuilder.Compra.CalcularMontante();
            var casasDecimaisMontante = AtributosDeNumeros.ObterCasasDecimais(montante);
            
            Assert.IsTrue(casasDecimaisMontante <= 2);

            foreach(var item in this.MassaBuilder.Compra.Parcelas.ToList())
            {
                var casasDecimaisValorParcela = AtributosDeNumeros.ObterCasasDecimais(item.Valor);
                Assert.IsTrue(casasDecimaisValorParcela <= 2);
            } 
        }

        [Then(@"o valor do montante deve corresponder a soma dos valores das parcelas")]
        public void ThenOvalorDoMontanteDeveCorresponderAsomaDosValoresDasParcelas()
        {
            var montante = this.MassaBuilder.Compra.CalcularMontante();
            var somaDasParcelas = this.MassaBuilder.Compra.Parcelas.Sum(p => p.Valor);
             
            Assert.AreEqual(montante, somaDasParcelas);
        }

        [Then(@"a diferença de valores do montante e da soma das parcelas deve ser inferior a um centavo")]
        public void ThenAdiferencaDeValoresDoMontanteEdaSomaDasParcelasDeveSerInferiorAumCentavo()
        {
            var montante = this.MassaBuilder.Compra.CalcularMontante();
            var somaDasParcelas = this.MassaBuilder.Compra.Parcelas.Sum(p => p.Valor);
            var diferenca = montante - somaDasParcelas;

            Assert.IsTrue(diferenca < 0.01m);
        }
    }
}