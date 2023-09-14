using System.ComponentModel.DataAnnotations;

namespace KanbanBoard.Server.Models
{
    public class Stage
    {
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; } = string.Empty;

        public Board Board { get; set; } = null!;
        
        public Guid BoardId { get; set; }

        public ICollection<Task> Tasks { get; } = new List<Task>();

    }
}
