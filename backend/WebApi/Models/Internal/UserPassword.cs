using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Internal
{

    /// <summary>
    /// This trick allows use to write safe code, and be sure that there will be no situation
    /// when user password hash will be sent through API.
    /// 
    /// Entity Framework doesnt have [IgnoreGet] attribute, so without this, we would be obligated
    /// to do "user.Password = null;" in every API endpoint which will be using user table.
    /// 
    /// This is the reason of that separated table. Now, to get password, we must to .Include() this.
    /// </summary>
    public class UserPassword
    {
        public int Id { get; set; }
        [Required]
        [StringLength(64)]
        public string Password { get; set; }
    }

}
