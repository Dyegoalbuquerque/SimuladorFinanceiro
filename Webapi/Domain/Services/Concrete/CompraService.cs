using System;
using System.Collections.Generic;
using System.Linq;
using Webapi.Infraestructure.Repositorys.Abstract;
using Webapi.Domain.Services.Abstract;
using Webapi.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Webapi.Exceptions;

namespace Webapi.Domain.Services.Concrete
{
    public class CompraService : ICompraService
    {
        private ICompraRepository Repository { get; set;}

        private IParcelaRepository ParcelaRepository {get; set;}
        public CompraService(ICompraRepository repository, IParcelaRepository parcelaRepository)
        {
            this.Repository = repository;
            this.ParcelaRepository = parcelaRepository;
        }

        public async Task Adicionar(Compra item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("Argumento item não pode ser nulo");
            }

            if (item.Id != 0)
            {
                throw new CompraException("Id do objeto deve ser zero");
            }

           

            var parcelas = new List<Parcela>();

            for(int i=0; i < item.QuantidadeParcelas; i++)
            {
                var parcela = new Parcela() 
                { 
                    Juros = item.Juros, 
                    Valor = 4, 
                    Vencimento = DateTime.Now
                };

                parcelas.Add(parcela);
            }

            item.Parcelas = parcelas;
            await this.Repository.Adicionar(item);

            await this.SalvarMudancas();
        }
        public async Task<List<Compra>> ObterTodos()
        {
            return await this.Repository.ObterTodos().ToListAsync();
        }

        public async Task<Compra> BuscarPorId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("Id não pode ser menor ou igual a zero");
            }
            return  await this.Repository.BuscarPorId(id);
        }
    
        public async Task Remover(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("Id não pode ser menor ou igual a zero");
            }
            var item = await this.Repository.BuscarPorId(id);
            this.Repository.Remover(item);
            await this.SalvarMudancas();
        }
   
        public async Task SalvarMudancas()
        {
            await this.Repository.SalvarMudancas();
        }
    }
}