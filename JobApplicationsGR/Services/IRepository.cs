using JobApplicationsGR.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace JobApplicationsGR.Services
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(Func<T, bool> predicate);
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Attach(T entity);
    }
}
