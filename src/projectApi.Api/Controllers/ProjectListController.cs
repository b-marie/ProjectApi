using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using projectApi.Common.Models;
using projectApi.Service.Services;

namespace projectApi.Api.Controllers
{
    [Route("projectlist")]
    [ApiController]
    public class ProjectListController : ControllerBase
    {
        ProjectListService _projectListService;
        public ProjectListController(ProjectListService projectListService)
        {
            _projectListService = projectListService;
        }

        // GET projectlist
        [HttpGet]
        public async Task<List<Project>> Get()
        {
            return await _projectListService.GetAllProjectsAsync();
        }

        // GET projectlist/guid
        [HttpGet("{id}")]
        public async Task<Project> Get(Guid id)
        {
            return await _projectListService.GetProjectByIdAsync(id);
        }

        // POST projectlist
        [HttpPost]
        public async void Post([FromBody] Project project)
        {
            await _projectListService.CreateNewProject(project);
        }

        // PUT projectlist/guid
        [HttpPut("{id}")]
        public async void Put([FromRoute]Guid id, [FromBody] Project project)
        {
            await _projectListService.UpdateProjectDetailsAsync(id, project.Title, project.Description, project.ProjectLink, project.GitHubLink);
        }

        //PUT projectlist/guid/upvote
        [HttpPut("{id}/upvote")]
        public async void Upvote(Guid id)
        {
            await _projectListService.UpvoteProjectAsyncById(id);
        }

        //PUT projectlist/guid/downvote
        [HttpPut("{id}/downvote")]
        public async void Downvote(Guid id)
        {
            await _projectListService.DownvoteProjectAsyncById(id);
        }

        //PUT projectlist/guid/complete
        [HttpPut("{id}/complete")]
        public async void Complete(Guid id)
        {
            await _projectListService.CompleteProjectAsyncById(id);
        }

        // DELETE projectlist/guid/delete
        [HttpPut("{id}/delete")]
        public async void Delete(Guid id)
        {
            await _projectListService.DeleteProjectAsyncById(id);
        }
    }
}
