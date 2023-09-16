namespace KanbanBoard.Shared.Requests
{
    public class CreateBoardRequest : IValidatable
    {
        public string Name { get; set; } = string.Empty;

        public bool IsValid() => !string.IsNullOrEmpty(Name);
    }
}
