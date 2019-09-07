using System;
using System.Collections.Generic;
using System.Text;

namespace projectApi.Common.Models
{
    public class Project
    {
        public Guid Id { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public String ProjectLink { get; set; }
        public String GitHubLink { get; set; }
        public int Votes { get; set; }
        public bool Completed { get; set; }
    }
}
