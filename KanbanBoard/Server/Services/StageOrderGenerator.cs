namespace KanbanBoard.Server.Services
{
    /// <summary>
    /// Default implementation of <see cref="IStageOrderGenerator"/> that use ordering with a "step". Every time we need a new order, we will take previous order and add "step" to it.
    /// So if in the future we add reordering, we could just place new items right in the middle of the "step". This is not ideal algorighm and is limited to the "step"/2 reorderings.
    /// </summary>
    public class StageOrderGenerator : IStageOrderGenerator
    {
        private const uint DEFAULT_STEP = 100_000;

        public uint GenerateIntialOrder()
        {
            return DEFAULT_STEP;
        }

        public uint GenerateOrder(uint previousOrder, uint? nextOrder = null)
        { 
            // In this case we are generating order for the last stage
            if (nextOrder == null)
            {
                return previousOrder + DEFAULT_STEP;
            }

            // Input validation
            if (nextOrder <= (previousOrder + 1)) 
            {
                throw new ArgumentOutOfRangeException(
                    nameof(nextOrder), 
                    $"There has to be at least one available 'spot' between previous and next order. Current values: {previousOrder}:{nextOrder}");
            }

            // Here we are placing stage right in between existing stages
            var diff = (uint)(nextOrder - previousOrder) / 2;
            return previousOrder + diff;
        }
    }
}
