using System.ComponentModel.DataAnnotations;

namespace ApplicationOffice.Sso.IdentityServer.Controllers.Account
{
    public class LoginInputModel
    {
        [Required(ErrorMessage = "Поле имя - обязательное")]
        public string Username { get; set; } = default!;

        [Required(ErrorMessage = "Поле пароль - обязательное")]
        public string Password { get; set; } = default!;
        public bool RememberLogin { get; set; }
        public string ReturnUrl { get; set; } = default!;
    }
}