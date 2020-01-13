using Webapi.Infraestructure.Repositorys.Abstract;
using Webapi.Entities;
using Webapi.Infraestructure.Config;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Webapi.Infraestructure.Repositorys.Concrete
{
    public class CompraRepository : Repository<Compra>, ICompraRepository
    {
        public CompraRepository(SimuladorContext dbContext) : base(dbContext) {}

        public override IQueryable<Compra> ObterTodos()
        {
            return this.DbContext.Compras
                                 .Include(c => c.Parcelas)
                                 .AsNoTracking();
        }
    }
}