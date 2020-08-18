using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Internal
{

    public class User
    {
        public int Id { get; set; }
        [StringLength(35)]
        public string FirstName { get; set; }
        [StringLength(35)]
        public string LastName { get; set; }
        [StringLength(254)]
        public string Email { get; set; }
    }
}
