using SanTsgProject.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SanTsgProject.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private protected readonly UserDbContext _userDbContext;
        public Repository(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public void Add(T entity)
        {
            _userDbContext.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _userDbContext.Set<T>().AddRange(entities);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> filter)
        {
            return _userDbContext.Set<T>().Where(filter);
        }

        public T Get(int id)
        {
            return _userDbContext.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _userDbContext.Set<T>().ToList();
        }

        public void Remove(T entity)
        {
            _userDbContext.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _userDbContext.Set<T>().RemoveRange(entities);
        }

        void IRepository<T>.Update(T entity)
        {
            _userDbContext.Set<T>().Update(entity);
        }
    }
}
