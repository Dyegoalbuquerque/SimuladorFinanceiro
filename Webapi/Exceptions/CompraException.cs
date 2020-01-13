using System;

namespace Webapi.Exceptions
{
    public class CompraException : Exception
    {
        public CompraException(string mensagem): base(mensagem)
        {           
        }
    }
}