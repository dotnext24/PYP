using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PYP.Domain.Entities.Core
{
    public class EntityRepository<T> : IEntityRepository<T> where T:class,IEntity,new()
    {
        readonly DbContext _entityContext;


        public EntityRepository(DbContext entityContext)
        {
            if(entityContext==null)
            {
                throw new ArgumentNullException();
            }

            _entityContext = entityContext;
        }

        public virtual IQueryable<T> All
        {
           get { return GetAll(); }
        }

        public virtual void Add(T entity)
        {
            DbEntityEntry dbEntityEntry = _entityContext.Entry<T>(entity);
            _entityContext.Set<T>().Add(entity);
        }

        public virtual IQueryable<T> AllEncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _entityContext.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public virtual void Delete(T entity)
        {
            DbEntityEntry dbEntityEntry = _entityContext.Entry(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        public virtual void Edit(T entity)
        {
            DbEntityEntry dbEntityRntry = _entityContext.Entry(entity);
            dbEntityRntry.State = EntityState.Modified;
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _entityContext.Set<T>().Where(predicate);
        }

        public virtual IQueryable<T> GetAll()
        {
            return _entityContext.Set<T>();
        }

        public virtual T GetSingle(Guid key)
        {
           return GetAll().FirstOrDefault(x => x.Key == key);
        }

        public virtual PaginatedList<T> Paginate<TKey>(int pageIndex, int pageSize, Expression<Func<T, TKey>> keySelector, Expression<Func<T, bool>> predicate, params Expression<Func<T, bool>>[] includeProperties)
        {
            return Paginate(pageIndex, pageSize, keySelector, null);
        }

        public virtual void Save()
        {
            _entityContext.SaveChanges();
        }
    }
}
