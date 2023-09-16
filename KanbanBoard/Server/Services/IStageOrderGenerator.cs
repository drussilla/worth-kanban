namespace KanbanBoard.Server.Services
{
    /// <summary>
    /// Interface to abstract stage order generation logic
    /// </summary>
    public interface IStageOrderGenerator 
    {
        /// <summary>
        /// Generate order for the first stage
        /// </summary>
        uint GenerateIntialOrder();

        /// <summary>
        /// Generate order based on <paramref name="previousOrder"/> and <paramref name="nextOrder"/>. If <paramref name="nextOrder"/> is null, generate order to place it at the end.
        /// </summary>
        /// <param name="previousOrder"></param>
        /// <param name="nextOrder"></param>
        /// <returns>Stage order to place it between previousOrder and nextOrder</returns>
        uint GenerateOrder(uint previousOrder, uint? nextOrder = null);
    }
}
