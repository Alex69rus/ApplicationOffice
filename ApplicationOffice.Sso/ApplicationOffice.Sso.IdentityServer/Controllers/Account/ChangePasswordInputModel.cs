using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApplicationOffice.Sso.IdentityServer.Controllers.Account
{
    public class ChangePasswordInputModel : IValidatableObject
    {
        public Uri BackUrl { get; set; }

        [Required(ErrorMessage = "Необходимо указать текущий пароль")]
        public string Password { get; set; } = default!;

        [Required(ErrorMessage = "Необходимо указать новый пароль")]
        public string NewPassword { get; set; } = default!;

        [Required(ErrorMessage = "Необходимо повторно указать новый пароль")]
        public string NewPasswordDublicate { get; set; } = default!;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (NewPassword != NewPasswordDublicate)
                yield return new ValidationResult("Новые пароли не совпадают");
        }
    }
}
