namespace ApplicationOffice.Sso.IdentityServer.Controllers.Account
{
    public class LogoutViewModel : LogoutInputModel
    {
        public bool ShowLogoutPrompt { get; set; } = true;
    }
}
