using System;
using System.Globalization;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using ApplicationOffice.Approvals.Api.Models;
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
    /// Applications
    /// </summary>
    [Authorize]
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

        [HttpGet("created")]
        [ProducesResponseType(typeof(ApplicationViewDto[]), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetCreatedApplications(ApplicationStatus[]? statuses)
        {
            var applications = await _service.GetCreatedApplications(
                User.GetUserIdOrThrow(),
                statuses);

            return Ok(applications);
        }

        [HttpGet("onApproval")]
        [ProducesResponseType(typeof(ApplicationViewDto[]), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetApprovalApplications(
            ApplicationStatus[]? statuses,
            ApplicationApproverStatus[]? approverStatuses)
        {
            var applications = await _service.GetApprovalApplications(
                User.GetUserIdOrThrow(),
                statuses,
                approverStatuses);

            return Ok(applications);
        }

        [HttpGet("{applicationId}")]
        [ProducesResponseType(typeof(FullApplicationDto), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetApplicationInfo(long applicationId)
        {
            var applications = await _service.Get(applicationId);

            return Ok(applications);
        }

        [HttpPost]
        [ProducesResponseType(typeof(long), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> CreateRegularVacationApplication([FromBody] CreateRegularVacationApplicationCommand request)
        {
            var applicationId = await _service.CreateApplication(new CreateApplicationRequestDto(
                $"{User.GetNameOrThrow()} - регулярный отпуск",
                request.Description,
                DateTime.UtcNow.Date.AddDays(4),
                ApplicationType.RegularVacation,
                User.GetUserIdOrThrow(),
                new[]
                {
                    new ApplicationFieldDto(
                        ApplicationFieldType.DateTime,
                        "Отпуск с",
                        request.VacationFrom.Date.ToString(CultureInfo.InvariantCulture)),
                    new ApplicationFieldDto(
                        ApplicationFieldType.DateTime,
                        "Отпуск по",
                        request.VacationTo.Date.ToString(CultureInfo.InvariantCulture)),
                    new ApplicationFieldDto(
                        ApplicationFieldType.Long,
                        "Количество дней",
                        ((request.VacationTo.Date - request.VacationFrom.Date).Days + 1).ToString()),
                }));

            return Ok(applicationId);
        }
    }
}
