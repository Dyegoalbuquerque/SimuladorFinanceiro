using System.Collections.Generic;
using Webapi.Infraestructure.Repositorys.Abstract;
using Webapi.Entities;
using Webapi.Infraestructure.Config;

namespace Webapi.Infraestructure.Repositorys.Concrete
{
    public class ParcelaRepository : Repository<Parcela>, IParcelaRepository
    {
        public ParcelaRepository(SimuladorContext dbContext) : base(dbContext) {}
    }
}