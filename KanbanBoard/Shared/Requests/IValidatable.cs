namespace KanbanBoard.Shared.Requests
{
    /// <summary>
    /// Something that can be validated
    /// </summary>
    public interface IValidatable
    {
        /// <summary>
        /// Report if this object is valid
        /// </summary>
        /// <returns>True if valid, otherwise false.</returns>
        bool IsValid();
    }
}
