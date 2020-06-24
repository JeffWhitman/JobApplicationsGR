using JobApplicationsGR.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactGenericUnitOfWork.Data
{
    public static class ModelBuilderExtensions
    {
            public static void Seed(this ModelBuilder modelBuilder)
            {

            //modelBuilder.Entity<Goal>().HasData(
            //             new Goal
            //             {
            //                 Id = 1,
            //                 Description = "Goal 1",
            //                 DateCompleted = DateTime.Now,
            //                 Comments="Comments 1"
            //             },
            //            new Goal
            //            {
            //                Id = 2,
            //                Description = "Goal 2",
            //                Comments = "Comments 2"
            //            }
            //        );

            }
    }
}
