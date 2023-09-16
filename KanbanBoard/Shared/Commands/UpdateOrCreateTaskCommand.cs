namespace KanbanBoard.Shared.Commands
{
    public class UpdateOrCreateTaskCommand
    {
        public Guid StageId { get; set; }
        public Guid BoardId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// Simple validation for the pathc model to make shure do not accept invalid data.
        /// </summary>
        /// <returns>True if object is valid, otherwise false</returns>
        public bool IsValid() => !string.IsNullOrEmpty(Title);
    }
}
