using KanbanBoard.Shared;
using KanbanBoard.Shared.Commands;
using System.Net.Http.Json;

namespace KanbanBoard.Client.Services
{
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
    }

    public class BoardsService : IBoardService
    {
        private readonly HttpClient httpClient;

        public BoardsService(HttpClient httpClient) 
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<BoardDto>> GetBoardsAsync()
        {
            return await httpClient.GetFromJsonAsync<List<BoardDto>>($"api/board") ?? new List<BoardDto>();
        }

        public async Task UpdateTaskAsync(Guid id, Guid stageId, Guid boardId, string title, string description)
        {
            await httpClient.PatchAsJsonAsync($"api/task/{id}", new PatchOrCreateTaskCommand 
            {
                StageId = stageId,
                BoardId = boardId,
                Title = title,
                Description = description
            });
        }
    }
}
