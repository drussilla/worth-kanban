using System.ComponentModel.DataAnnotations;

namespace KanbanBoard.Server.Models
{
    public class Task
    {
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public Stage Stage { get; set; } = null!;
        public Guid StageId { get; set; }

        public Board Board { get; set; } = null!;
        public Guid BoardId { get; set; }
    }
}
