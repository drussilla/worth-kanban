namespace KanbanBoard.Shared
{
    public class BoardDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<StageDto> Stages { get; set; } = new List<StageDto>();
    }
}