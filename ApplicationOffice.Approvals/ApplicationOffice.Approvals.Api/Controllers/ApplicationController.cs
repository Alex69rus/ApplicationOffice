using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationOffice.Approvals.Api.Tools;
using ApplicationOffice.Approvals.Core.Contracts;
using ApplicationOffice.Approvals.Core.Contracts.Enums;
using ApplicationOffice.Common.Api.Cors;
using ApplicationOffice.Common.Core.Constants;
using ApplicationOffice.Common.Core.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationOffice.Approvals.Api.Controllers
{
    /// <summary>
    /// Applications
    /// </summary>
    // [Authorize] TODO: enable AUTH
    [ApiController]
    [ApiVersion("1.0")]
    [Produces(MediaTypeNames.Application.Json)]
    [EnableCors(AoCorsConstants.Cors)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _service;

        public ApplicationController(IApplicationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetCreatedApplications(ApplicationStatus[]? statuses)
        {
            var applications = await _service.GetCreatedApplications(
                0, // User.GetUserIdOrThrow(),
                statuses);

            return Ok(applications);
        }
    }
}
