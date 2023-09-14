using Fluxor;
using KanbanBoard.Shared;
using System.Xml;

namespace KanbanBoard.Client.Store.BoardUseCase
{
    [FeatureState]
    public class BoardsState
    {
        private BoardsState() { }

        public BoardsState(bool isLoading, IDictionary<Guid, BoardState> boards, Guid? selectedBoard)
        {
            IsLoading = isLoading;
            Boards = boards;
            SelectedBoard = selectedBoard;
        }

        public bool IsLoading { get; } = true;

        public IDictionary<Guid, BoardState> Boards { get; } = new Dictionary<Guid, BoardState>();

        public Guid? SelectedBoard { get; }

        public BoardState? GetSelectedBoard => SelectedBoard.HasValue ? Boards[SelectedBoard.Value] : null;
    }

    public class BoardState 
    {
        public BoardState(BoardDto dto)
        {
            Id = dto.Id;
            Name = dto.Name;
            Stages = dto.Stages.ToDictionary(key => key.Id, value => new StageState(value));
        }

        public Guid Id { get; }

        public string? Name { get; }

        public IDictionary<Guid, StageState> Stages { get; } = new Dictionary<Guid, StageState>();

        public StageState? GetStageState(Guid id) => Stages.ContainsKey(id) ? Stages[id] : null;
    }

    public class StageState 
    {
        public StageState(StageDto dto)
        {
            Id = dto.Id;
            Name = dto.Name;
            Tasks = dto.Tasks.ToDictionary(key => key.Id, value => new TaskState(value.Id, value.Title ?? string.Empty, value.Description ?? string.Empty));
        }

        public Guid Id { get; }
        public string? Name { get; }
        public IDictionary<Guid, TaskState> Tasks { get; } = new Dictionary<Guid, TaskState>();
    }

    public record TaskState(Guid Id, string Title, string Description, bool IsEditing = false);
}
