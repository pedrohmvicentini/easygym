using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class MessageViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public bool Active { get; set; } = true;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string UserId { get; set; }
    }
}
