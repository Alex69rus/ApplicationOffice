using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using ApplicationOffice.Approvals.Core.Contracts;
using ApplicationOffice.Approvals.Core.Contracts.Models;
using ApplicationOffice.Common.Api.Cors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationOffice.Approvals.Api.Controllers
{
    /// <summary>
    /// Unit approvers
    /// </summary>
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces(MediaTypeNames.Application.Json)]
    [EnableCors(AoCorsConstants.Cors)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UnitApproverController : ControllerBase
    {
        private readonly IUnitApproverService _service;

        public UnitApproverController(IUnitApproverService service)
        {
            _service = service;
        }

        [HttpGet("{unitId}")]
        [ProducesResponseType(typeof(UnitApproverDto[]), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUnitApprovers(long unitId)
        {
            var unitApprovers = await _service.GetUnitApprovers(unitId);

            return Ok(unitApprovers);
        }
    }
}