#nullable disable
using System.ComponentModel.DataAnnotations;

namespace ArrestsCrimesUk.Contracts.Requests
{
    public class UserLoginRequest
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
