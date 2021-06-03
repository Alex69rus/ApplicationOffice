using System;
using System.ComponentModel.DataAnnotations;

namespace ApplicationOffice.Sso.IdentityServer.Quickstart.Account
{
    public class ChangePasswordInputModel
    {
        public Uri? BackUrl { get; set; }

        [Required]
        public string Password { get; set; } = default!;

        [Required]
        public string NewPassword { get; set; } = default!;
    }
}
