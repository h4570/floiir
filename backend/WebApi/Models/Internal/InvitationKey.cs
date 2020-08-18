using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Internal
{

    public class InvitationKey
    {
        public int Id { get; set; }
        [StringLength(10)]
        public string Key { get; set; }
        public int InviterId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? UsedByUserId { get; set; }
    }
}
