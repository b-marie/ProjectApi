using Amazon.DynamoDBv2.DocumentModel;
using projectApi.Common.Models;
using projectApi.Service.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace projectApi.Service.Services
{
    public class ProjectListService
    {
        private DynamoDBService _dynamoDBService;

        private static string tableName = "ProjectList";
        private static string region = "us-west-2";

        public ProjectListService(DynamoDBService dynamoDBService)
        {
            _dynamoDBService = dynamoDBService;
        }

        private Project ConvertDocumentToProject(Document document)
        {
            string projectLink = String.IsNullOrWhiteSpace(document["ProjectLink"]) ? "blank" : document["ProjectLink"].AsString();
            string githubLink = String.IsNullOrWhiteSpace(document["GitHubLink"]) ? "blank" : document["GitHubLink"].AsString();

            return new Project(document["Id"].AsGuid(), 
                document["Title"], 
                document["Description"], 
                projectLink, 
                githubLink, 
                document["Votes"].AsInt(), 
                document["Completed"].AsBoolean(),
                document["Deleted"].AsBoolean());
        }

        private Document ConvertProjectToDocument(Project project)
        {
            var dynamoProject = new Document();
            dynamoProject["Id"] = project.Id.ToString();
            dynamoProject["Title"] = project.Title;
            dynamoProject["Description"] = project.Description;
            dynamoProject["ProjectLink"] = project.ProjectLink;
            dynamoProject["GitHubLink"] = project.GitHubLink;
            dynamoProject["Votes"] = project.Votes;
            dynamoProject["Completed"] = new DynamoDBBool(project.Completed);
            dynamoProject["Deleted"] = new DynamoDBBool(project.Deleted);

            return dynamoProject;
        }

        public async Task CreateNewProject(Project project)
        {
            Table projectListTable = _dynamoDBService.LoadTable(tableName, region);

            var dynamoProject = new Document();
            dynamoProject["Id"] = Guid.NewGuid();
            dynamoProject["Title"] = project.Title;
            dynamoProject["Description"] = project.Description;
            dynamoProject["ProjectLink"] = "blank";
            dynamoProject["GitHubLink"] = "blank";
            dynamoProject["Votes"] = 0;
            dynamoProject["Completed"] = new DynamoDBBool(false);
            dynamoProject["Deleted"] = new DynamoDBBool(false);

            await projectListTable.PutItemAsync(dynamoProject);
        }

        public async Task<Project> GetProjectByIdAsync(Guid id)
        {
            Table projectListTable = _dynamoDBService.LoadTable(tableName, region);
            Document projectDocument = await projectListTable.GetItemAsync(id.ToString());
            if (projectDocument != null)
            {
                return ConvertDocumentToProject(projectDocument);
            }
            throw new KeyNotFoundException();
        }

        public async Task<List<Project>> GetAllProjectsAsync()
        {
            Table projectListTable = _dynamoDBService.LoadTable(tableName, region);
            List<Document> results = await _dynamoDBService.GetAllDocumentsFromTable(projectListTable);
            List<Project> projects = new List<Project>();
            foreach(Document result in results)
            {
                projects.Add(ConvertDocumentToProject(result));
            }
            return projects;
        }

        private async Task UpdateProjectAsync(Project project)
        {
            Table projectListTable = _dynamoDBService.LoadTable(tableName, region);
            await projectListTable.UpdateItemAsync(ConvertProjectToDocument(project));
        }

        public async Task UpdateProjectDetailsAsync(Guid id, string title, string description, string projectLink, string gitHubLink)
        {
            Project project = await GetProjectByIdAsync(id);
            // These are the only things that should be publically available to update
            project.Title = title;
            project.Description = description;
            project.ProjectLink = String.IsNullOrWhiteSpace(projectLink)? "blank" : projectLink;
            project.GitHubLink = String.IsNullOrWhiteSpace(gitHubLink)? "blank" : gitHubLink;
            await UpdateProjectAsync(project);
        }

        public async Task UpvoteProjectAsyncById(Guid id)
        {
            Project project = await GetProjectByIdAsync(id);
            project.Votes++;
            await UpdateProjectAsync(project);
        }

        public async Task DownvoteProjectAsyncById(Guid id)
        {
            Project project = await GetProjectByIdAsync(id);
            project.Votes--;
            await UpdateProjectAsync(project);
        }

        public async Task CompleteProjectAsyncById(Guid id)
        {
            Project project = await GetProjectByIdAsync(id);
            project.Completed = true;
            await UpdateProjectAsync(project);
        }

        public async Task DeleteProjectAsyncById(Guid id)
        {
            Project project = await GetProjectByIdAsync(id);
            project.Deleted = true;
            await UpdateProjectAsync(project);
        }

    }
}
