using System;
using System.Collections.Generic;
using System.Text;

namespace projectApi.Common.Models
{
    public class Project
    {
        public Project(Guid id, string title, string description, string projectLink, string gitHubLink, int votes, bool completed, bool deleted)
        {
            Id = id;
            Title = title;
            Description = description;
            ProjectLink = projectLink;
            GitHubLink = gitHubLink;
            Votes = votes;
            Completed = completed;
            Deleted = deleted;
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ProjectLink { get; set; }
        public string GitHubLink { get; set; }
        public int Votes { get; set; }
        public bool Completed { get; set; }
        public bool Deleted { get; set; }
    }
}
