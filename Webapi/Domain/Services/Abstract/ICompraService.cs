using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Webapi.Entities;

namespace Webapi.Domain.Services.Abstract
{
    public interface ICompraService
    {
        Task<Compra> BuscarPorId(int id);
        Task Adicionar(Compra item);
        Task<List<Compra>> ObterTodos();
        Task Remover(int id);

        Task SalvarMudancas();
    }
}