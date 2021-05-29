using System.Net.Mime;
using System.Threading.Tasks;
using ApplicationOffice.Approvals.Core.Contracts;
using ApplicationOffice.Approvals.Core.Contracts.Enums;
using ApplicationOffice.Common.Api.Cors;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationOffice.Approvals.Api.Controllers
{
    /// <summary>
    /// Application approvers
    /// </summary>

    // [Authorize] TODO: enable AUTH
    [ApiController]
    [ApiVersion("1.0")]
    [Produces(MediaTypeNames.Application.Json)]
    [EnableCors(AoCorsConstants.Cors)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ApplicationApproverController : ControllerBase
    {
        private readonly IApplicationApproverService _service;

        public ApplicationApproverController(IApplicationApproverService service)
        {
            _service = service;
        }

        [HttpGet("{applicationId}")]
        public async Task<IActionResult> GetApplicationApprovers(long applicationId)
        {
            var applicationApprovers = await _service.GetApplicationApprovers(applicationId);

            return Ok(applicationApprovers);
        }

        [HttpPut("{applicationId}/approve")]
        public async Task<IActionResult> Approve(long applicationId, long approverId)
        {
            await _service.MakeDecision(approverId, applicationId, ApplicationApproverStatus.Approved);

            return Ok();
        }

        [HttpPut("{applicationId}/reject")]
        public async Task<IActionResult> Reject(long applicationId, long approverId)
        {
            await _service.MakeDecision(approverId, applicationId, ApplicationApproverStatus.Rejected);

            return Ok();
        }
    }
}
