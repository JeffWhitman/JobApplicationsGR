using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobApplicationsGR.Entities
{
    public class Job
    {
        public int Id { get; set; }
        public string JobTitle { get; set; }
        public int SourceId { get; set; }
        public string CompanyAppliedTo { get; set; }
        public bool HaveTheyContactedMe { get; set; }
        public int StatusId { get; set; }
        public string URL { get; set; }
        public string Skills { get; set; }
        public DateTime? DateApplied { get; set; }
    }
}
