using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace projectApi.Api.Controllers
{
    [Route("/")]
    [ApiController]
    public class ProjectApiController : ControllerBase
    {
        // GET projectlist
        [HttpGet]
        public string Get()
        {
            return "You made it to the Project API!";
        }
    }
}