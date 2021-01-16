using System.Collections.Generic;

namespace ApplicationOffice.Sso.Core.Models
{
    public class AddUserDto
    {
        public string UserName { get; init; } = default!;
        public string Password { get; init; } = default!;
        public string PhoneNumber { get; init; } = default!;
        public string Email { get; init; } = default!;
        public Dictionary<string, string> Claims { get; init; } = default!;
    }
}