using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobApplicationsGR.Models.ViewModels
{
    public class StatusViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }
    }
}
