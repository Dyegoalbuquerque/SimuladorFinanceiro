using System.Linq;
using System.Threading.Tasks;
using Webapi.Entities;

namespace Webapi.Infraestructure.Repositorys
{
    public interface IRepository<TEntity> where TEntity : Entity
    {   
    IQueryable<TEntity> ObterTodos();
 
    Task<TEntity> BuscarPorId(int id);
 
    Task Adicionar(TEntity entity);
 
    void Atualizar(int id, TEntity entity);
 
    void Remover(TEntity entity);
    
    Task SalvarMudancas();
    }
}