using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobApplicationsGR.Entities;
using JobApplicationsGR.Models.ViewModels;
using JobApplicationsGR.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationsGR.Controllers
{
    public class StatusController : Controller
    {
        private IUnitOfWork unitOfWork;

        public StatusController(IUnitOfWork unitOfWork)
        {

            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var statuses = unitOfWork.Repository<Status>().GetAll().Select(status => new StatusViewModel { Id = status.Id, Status = status.Description });

            return View(statuses);
        }
            // GET: Status/Details/5
        public ActionResult Details(int id)
        {
            var status = unitOfWork.Repository<Status>().Get(x => x.Id == id);
            StatusViewModel model = new StatusViewModel { Status = status.Description };

            return View(model);
        }

        // GET: Status/Create
        public ActionResult Create()
        {

            return View( new StatusEditViewModel());
        }

        // POST: Status/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StatusEditViewModel model)
        {
            try
            {
                if (ModelState.IsValid == false) return View(model);

                Status newStatus = new Status { Description=model.Status};

                unitOfWork.Repository<Status>().Add(newStatus);
                unitOfWork.SaveChanges();

                return RedirectToAction("Details", new { Id = newStatus.Id });

            }
            catch
            {
                return View(model);
            }
        }

        // GET: Status/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var status = unitOfWork.Repository<Status>().Get(x=>x.Id==id);

            return View( new StatusEditViewModel { Id=status.Id, Status=status.Description});
        }

        // POST: Status/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, StatusEditViewModel model)
        {
            try
            {
                if (ModelState.IsValid == false) return View(model);

                var status = unitOfWork.Repository<Status>().Get(x => x.Id == model.Id);

                status.Description = model.Status;


                unitOfWork.Repository<Status>().Update(status);
                unitOfWork.SaveChanges();

                return RedirectToAction("Details", new { Id = status.Id });

            }
            catch
            {
                return View(model);
            }
        }

        // GET: Status/Delete/5
        public ActionResult Delete(int id)
        {
            var status = unitOfWork.Repository<Status>().Get(x => x.Id == id);
            ViewBag.Status = status.Description;
            return View(new StatusEditViewModel { Id = status.Id, Status = status.Description });
        }

        // POST: Status/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(StatusEditViewModel model)
        {
            try
            {
                var statusCount = unitOfWork.Repository<Job>().FindBy(x => x.StatusId==model.Id).Count();

                if(statusCount > 0)
                {
                    ModelState.AddModelError("Status","Cannot delete status.  There are one or more Job records that contain this status.");
                }
                if(ModelState.IsValid == false) return View(model);

                var statusToDelete = unitOfWork.Repository<Status>().Get(x => x.Id == model.Id);
                unitOfWork.Repository<Status>().Delete(statusToDelete);
                unitOfWork.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }
    }
}