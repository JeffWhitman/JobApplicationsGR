using JobApplicationsGR.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobApplicationsGR.Models.ViewModels
{
    public class JobEditViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }

        public int SourceId { get; set; }

        [Display(Name = "Source")]
        public IEnumerable<SelectListItem> Sources { get; set; }

        [Display(Name = "Company Applied To")]
        public string CompanyAppliedTo { get; set; }

        [Display(Name = "Have They Contacted Me?")]
        public bool HaveTheyContactedMe { get; set; }

        [Display(Name = "Status")]
        public IEnumerable<SelectListItem> Statuses { get; set; }
        public int StatusId { get; set; }

        [Display(Name = "Job Link")]
        public string URL { get; set; }

        [Display(Name ="Skills")]
        public string Skills { get; set; }

        [Display(Name = "Date Applied")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        public DateTime DateApplied { get; set; }

    }
}
