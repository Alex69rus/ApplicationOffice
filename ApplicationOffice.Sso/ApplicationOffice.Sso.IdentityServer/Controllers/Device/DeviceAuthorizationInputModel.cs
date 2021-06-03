using ApplicationOffice.Sso.IdentityServer.Controllers.Consent;

namespace ApplicationOffice.Sso.IdentityServer.Controllers.Device
{
    public class DeviceAuthorizationInputModel : ConsentInputModel
    {
        public string UserCode { get; set; }
    }
}