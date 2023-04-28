using ContextRepro.Contracts.Repository;
using ContextRepro.Infrastructure.Core;
using Microsoft.EntityFrameworkCore;

namespace ContextRepro.Repositories
{

    public class DbContextRepository<T, U> : IDisposable, IRepository<T> where T : class where U : DbContext
    {
        protected readonly U Context;
        protected readonly DbSet<T> Set;
        protected readonly EntityKeyManager EntityKeyManager;

        public DbContextRepository(U context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            Set = Context.Set<T>();

            EntityKeyManager = new EntityKeyManager();
        }

        public virtual void Add(T entity, bool saveImmediately = true)
        {
            Set.Add(entity);

            OnBeforeSave(entity);
            Context.Entry(entity).State = EntityState.Added;

            if (saveImmediately)
            {
                Save();
            }

            OnAfterSave(entity);
        }

        public virtual void Update(T entity, bool saveImmediately = true)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                Set.Attach(entity);
            }

            OnBeforeSave(entity);
            Context.Entry(entity).State = EntityState.Modified;

            if (saveImmediately)
            {
                Save();
            }

            OnAfterSave(entity);
        }

        public virtual void AddOrUpdate(T entity, bool saveImmediately = true)
        {
            if (Exists(entity))
            {
                Update(entity, saveImmediately);
            }
            else
            {
                Add(entity, saveImmediately);
            }
        }

        private bool _disposed;

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    //_ctx.Dispose(); // NICHT Disposen, der kam von außen!!
                }
            }

            _disposed = true;
        }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual T Get(long id)
        {
            return Set.Find(id);
        }

        /// <summary>
        /// Check if entity exists by searching by entity's primary key(s).
        /// To lookup by other fields (non-keys) please use Get(Expression&lt;Func&lt;T, bool>> filter, ...)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual bool Exists(T entity)
        {
            // NOTE: .NET Core version from: https://entityframeworkcore.com/knowledge-base/42485128/entity-framework-core--what-is-the-fastest-way-to-check-if-a-generic-entity-is-already-in-the-dbset-and-then-add-or-update-it-
            if (entity == null)
            {
                return false;
            }

            var entityType = Context.Model.FindEntityType(typeof(T));
            var keyValues = EntityKeyManager.GetEntityKeyValues(entityType, entity);
            var obj = Context.Set<T>().Find(keyValues);
            return obj != null;
        }

        public virtual bool Save()
        {
            var entities = Context.ChangeTracker.Entries();

            // entities: required for internal interceptors/filters

            var context = Context as Context;
            return Context.SaveChanges() > 0;
        }

        public virtual T CreateInstance() => Set.CreateInstance();

        protected virtual void OnBeforeSave(T entity)
        {
        }

        protected virtual void OnAfterSave(T entity)
        {
        }
    }
}
