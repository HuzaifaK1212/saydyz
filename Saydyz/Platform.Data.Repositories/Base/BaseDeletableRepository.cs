using Platform.Data.Model;
using Platform.Data.Repositories.Interfaces;

namespace Platform.Data.Repositories
{
    public class BaseDeletableRepository<TEntity, TContext> : BaseRepository<TEntity, TContext> where TEntity : BaseEntity
         where TContext : IDbContext
    {
        public BaseDeletableRepository(IUnitOfWork<TContext> uow) : base(uow)
        {
        }

        public override void Delete(int id)
        {
            var entity = Set.Find(id);
            if (entity != null)
            {
                Set.Remove(entity);
            }
        }
      

        public override void Delete(TEntity entity)
        {
            Set.Remove(entity);
        }
    }
}
