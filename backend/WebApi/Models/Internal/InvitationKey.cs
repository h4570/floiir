using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models.Internal
{

    public class InvitationKey
    {
        public int Id { get; set; }
        [StringLength(10)]
        public string Key { get; set; }
        [ForeignKey("User")]
        public int InviterId { get; set; }
        public User Inviter { get; set; }
        public DateTime CreatedAt { get; set; }
        [ForeignKey("User")]
        public int? UsedByUserId { get; set; }
        public User UsedByUser { get; set; }
    }
}
