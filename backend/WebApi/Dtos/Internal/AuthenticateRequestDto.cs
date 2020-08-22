using System.ComponentModel.DataAnnotations;

namespace WebApi.Dtos.Internal
{
    public class AuthenticateRequestDto
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
