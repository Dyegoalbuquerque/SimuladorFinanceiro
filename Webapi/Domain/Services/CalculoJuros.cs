
using System;

namespace Webapi.Domain.Services
{
    public static class CalculoJuros
    {
        public static decimal CalcularParcela(decimal valorTotal, decimal juros, int parcelas)
        {
            // formula -> R = V x (juros / 1 - (1 + Juros)^-parcelas)
            // intervalo entre 0.000001... ate 99.999999...

            var somaDoJuros = 1 + juros;

            var casasDecimais = AtributosDeNumeros.ObterCasasDecimais(somaDoJuros);

            var denominador = ObterDenominador(casasDecimais);
            var numerador = somaDoJuros * denominador;

            var potenciaNumerador = CalcularPotencia(numerador, parcelas);
            var potenciaDenominador = CalcularPotencia(denominador, parcelas);

            var calculoDaPotencia = (potenciaDenominador / potenciaNumerador);
            
            return  valorTotal * (juros / (1 - calculoDaPotencia));
        }

        public static decimal CalcularPotencia(decimal numero, int expoente)
        { 
            decimal valor = 1;

            for(int i = 0; i < expoente; i++)
            {
                valor *= numero;
            }           

            return  valor;
        }
       
        private static long ObterDenominador(int numeroCasasDecimais)
        {
            string denominador = "1";

            for(int i = 0; i < numeroCasasDecimais; i++)
            {
                denominador += "0";
            } 

            return Convert.ToInt64(denominador);
        }
     }
}