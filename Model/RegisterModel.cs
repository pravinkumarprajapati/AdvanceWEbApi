using Microsoft.AspNetCore.Identity;

namespace AdvanceWebApi.Model
{
    public class RegisterModel : IdentityUser
    {
        public string Password { get; set; }
    }
}
