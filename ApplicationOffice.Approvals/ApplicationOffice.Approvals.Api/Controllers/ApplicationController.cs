using System.Net.Mime;
using System.Threading.Tasks;
using ApplicationOffice.Approvals.Core.Contracts;
using ApplicationOffice.Approvals.Core.Contracts.Enums;
using ApplicationOffice.Approvals.Core.Contracts.Models;
using ApplicationOffice.Common.Api.Cors;
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

        [HttpGet("created/{userId}")]
        public async Task<IActionResult> GetCreatedApplications(ApplicationStatus[]? statuses, long userId)
        {
            var applications = await _service.GetCreatedApplications(
                userId, // TODO: User.GetUserIdOrThrow(),
                statuses);

            return Ok(applications);
        }

        [HttpGet("onApproval/{userId}")]
        public async Task<IActionResult> GetApprovalApplications(
            ApplicationStatus[]? statuses,
            ApplicationApproverStatus[]? approverStatuses,
            long userId)
        {
            var applications = await _service.GetApprovalApplications(
                userId, // TODO: User.GetUserIdOrThrow(),
                statuses,
                approverStatuses);

            return Ok(applications);
        }

        [HttpGet("{applicationId}")]
        public async Task<IActionResult> GetApprovalApplications(long applicationId)
        {
            var applications = await _service.Get(applicationId);

            return Ok(applications);
        }

        [HttpPost]
        public async Task<IActionResult> CreateApplication([FromBody] CreateApplicationRequestDto request)
        {
            var applicationId = await _service.CreateApplication(request);

            return Ok(applicationId);
        }

        // TODO: fix userIds thould be fetched from Auth
    }
}
