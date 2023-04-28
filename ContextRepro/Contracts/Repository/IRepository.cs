using System.Linq.Expressions;

namespace ContextRepro.Contracts.Repository
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity, bool saveImmediately);
        void AddOrUpdate(T entity, bool saveImmediately);
        T CreateInstance();
        bool Exists(T entity);
        bool Save();
        void Update(T entity, bool saveImmediately);
        T Get(long id);
    }
}
