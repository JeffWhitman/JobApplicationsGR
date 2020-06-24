using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobApplicationsGR.Models.ViewModels
{
    public class SourceEditViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Source { get; set; }

    }
}
