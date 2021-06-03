using ApplicationOffice.Sso.IdentityServer.Controllers.Consent;

namespace ApplicationOffice.Sso.IdentityServer.Controllers.Device
{
    public class DeviceAuthorizationViewModel : ConsentViewModel
    {
        public string UserCode { get; set; }
        public bool ConfirmUserCode { get; set; }
    }
}