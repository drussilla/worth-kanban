using KanbanBoard.Client.Pages;
using KanbanBoard.Shared;
using KanbanBoard.Shared.Commands;
using System.Net.Http.Json;

namespace KanbanBoard.Client.Services
{
    /// <summary>
    /// Wrapper around API endponts. This could be splitted in to separate services for Tasks, Boards and Stages.
    /// </summary>
    public interface IBoardService 
    {
        /// <summary>
        /// Gets list of all boards, related Stages and Tasks
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<BoardDto>> GetBoardsAsync();

        /// <summary>
        /// Update task with new title and description
        /// </summary>
        Task UpdateTaskAsync(Guid id, Guid stageId, Guid boardId, string title, string description);

        Task MoveTaskToStageAsync(Guid id, Guid stageId);
        Task DeleteTaskAsync(Guid taskId);
    }

    public class BoardsService : IBoardService
    {
        private readonly HttpClient httpClient;

        public BoardsService(HttpClient httpClient) 
        {
            this.httpClient = httpClient;
        }

        public async Task DeleteTaskAsync(Guid taskId)
        {
            await httpClient.DeleteAsync($"api/task/{taskId}");
        }

        public async Task<IEnumerable<BoardDto>> GetBoardsAsync()
        {
            return await httpClient.GetFromJsonAsync<List<BoardDto>>($"api/board") ?? new List<BoardDto>();
        }

        public async Task MoveTaskToStageAsync(Guid id, Guid stageId)
        {
            await httpClient.PostAsJsonAsync($"api/task/{id}/move", new MoveTaskCommand
            {
                NewStageId = stageId
            });
        }

        public async Task UpdateTaskAsync(Guid id, Guid stageId, Guid boardId, string title, string description)
        {
            await httpClient.PatchAsJsonAsync($"api/task/{id}", new UpdateOrCreateTaskCommand 
            {
                StageId = stageId,
                BoardId = boardId,
                Title = title,
                Description = description
            });
        }
    }
}
