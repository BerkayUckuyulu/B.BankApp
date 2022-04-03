using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Udemy.BankApp.Web.Data.Context;

namespace Udemy.BankApp.Web.Data.Repositories
{
    public class Repository<T>:IRepository<T> where T:class,new()
    {
        private readonly BankContext _context;

        public Repository(BankContext context)
        {
            _context = context;
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
          

        }
        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
           

        }
        public List<T> GetAll()
        {
           return _context.Set<T>().ToList();
        }
        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
         
        }
        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);

        }

        public IQueryable<T> GetQueryable()
        {
            return _context.Set<T>().AsQueryable();
        }
    }
}
