using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace KanbanBoard.Server.Models
{
    public class Board
    {
        public Guid Id { get;set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Task> Tasks { get; } = new List<Task>();
        public ICollection<Stage> Stages { get; } = new List<Stage>();
    }
}
