using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using ApplicationOffice.Approvals.Api.Tools;
using ApplicationOffice.Approvals.Core.Contracts;
using ApplicationOffice.Approvals.Core.Contracts.Enums;
using ApplicationOffice.Approvals.Core.Contracts.Models;
using ApplicationOffice.Common.Api.Cors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationOffice.Approvals.Api.Controllers
{
    /// <summary>
    /// Application approvers
    /// </summary>
    [Authorize]
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
        [ProducesResponseType(typeof(ApplicationApproverDto), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetApplicationApprovers(long applicationId)
        {
            var applicationApprovers = await _service.GetApplicationApprovers(applicationId);

            return Ok(applicationApprovers);
        }

        [HttpPut("{applicationId}/approve")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        public async Task<IActionResult> Approve(long applicationId)
        {
            await _service.MakeDecision(User.GetUserIdOrThrow(), applicationId, ApplicationApproverStatus.Approved);

            return Ok();
        }

        [HttpPut("{applicationId}/reject")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        public async Task<IActionResult> Reject(long applicationId)
        {
            await _service.MakeDecision(User.GetUserIdOrThrow(), applicationId, ApplicationApproverStatus.Rejected);

            return Ok();
        }
    }
}
