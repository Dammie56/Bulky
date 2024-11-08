using System.Linq.Expressions;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        //T CATEGORY
        IEnumerable<T> GetAll(string? includeProperties = null);  //This is to retrieve all of the category
        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null); //To get a single category
        void Add(T entity);
        void Remove(T entity);
        void Removerange(IEnumerable<T> entity); // This remove multiple entities in a single call
    }
}
