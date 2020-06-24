using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JobApplicationsGR.Models;
using JobApplicationsGR.Services;
using JobApplicationsGR.Entities;
using JobApplicationsGR.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JobApplicationsGR.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWork unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {

            this.unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(int? pageNumber, string searchString, string submitButton)
        {
            if(submitButton== "Clear Filter")
            {
                searchString = string.Empty;
            }
            ViewData["CurrentFilter"] = searchString;

            List<JobsViewModel> model = new List<JobsViewModel>();

            var jobs = unitOfWork.Repository<Job>().GetAll().ToList();

            foreach(var job in jobs)
            {
                var source = unitOfWork.Repository<Source>().Get(x=>x.Id==job.SourceId);
                var status = unitOfWork.Repository<Status>().Get(x => x.Id == job.StatusId);
                string dateApplied = job.DateApplied == null ? string.Empty : Convert.ToDateTime(job.DateApplied).ToShortDateString();

                var jobViewModel = new JobsViewModel { Id = job.Id, CompanyAppliedTo = job.CompanyAppliedTo, JobTitle = job.JobTitle, HaveTheyContactedMe = job.HaveTheyContactedMe, Source = source.Description,Status=status.Description, URL=job.URL,DateApplied= dateApplied };
                
                model.Add(jobViewModel);
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                model = model.Where(s => s.CompanyAppliedTo.Contains(searchString,StringComparison.OrdinalIgnoreCase)).ToList();
            }
            int pageSize = 10;
            return View(await PaginatedList<JobsViewModel>.CreateAsync(model.AsQueryable(), pageNumber ?? 1, pageSize));
            //return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var job = unitOfWork.Repository<Job>().Get(x => x.Id == Id);

            var sources = unitOfWork.Repository<Source>().GetAll().Select(u => new SelectListItem
            {
                Text = u.Description,
                Value = u.Id.ToString()
            });

            var statuses = unitOfWork.Repository<Status>().GetAll().Select(u => new SelectListItem
            {
                Text = u.Description,
                Value = u.Id.ToString()
            });
            var model = new JobEditViewModel { Statuses = statuses, Sources = sources,Id=job.Id,JobTitle=job.JobTitle,StatusId=job.StatusId,SourceId=job.SourceId,HaveTheyContactedMe=job.HaveTheyContactedMe,CompanyAppliedTo=job.CompanyAppliedTo, URL=job.URL,DateApplied=job.DateApplied == null ? Convert.ToDateTime(job.DateApplied) : DateTime.Now };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(JobEditViewModel model)
        {
            if (ModelState.IsValid == false) return View(model);

            var job = unitOfWork.Repository<Job>().Get(x => x.Id == model.Id);

            job.JobTitle = model.JobTitle;
            job.CompanyAppliedTo = model.CompanyAppliedTo;
            job.SourceId = model.SourceId;
            job.StatusId= model.StatusId;
            job.HaveTheyContactedMe = model.HaveTheyContactedMe;
            job.URL = model.URL;
            job.Skills = model.Skills;
            job.DateApplied = model.DateApplied;

            unitOfWork.Repository<Job>().Update(job);
            unitOfWork.SaveChanges();

            return RedirectToAction("Details", new { Id = job.Id });
            //return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            var sources = unitOfWork.Repository<Source>().GetAll().Select(u => new SelectListItem
            {
                Text = u.Description,
                Value = u.Id.ToString()
            });

            var statuses = unitOfWork.Repository<Status>().GetAll().Select(u => new SelectListItem
            {
                Text = u.Description,
                Value = u.Id.ToString()
            });
            var model = new JobEditViewModel { Statuses=statuses, Sources=sources,DateApplied=DateTime.Now}; 

            return View(model);
        }
        
        [HttpPost]
        public IActionResult Create(JobEditViewModel model)
        {
            if (ModelState.IsValid == false) return View(model);

            Job newJob = new Job { JobTitle = model.JobTitle, CompanyAppliedTo = model.CompanyAppliedTo, StatusId = model.StatusId, SourceId = model.SourceId,HaveTheyContactedMe=model.HaveTheyContactedMe, URL=model.URL,Skills=model.Skills,DateApplied=model.DateApplied };

            unitOfWork.Repository<Job>().Add(newJob);
            unitOfWork.SaveChanges();

            return RedirectToAction("Details", new { Id = newJob.Id });
            //return View();
        }
        public IActionResult Details(int Id)
        {
            var job = unitOfWork.Repository<Job>().Get(x=>x.Id==Id);
            var source = unitOfWork.Repository<Source>().Get(x => x.Id == job.SourceId);
            var status = unitOfWork.Repository<Status>().Get(x => x.Id == job.StatusId);

            var jobViewModel = new JobsViewModel { Id = job.Id, CompanyAppliedTo = job.CompanyAppliedTo, JobTitle = job.JobTitle, HaveTheyContactedMe = job.HaveTheyContactedMe, Source = source.Description, Status = status.Description, URL=job.URL, Skills = job.Skills };


            return View(jobViewModel);
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var job = unitOfWork.Repository<Job>().Get(x => x.Id == Id);
            var source = unitOfWork.Repository<Source>().Get(x => x.Id == job.SourceId);
            var status = unitOfWork.Repository<Status>().Get(x => x.Id == job.StatusId);

            var jobViewModel = new JobsViewModel { Id = job.Id, CompanyAppliedTo = job.CompanyAppliedTo, JobTitle = job.JobTitle, HaveTheyContactedMe = job.HaveTheyContactedMe, Source = source.Description, Status = status.Description,URL=job.URL };


            return View(jobViewModel);
        }

        [HttpPost]
        public IActionResult Delete(JobsViewModel model)
        {
            var job = unitOfWork.Repository<Job>().Get(x => x.Id == model.Id);

            unitOfWork.Repository<Job>().Delete(job);
            unitOfWork.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "About my wonderful program!";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Contact me!";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
