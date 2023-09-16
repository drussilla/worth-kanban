namespace KanbanBoard.Shared.Requests
{
    public class UpdateBoardRequest : IValidatable
    {
        public string Name { get; set; }

        /// <summary>
        /// Simple validation for the pathc model to make shure do not accept invalid data.
        /// </summary>
        /// <returns>True if object is valid, otherwise false</returns>
        public bool IsValid() => !string.IsNullOrEmpty(Name);
    }
}
