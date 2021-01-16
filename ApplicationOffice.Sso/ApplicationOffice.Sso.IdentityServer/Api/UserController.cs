using System.Net.Mime;
using System.Threading.Tasks;
using ApplicationOffice.Common.Api.Cors;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationOffice.Sso.IdentityServer.Api
{
    [ApiController]
    [ApiVersion("1.0")]
    [EnableCors(CorsConstants.Cors)]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserController : ControllerBase
    {
        public UserController()
        {
        }

        [HttpGet]
        public async Task<IActionResult> Test()
        {
            await Task.Yield();
            return Ok("test");
        }
    }
}