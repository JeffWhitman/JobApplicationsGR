using JobApplicationsGR.Data;
using JobApplicationsGR.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobApplicationsGR.Services
{
    public interface IUnitOfWork : IDisposable
    {
        //ApplicationContext Context { get; }
        void SaveChanges();
        IRepository<T> Repository<T>() where T : class;
    }
}
