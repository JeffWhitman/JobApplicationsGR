using ContactGenericUnitOfWork.Data;
using JobApplicationsGR.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobApplicationsGR.Data
{
    public class ApplicationContext:DbContext
    {
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Source> Sources { get; set; }
        public DbSet<Status> Statuses { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Seed();
        }
    }
}
