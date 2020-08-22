using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models.Internal
{

    public class User
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Login { get; set; }
        [Required]
        [ForeignKey("UserPassword")]
        public int PasswordId { get; set; }
        public UserPassword UserPassword { get; set; }
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
