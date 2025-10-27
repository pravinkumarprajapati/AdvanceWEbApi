using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AdvanceWebApi.Model
{
    public class RegisterModel : IdentityUser
    {
        [Required]
        public required string Password { get; set; }
    }
}
