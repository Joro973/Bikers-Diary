using System;
using System.Linq;
using BikersDiary.ForumSystem.Data.Model.Contracts;

namespace BikersDiary.ForumSystem.Data.Repositories
{
    public interface IEfRepository<T> where T : class, IDeletable
    {
        IQueryable<T> All { get; }
        IQueryable<T> AllAndDeleted { get; }

        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);

        T Find(Guid id);
    }
}