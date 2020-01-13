using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Webapi.Entities;
using Webapi.Infraestructure.Config;

namespace Webapi.Infraestructure.Repositorys
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly SimuladorContext DbContext;
 
        public Repository(SimuladorContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public virtual IQueryable<T> ObterTodos()
        {
            return this.DbContext.Set<T>().AsNoTracking();
        }

        public virtual async Task<T> BuscarPorId(int id)
        {
            return await this.DbContext.Set<T>()
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(e => e.Id == id);
        }

        public virtual async Task Adicionar(T entity)
        {
            await this.DbContext.Set<T>().AddAsync(entity);
        }
 
        public virtual void Atualizar(int id, T entity)
        {
             this.DbContext.Set<T>().Update(entity);
        }
 
        public virtual void Remover(T entity)
        {       
           this.DbContext.Set<T>().Remove(entity);
        }

        public virtual async Task SalvarMudancas()
        {
            await this.DbContext.SaveChangesAsync();
        }
    }
}