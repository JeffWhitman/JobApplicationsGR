using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobApplicationsGR.Models.ViewModels
{
    public class JobsViewModel
    {
        public int Id { get; set; }

        [Display(Name ="Job Title")]
        public string JobTitle { get; set; }
        public string Source { get; set; }

        [Display(Name = "Company Applied To")]
        public string CompanyAppliedTo { get; set; }

        [Display(Name = "Have They Contacted Me?")]
        public bool HaveTheyContactedMe { get; set; }
        public string Status { get; set; }

        [Display(Name = "Job Link")]
        public string URL { get; set; }

        [Display(Name = "Skills")]
        public string Skills { get; set; }

        [Display(Name = "Date Applied")]
        public string DateApplied { get; set; }

    }
}
