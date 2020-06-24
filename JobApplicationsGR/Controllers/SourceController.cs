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
    public class SourceController : Controller
    {
    // GET: Source
        private IUnitOfWork unitOfWork;

        public SourceController(IUnitOfWork unitOfWork)
        {

            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var sources = unitOfWork.Repository<Source>().GetAll().Select(source => new SourceViewModel { Id = source.Id, Source = source.Description });

            return View(sources);
        }
            // GET: Status/Details/5
        public ActionResult Details(int id)
        {
            var source = unitOfWork.Repository<Source>().Get(x => x.Id == id);
            SourceViewModel model = new SourceViewModel { Source = source.Description };

            return View(model);
        }

        // GET: Status/Create
        public ActionResult Create()
        {

            return View(new SourceEditViewModel());
        }

        // POST: Status/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SourceEditViewModel model)
        {
            try
            {
                if (ModelState.IsValid == false) return View(model);

                Source newSource = new Source { Description = model.Source };

                unitOfWork.Repository<Source>().Add(newSource);
                unitOfWork.SaveChanges();

                return RedirectToAction("Details", new { Id = newSource.Id });

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
            var source = unitOfWork.Repository<Source>().Get(x => x.Id == id);

            return View(new SourceEditViewModel { Id = source.Id, Source = source.Description });
        }

        // POST: Status/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SourceEditViewModel model)
        {
            try
            {
                if (ModelState.IsValid == false) return View(model);

                var source = unitOfWork.Repository<Source>().Get(x => x.Id == model.Id);

                source.Description = model.Source;


                unitOfWork.Repository<Source>().Update(source);
                unitOfWork.SaveChanges();

                return RedirectToAction("Details", new { Id = source.Id });

            }
            catch
            {
                return View(model);
            }
        }

        // GET: Status/Delete/5
        public ActionResult Delete(int id)
        {
            var source = unitOfWork.Repository<Source>().Get(x => x.Id == id);
            ViewBag.Status = source.Description;
            return View(new SourceEditViewModel { Id = source.Id, Source = source.Description });
        }

        // POST: Status/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(StatusEditViewModel model)
        {
            try
            {
                var sourceCount = unitOfWork.Repository<Job>().FindBy(x => x.StatusId == model.Id).Count();

                if (sourceCount > 0)
                {
                    ModelState.AddModelError("Source", "Cannot delete source.  There are one or more Job records that contain this source.");
                }
                if (ModelState.IsValid == false) return View(model);

                var sourceToDelete = unitOfWork.Repository<Source>().Get(x => x.Id == model.Id);
                unitOfWork.Repository<Source>().Delete(sourceToDelete);
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