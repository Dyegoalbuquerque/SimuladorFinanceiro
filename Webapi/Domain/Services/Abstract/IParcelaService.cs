using System.Collections.Generic;
using Webapi.Entities;

namespace Webapi.Domain.Services.Abstract
{
    public interface IParcelaService
    {
           List<Parcela> ObterTodos();
    }
}