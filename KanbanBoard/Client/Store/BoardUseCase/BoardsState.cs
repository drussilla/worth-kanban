using Fluxor;
using KanbanBoard.Shared;

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

        public BoardState(Guid id, string name, IDictionary<Guid, StageState> stages)
        {
            Id = id;
            Name = name;
            Stages = stages;
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
            Tasks = dto.Tasks.ToDictionary(key => key.Id, value => new TaskState(value.Id, value.Title ?? string.Empty, value.Description ?? string.Empty, true, false));
            IsEditing = false;
            IsPersisted = true;
        }

        public StageState(Guid id, string? name, IDictionary<Guid, TaskState> tasks, bool isPersisted = false, bool isEditing = false)
        {
            Id = id;
            Name = name;
            Tasks = tasks;
            IsEditing = isEditing;
            IsPersisted = isPersisted;
        }

        public Guid Id { get; }

        public string? Name { get; }

        public bool IsEditing { get; }

        /// <summary>
        /// Indicates whether this object is alredy saved on the backned.
        /// </summary>
        public bool IsPersisted { get; }

        public IDictionary<Guid, TaskState> Tasks { get; } = new Dictionary<Guid, TaskState>();
    }

    public record TaskState(Guid Id, string Title, string Description, bool IsPersistent, bool IsEditing);
}
