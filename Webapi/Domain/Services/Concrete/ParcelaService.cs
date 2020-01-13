using System.Collections.Generic;
using Webapi.Infraestructure.Repositorys.Abstract;
using Webapi.Domain.Services.Abstract;
using Webapi.Entities;
using System.Linq;

namespace Webapi.Domain.Services.Concrete
{
    public class ParcelaService: IParcelaService
    {
        private IParcelaRepository Repository { get; set;}
        public ParcelaService(IParcelaRepository repository)
        {
            this.Repository = repository;
        }

        public List<Parcela> ObterTodos()
        {
            return this.Repository.ObterTodos().ToList();
        }
    }
}