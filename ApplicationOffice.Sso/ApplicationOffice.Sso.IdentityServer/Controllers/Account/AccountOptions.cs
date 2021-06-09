using System;

namespace ApplicationOffice.Sso.IdentityServer.Controllers.Account
{
    public class AccountOptions
    {
        public static readonly bool AllowLocalLogin = true;
        public static readonly bool AllowRememberLogin = true;
        public static readonly TimeSpan RememberMeLoginDuration = TimeSpan.FromDays(30);

        public static readonly bool ShowLogoutPrompt = true;
        public static readonly bool AutomaticRedirectAfterSignOut = false;

        public static readonly string InvalidCredentialsErrorMessage = "Неверное имя пользователя или пароль";
    }
}
