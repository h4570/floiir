using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Internal
{

    public class User
    {

        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Login { get; set; }
        [Required]
        [StringLength(64)]
        public string Password { get; set; }
        [Required]
        [StringLength(35)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(35)]
        public string LastName { get; set; }
        [Required]
        [StringLength(254)]
        public string Email { get; set; }

        public string FullName { get { return $"{FirstName} {LastName}"; } }

    }
}
