using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models.Internal
{

    public enum AppHistoryType
    {
        Add,
        Update,
        Delete
    }

    public class AppHistory
    {

        public AppHistory() { }

        public AppHistory(AppTable tableId, AppHistoryType type, int userId, int? elementId = null, string description = null)
        {
            TableId = tableId;
            Type = type;
            UserId = userId;
            DateTime = DateTime.Now;
            ElementId = elementId;
            if (description != null)
            {
                if (description.Length > 199) description = description.Substring(0, 199);
                Description = description;
            }
        }

        public int Id { get; set; }
        [Required]
        public AppTable TableId { get; set; }
        public int? ElementId { get; set; }
        [Required]
        public AppHistoryType Type { get; set; }
        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
    }
}
