namespace KanbanBoard.Shared.Commands
{
    public class CreateStageCommand : IValidatable
    {
        public Guid Id { get; set; }
        public Guid BoardId { get; set;  }
        public string Title { get; set; } = string.Empty;

        public bool IsValid() => !string.IsNullOrEmpty(Title);
    }
}
