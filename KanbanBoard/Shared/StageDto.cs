namespace KanbanBoard.Shared
{
    public class StageDto
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; } = string.Empty;

        public List<TaskDto> Tasks { get; set; } = new List<TaskDto>();
    }
}
