namespace KanbanBoard.Shared.Requests
{
    public class UpdateOrCreateStageRequest : IValidatable
    {
        public Guid BoardId { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Simple validation for the pathc model to make shure do not accept invalid data.
        /// </summary>
        /// <returns>True if object is valid, otherwise false</returns>
        public bool IsValid() => !string.IsNullOrEmpty(Name);
    }
}
