using System;
using System.Globalization;

namespace Webapi.Domain
{
    public static class AtributosDeNumeros
    {
        public static int ObterQuantidadeDeCasasDoNumero(decimal numero)
        {
            if(numero == 0)
            {
                return 1;
            }

            string texto = Convert.ToString(numero);

            if(texto.IndexOf(".") < 0)
            {
                return texto.Length;
            }else
            {
               return (numero.ToString(CultureInfo.InvariantCulture)).Split('.')[0].Length;
            }       
        }

        public static int ObterCasasDecimais(decimal numero)
        {
            if(Convert.ToString(numero).IndexOf(".") < 0)
            {
                return 0;
            }
            return (numero.ToString(CultureInfo.InvariantCulture)).Split('.')[1].Length;
        }
    }
}