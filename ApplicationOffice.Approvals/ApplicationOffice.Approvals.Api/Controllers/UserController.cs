using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using ApplicationOffice.Approvals.Api.Tools;
using ApplicationOffice.Approvals.Core.Contracts;
using ApplicationOffice.Approvals.Core.Contracts.Models;
using ApplicationOffice.Common.Api.Cors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationOffice.Approvals.Api.Controllers
{
    /// <summary>
    /// Users
    /// </summary>
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces(MediaTypeNames.Application.Json)]
    [EnableCors(AoCorsConstants.Cors)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(FullUserDto), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetUser()
        {
            var user = await _service.GetUser(User.GetUserIdOrThrow());

            return Ok(user);
        }
    }
}