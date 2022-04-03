using Udemy.BankApp.Web.Data.Repositories;

namespace Udemy.BankApp.Web.Data.UnitOfWork
{
    public interface IUow
    {
        IRepository<T> GetRepository<T>() where T : class, new();
        void SaveChanges();
    }
}